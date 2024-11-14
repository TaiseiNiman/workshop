using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class expainTextButton : MonoBehaviour
{
    //変数の定義
    private bool clickOn = false;//クリックされたらtrueにする.
    private DateTime clickTimeStamp = new DateTime(2024, 1, 1, 0, 0, 0);//クリックされた時刻を格納.初期値は適当に設定した
    public Button myButton;
    //初期化(コンストラクタの代わり)
    public void utilize(string text)
    {
        this.gameObject.GetComponent<Text>().text = text;//テキストを初期化
        myButton.onClick.AddListener(OnButtonClick);//クリックイベントの設定
    }
    void OnButtonClick()
    {
        clickTimeStamp = DateTime.Now;//クリック時の時刻を記録
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
