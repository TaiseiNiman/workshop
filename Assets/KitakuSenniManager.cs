using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class KitakuSenniManager : MonoBehaviour
{
    // プレハブを参照するための変数
    public GameObject selectorPrefab;//帰宅を選択する画面
    public GameObject selectorResultPrefab;//選択結果の画面
    public GameObject ImagePrefab;//帰宅状況画像の表示画面
    //プレハブの初期化メソッドを追加
    [SerializeField]
    public UnityEvent<string, string,bool> initiailze;
    //プレハブ初期化変数
    public string userName;
    public string KitakuStateId;
    public bool IsResult;
    //時間を取得
    public DateTimeSync current;

    //

    //プライベート
    private GameObject activePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current.currentTime > new DateTime(1997, 7, 1, KitakuStateId.Length + 11, 0, 0))
        {
            KitakuSenniUpdate(KitakuStateId + "1", true);//自動更新
            IsResult = true;
        }
    }
    //帰宅遷移状況を表示するゲームオブジェクトを更新する
    public void KitakuSenniUpdate(string kitakuStateId, bool isResult)
    {
        //プレハブのインスタンスを破棄
        foreach (Transform child in transform)
        { 
           //子要素を破棄
           Destroy(child.gameObject);
            
        }

        if (!ContainsOtherThanOne(kitakuStateId) && isResult) {
            activePrefab = selectorResultPrefab;
        }
        else if (!ContainsOtherThanOne(kitakuStateId))
        {
            activePrefab = selectorPrefab;
        }
        else
        {
            activePrefab = ImagePrefab;
        }

        //プレハブを初期化しインスタンスを初期化しているように見せかける
        initiailze?.Invoke(kitakuStateId, userName, isResult);

        // プレハブをインスタンス化
        GameObject instance = Instantiate(activePrefab);

        //インスタンスの名前を変更
        instance.name = KitakuStateId;

        // インスタンスをこのオブジェクトの子要素として設定
        instance.transform.parent = this.transform;

        //帰宅遷移状況を更新
        KitakuStateId = kitakuStateId;
        IsResult = isResult;



    }
    //1以外の文字列が含まれるならtrue、そうでないならfalseを返すメソッド
    public static bool ContainsOtherThanOne(string input)
    {
        foreach (char c in input)
        {
            if (c != '1')
            {
                return true;
            }
        }
        return false;
    }
}

