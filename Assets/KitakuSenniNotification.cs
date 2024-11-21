using System;
using UnityEngine;
using TMPro;

public class KitakuSenniNotification : MonoBehaviour
{
    public TMP_Text timerText; // TextMeshProのテキストコンポーネントをアタッチ
    public GameObject SceneObject;
    private DateTimeSync currentWatch;

    public float elapsedTime = 0f;

    void OnEnable()
    {
        // 初期化
        if (timerText == null)
        {
            UnityEngine.Debug.LogError("TextMeshProのテキストコンポーネントがアタッチされていません！");
            elapsedTime = 0f;
        }
        UnityEngine.Debug.Log("オブジェクトがアクティブになりました！");
    }

    void Start()
    {
        // 必要な初期化処理があればここに記述
    }

    void Update()
    {
        currentWatch = GameObject.Find("SimulationWatch").GetComponent<DateTimeSync>();
        if (SceneObject != null && SceneObject.name.Length <= 9)
        {
            if (currentWatch != null && new DateTime(1997, 7, 1, 10 + SceneObject.name.Length, 55, 0) <= currentWatch.currentTime)
            {
                // 特定の処理をここに記述
                // 経過時間を更新
                elapsedTime += Time.deltaTime;

                // 1秒ごとにテキストを更新
                if (Mathf.FloorToInt(elapsedTime) % 1 == 0)
                {
                    timerText.text = "残り" + (15 - Mathf.FloorToInt(elapsedTime)).ToString() + "秒で次の帰宅状況に遷移します";
                }
            }
            else
            {

            }
        }
        else
        {
            // その他の処理をここに記述
            if (currentWatch != null && new DateTime(1997, 7, 1, 10 + SceneObject.name.Length, 54, 0) <= currentWatch.currentTime)
            {
                // 特定の処理をここに記述
                // 経過時間を更新
                elapsedTime += Time.deltaTime;

                // 1秒ごとにテキストを更新
                if (Mathf.FloorToInt(elapsedTime) % 1 == 0)
                {
                    timerText.text = "残り" + (9 - Mathf.FloorToInt(elapsedTime)).ToString() + "秒です";
                }
            }
            else
            {

            }
        }

    }
}