using System;
using UnityEngine;
using TMPro;

public class KitakuSenniNotification : MonoBehaviour
{
    public TMP_Text timerText; // TextMeshProのテキストコンポーネントをアタッチ
    public GameObject SceneObject;
    private DateTimeSync currentWatch;

    public float elapsedTime = 0f;

    void Start()
    {
        // 初期化
        if (timerText == null)
        {
            UnityEngine.Debug.LogError("TextMeshProのテキストコンポーネントがアタッチされていません！");
            elapsedTime = 0f;
        }
        UnityEngine.Debug.Log("オブジェクトがアクティブになりました！");
    }

    void Update()
    {
        currentWatch = GameObject.Find("SimulationWatch").GetComponent<DateTimeSync>();
        DateTime current = currentWatch.currentTime;
        bool limit = false;
        // 経過時間を更新
        elapsedTime += Time.deltaTime;

        if (current.Hour >= 11 && current.Hour < 13 && current.Minute > 52.5)
        {
            limit = true; // 1時間が実時間2分で進む
        }
        else if (current.Hour >= 13 && current.Hour < 18 && current.Minute > 50)
        {
            limit = true; // 1時間が実時間1.5分で進む
        }
        else if (current.Hour >= 18 && current.Hour < 24 && current.Minute > 45)
        {
            limit = true; // 1時間が実時間1分で進む
        }
        else
        {
            limit = true; // 通常の速度
        }

        if (Mathf.FloorToInt(elapsedTime) % 1 == 0 && limit)
        {
            timerText.text = "残り" + (15 - Mathf.FloorToInt(elapsedTime)).ToString() + "秒で次の帰宅状況に遷移します";
        }

    }


}