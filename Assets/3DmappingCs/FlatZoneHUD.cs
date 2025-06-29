using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static CameraImageSender;

public class FlatZoneHUD : MonoBehaviour
{
    [SerializeField]
    public UnityEvent<string> SendQueue;
    public Camera arCamera;
    private int initial = 0;
    [Header("HUD設定")]
    public RectTransform hudRoot; // 親コンテナ (Canvas内)
    public GameObject zonePrefab; // 1マス分のImageプレハブ

    private Image[,] zones;
    private bool[,] zoneCompleted;

    // 内部に履歴を保持（content の配列）
    private List<Dictionary<string, object>> contentsArray = new List<Dictionary<string, object>>();

    // payload まとめ用
    private Dictionary<string, object> payload = new Dictionary<string, object>();

    public void AddPayload(string jpgBase64, float[][] K, float[] R, float[] T, int x, int y)
    {
        // 1つの contents を作る
        var contentsItem = new Dictionary<string, object>()
    {
        { "image", jpgBase64 },
        { "K", K },
        { "R", R },
        { "t", T }
    };

        // 配列に追加
        contentsArray.Add(contentsItem);

        Debug.Log($"保存済み contents 数: {contentsArray.Count}");

        // contents 配列と meta を payload に格納（毎回上書きでOK）
        payload["contents"] = contentsArray;
        payload["meta"] = "3Ddata";

        // 全部そろったらサーバ送信
        if (contentsArray.Count >= x * y)
        {
            string jsonStr = JsonConvert.SerializeObject(payload);
            SendQueue?.Invoke(jsonStr);
            contentsArray.Clear();
        }
    }


    void initializee(int x, int y)
    {
        zones = new Image[x, y];
        zoneCompleted = new bool[x, y];

        // 画面サイズに対して1/16スケーリング
        float hudWidth = Screen.width / 4f;  // 横幅
        float hudHeight = Screen.height / 4f; // 縦幅

        hudRoot.sizeDelta = new Vector2(hudWidth, hudHeight);
        hudRoot.anchorMin = new Vector2(1, 1);
        hudRoot.anchorMax = new Vector2(1, 1);
        hudRoot.pivot = new Vector2(1, 1);
        hudRoot.anchoredPosition = new Vector2(-20, -20); // 右上に配置

        float cellWidth = hudWidth / x;
        float cellHeight = hudHeight / y;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject zone = Instantiate(zonePrefab, hudRoot);
                RectTransform rt = zone.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(cellWidth, cellHeight);
                rt.anchoredPosition = new Vector2(i * cellWidth, -j * cellHeight);
                zones[i, j] = zone.GetComponent<Image>();
                zones[i, j].color = Color.gray;
            }
        }
    }

    public void Zoneing(string json)
    {
        ZoneingParams p = JsonConvert.DeserializeObject<ZoneingParams>(json);
        Debug.Log($"phi: {p.phi}, theta: {p.theta}, x: {p.x}, y: {p.y}");
        Debug.Log($"K: {p.K}, R: {p.R}, T: {p.T}");
        //canvasの初期化
        if (initial == 0) { 
            initializee(p.x, p.y);
            initial = 1;
        }
        //ゾーニングの計算
        if (p.theta < 0) p.theta += 2 * Mathf.PI;

        int n = Mathf.FloorToInt(p.theta / (2 * Mathf.PI) * p.x);
        int m = Mathf.FloorToInt(p.phi / Mathf.PI * p.y);

        n = Mathf.Clamp(n, 0, p.x - 1);
        m = Mathf.Clamp(m, 0, p.y - 1);

        if (!zoneCompleted[n, m])
        {
            //ゾーンに静止画を割り当て
            AddPayload(p.jpgBase64, p.K, p.R, p.T, p.x, p.y);
            zoneCompleted[n, m] = true;
            zones[n, m].color = Color.green;
            Debug.Log($"ゾーン({n},{m}) を完了");
        }
    }
}
