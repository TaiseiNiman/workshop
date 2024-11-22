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
    public UnityEvent<string,string,bool,GameObject> initiailze;
    //シミュレーション自動終了時に実行されるイベントメソッドを指定
    [SerializeField]
    public UnityEvent simulationOnClose;
    //プレハブ初期化変数
    public string userName;
    public string KitakuStateId;
    public bool IsResult = true;
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
        if (!ContainsOtherThanOne(KitakuStateId) && IsResult)
        {
            //今、リザルト画面を表示しています
            //12時,13時...となったら自動で遷移
            if (current.currentTime > new DateTime(1997, 7, 1, KitakuStateId.Length + 11, 0, 0))
            {
                KitakuSenniUpdate(1);//自動更新
            }
            
        }
        else if (!ContainsOtherThanOne(KitakuStateId))
        {
            //今、帰宅選択画面を表示しています
            //11時45分,12時45分,...となったら自動で遷移
            if (current.currentTime > new DateTime(1997, 7, 1, KitakuStateId.Length + 10, 45, 0))
            {
                KitakuSenniUpdate(1);//自動更新
            }
            
        }
        else
        {
            //今、帰宅状況の画像を表示しています
            //11時,12時,...となったら自動で遷移
            if (current.currentTime > new DateTime(1997, 7, 1, KitakuStateId.Length + 11, 0, 0))
            {
                if (string.IsNullOrEmpty(KitakuStateId))
                {
                    throw new System.ArgumentException("The string cannot be null or empty.");
                }
                
                KitakuSenniUpdate(KitakuStateId[KitakuStateId.Length - 1]);//自動更新
            }
            
        }
        //25時で自動的にシミュレーションを終了させる
        if(current.currentTime > new DateTime(1997, 7, 2, 1, 0, 0))
        {
            //シミュレーションを終了させるので帰宅状況を破棄する
            foreach (Transform child in transform)

            {
                //子要素を破棄
                Destroy(child.gameObject);

            }
            //シュミレーションクローズイベントリスナーの実行
            simulationOnClose?.Invoke();
        } 

        
    }
    //帰宅遷移状況を表示するゲームオブジェクトを更新する
    public void KitakuSenniUpdate(int SelectNumber)
    {
        //帰宅遷移状況を更新
        KitakuStateId += SelectNumber.ToString();
        IsResult = !IsResult;
        //プレハブのインスタンスを破棄
        foreach (Transform child in transform)

        { 
           //子要素を破棄
           Destroy(child.gameObject);
            
        }

        if (!ContainsOtherThanOne(KitakuStateId) && IsResult) {
            activePrefab = selectorResultPrefab;
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
        initiailze?.Invoke(KitakuStateId,userName,IsResult,gameObject);

        // プレハブをインスタンス化
        GameObject instance = Instantiate(activePrefab, gameObject.transform);
        RectTransform rectTransform = instance.GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
        else
        {
            Debug.LogError("RectTransform component not found on prefab.");
        }


        KitakuSenniInitialize test = instance.GetComponent<KitakuSenniInitialize>();
        test.userName = userName;
        test.gameObject.name = KitakuStateId;
        test.ActiveScreenObject = gameObject;

        // インスタンスをこのオブジェクトの子要素として設定
        instance.transform.SetParent(this.transform, false);
        instance.SetActive(true);

        



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

