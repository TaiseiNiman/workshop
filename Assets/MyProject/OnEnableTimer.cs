using System;
using UnityEngine;
using TMPro;

public class ExampleScript : MonoBehaviour
{
    public TMP_Text timerText; // TextMeshProのテキストコンポーネントをアタッチ
    public GameObject SceneObject;
    private DateTimeSync currentWatch;

    public float elapsedTime = 0f;

    void OnEnable()
    {
        
    }

    void Start()
    {
        // 初期化
        if (timerText == null)
        {
            UnityEngine.Debug.LogError("TextMeshProのテキストコンポーネントがアタッチされていません！");
            
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

        if (current.Hour >= 11 && current.Hour < 13 && current.Minute > 37.5)
        {
            limit = true; // 1時間が実時間2分で進む
        }
        else if (current.Hour >= 13 && current.Hour < 18 && current.Minute > 35)
        {
            limit = true; // 1時間が実時間1.5分で進む
        }
        else if (current.Hour >= 18 && current.Hour < 24 && current.Minute > 30)
        {
            limit = true; // 1時間が実時間1分で進む
        }
        else
        {
            elapsedTime = 0f;//時刻を初期化
        }

        if (Mathf.FloorToInt(elapsedTime) % 1 == 0 && limit)
        {
            timerText.text = "残り" + (14 - Mathf.FloorToInt(elapsedTime)).ToString() + "秒です　　次に進んで下さい.";
        }

    }
}