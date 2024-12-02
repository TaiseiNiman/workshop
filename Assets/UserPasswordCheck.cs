using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UserPasswordCheck : MonoBehaviour
{
    public TMP_Text errorText;
    [SerializeField]
    public UnityEvent<string,string> onCorrect;
    public GameObject passScreen;
    public WebsocketClientConnecition userlog;
    
    public HashSet<string> passHistory = new HashSet<string>();
    public UnityEvent<string> valueChanged = new UnityEvent<string>();

    // Start is called before the first frame update
    void Start()
    {
        //userlog.onMessage1.AddListener(OnMessaged);//イベントリスナーを設定
        valueChanged.AddListener(userlog.SendText);//イベントリスナーを設定
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMessaged(string message)
    {
        //パスワードが認証されたかどうかで処理を分岐させる
        if (message == "password is incorrect")
        {
            errorText.text = "パスワードが間違っています。再入力してください。";
        }
        else if (message == "user is already joined")
        {
            errorText.text = "すでにそのユーザーは認証されました。再入力してください。";
        }
        else if (passHistory.Contains(message.Split(':')[1]))//認証されたパスがパスヒストリーに存在すればtrue
        {
            //ここに参加したユーザーの氏名のメッセージを処理
            onCorrect?.Invoke(message.Split(':')[0],message.Split(':')[1]);
            userlog?.onMessage1.RemoveListener(OnMessaged);//イベントリスナーを削除
            Destroy(passScreen);//パスワード画面を破壊 
        }
    }
    public void onValueChanged(string pass)
    {
        passHistory.Add(pass);//パスワードをヒストリーに追加
        valueChanged?.Invoke(pass);//パスワードをwebsocket通信でサーバーに送る
    }
}
