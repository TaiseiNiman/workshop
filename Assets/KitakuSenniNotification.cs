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
    private float realTime = 0f;

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

        if(current.Minute >= 52){
            //1分が実時間何秒か 
            if(realTime == 0f && current.Minute - 52 > 0 && elapsedTime != 0f){
            realTime = elapsedTime/(current.Minute - 52);
            currentColor = myImage.color;

                // アルファ値を変更（0.0fは完全に透明、1.0fは完全に不透明）
                currentColor.a = 0.5f; // 半透明にする

                // 変更した色をImageコンポーネントに適用
                myImage.color = currentColor;
            }
            else if(realTime != 0f){

                timerText.text = "残り" + Mathf.FloorToInt(realTime*8 - (current.Minute - 52)*realTime).ToString() + "秒です　　次に進んで下さい.";
            }
            elapsedTime += Time.deltaTime;
        }

    }


}