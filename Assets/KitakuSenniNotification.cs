using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KitakuSenniNotification : MonoBehaviour
{
    public TMP_Text timerText; // TextMeshProのテキストコンポーネントをアタッチ
    public GameObject SceneObject;
    private DateTimeSync currentWatch;
    public Image myImage;
    public float elapsedTime = 0f;
    bool limit = false;

    void OnEnable()
    {
        // 初期化
        if (timerText == null)
        {
            UnityEngine.Debug.LogError("TextMeshProのテキストコンポーネントがアタッチされていません！");
           
        }
        else
        {
            
            UnityEngine.Debug.Log("オブジェクトがアクティブになりました！");
        }
        
        
    }

    void Update()
    {
        currentWatch = GameObject.Find("SimulationWatch").GetComponent<DateTimeSync>();
        DateTime current = currentWatch.currentTime;
        Color currentColor;

        // 経過時間を更新
        elapsedTime += Time.deltaTime;

        if (current.Hour >= 11 && current.Hour < 13 && current.Minute >= 52.5)
        {
            if (!limit)
            {
                limit = true;
                elapsedTime = 0f;//経過時間を初期化
                currentColor = myImage.color;

                // アルファ値を変更（0.0fは完全に透明、1.0fは完全に不透明）
                currentColor.a = 0.5f; // 半透明にする

                // 変更した色をImageコンポーネントに適用
                myImage.color = currentColor;

            }
            
        }
        else if (current.Hour >= 13 && current.Hour < 18 && current.Minute >= 50)
        {
            if (!limit)
            {
                limit = true;
                elapsedTime = 0f;//経過時間を初期化
                currentColor = myImage.color;

                // アルファ値を変更（0.0fは完全に透明、1.0fは完全に不透明）
                currentColor.a = 0.5f; // 半透明にする

                // 変更した色をImageコンポーネントに適用
                myImage.color = currentColor;

            }
        }
        else if (current.Hour >= 18 && current.Hour < 24 && current.Minute >= 45)
        {
            if (!limit)
            {
                limit = true;
                elapsedTime = 0f;//経過時間を初期化
                currentColor = myImage.color;

                // アルファ値を変更（0.0fは完全に透明、1.0fは完全に不透明）
                currentColor.a = 0.5f; // 半透明にする

                // 変更した色をImageコンポーネントに適用
                myImage.color = currentColor;

            }
        }
        else
        {
            
        }

        if (Mathf.FloorToInt(elapsedTime) % 1 == 0 && limit)
        {
            timerText.text = "残り" + (15 - Mathf.FloorToInt(elapsedTime)).ToString() + "秒で次の帰宅状況に遷移します";
        }

    }


}