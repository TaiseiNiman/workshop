using System;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class ExampleScript : MonoBehaviour
{
    public TMP_Text timerText; // TextMeshProのテキストコンポーネントをアタッチ
    public GameObject SceneObject;
    private DateTimeSync currentWatch;

    public float elapsedTime = 0f;

    private float realTime = 0f;

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
        if(current.Minute >= 37){
            //1分が実時間何秒か 
            if(realTime == 0f && current.Minute - 37 > 0 && elapsedTime != 0f){
            realTime = elapsedTime/(current.Minute - 37);
            }
            else if(realTime != 0f){

                timerText.text = "残り" + Mathf.FloorToInt(realTime*8 - (current.Minute - 37)*realTime).ToString() + "秒です　　次に進んで下さい.";
            }
            elapsedTime += Time.deltaTime;
        }
    }
}