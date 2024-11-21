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
        //自動遷移を実装する
        if (!ContainsOtherThanOne(kitakuStateId) && isResult)
        {
            //今、リザルト画面を表示しています
            //12時,13時...となったら自動で遷移
            if (current.currentTime > new DateTime(1997, 7, 1, KitakuStateId.Length + 11, 0, 0))
            {
                KitakuSenniUpdate(KitakuStateId);//自動更新
            }
            IsResult = !IsResult;
        }
        else if (!ContainsOtherThanOne(kitakuStateId))
        {
            //今、帰宅選択画面を表示しています
            //11時45分,12時45分,...となったら自動で遷移
            if (current.currentTime > new DateTime(1997, 7, 1, KitakuStateId.Length + 11, 0, 0))
            {
                KitakuSenniUpdate(KitakuStateId);//自動更新
            }
            IsResult = !IsResult;
        }
        else
        {
            //今、帰宅状況の画像を表示しています
            //11時,12時,...となったら自動で遷移
            if (current.currentTime > new DateTime(1997, 7, 1, KitakuStateId.Length + 11, 0, 0))
            {
                if (string.IsNullOrEmpty(str))
                {
                    throw new System.ArgumentException("The string cannot be null or empty.");
                }
                
                KitakuSenniUpdate(kitakuStateId + kitakuStateId[kitakuStateId.Length - 1]);//自動更新
            }
            IsResult = !IsResult;
        }

        
    }
    //帰宅遷移状況を表示するゲームオブジェクトを更新する
    public void KitakuSenniUpdate(string SelectNumber)
    {
        //帰宅遷移状況を更新
        KitakuStateId += SelectNumber;
        //プレハブのインスタンスを破棄
        foreach (Transform child in transform)

        { 
           //子要素を破棄
           Destroy(child.gameObject);
            
        }

        if (!ContainsOtherThanOne(KitakuStateId) && isResult) {
            activePrefab = selectorResultPrefab;
            IsResult = false;
        }
        else if (!ContainsOtherThanOne(KitakuStateId))
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

