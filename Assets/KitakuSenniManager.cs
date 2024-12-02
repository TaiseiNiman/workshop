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
    private string _userId;
    public string userId { get { return _userId; } set { _userId = value; } }
    //時間を取得
    public DateTimeSync current;

    public KitakuSenniInitialize child;

    public WebsocketClientConnecition simulation;

    private float updateErapsed;
    //

    //プライベート
    private GameObject activePrefab;

    // Start is called before the first frame update
    void Start()
    {
        updateErapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateErapsed += Time.deltaTime;
        int num;
        int hour;
        int day;
        string status = "";
        if (child != null)
        {
            if (child.child != null)
            {
                if (child.child.MoveSceneNumber != null)
                {
                    //ユーザーが選んだ帰宅選択状況で更新
                    status = child.child.MoveSceneNumber;

                }
                else if (child.child.TestMoveSceneNumber != null)
                {
                    //ルール上選択できる帰宅遷移状況のうちのどれか一つで更新
                    status = child.child.TestMoveSceneNumber;
                }
                else
                {
                    //
                    
                }
            }
        }

            //自動遷移を実装する
            if (!ContainsOtherThanOne(KitakuStateId) && IsResult)
            {
                hour = (KitakuStateId.Length + 11) % 24;
                day = (KitakuStateId.Length + 11) / 24;
                //今、リザルト画面を表示しています
                //12時,13時...となったら自動で遷移
                if (current.currentTime > new DateTime(1997, 7, 1 + day, hour, 0, 0))
                {
                    KitakuSenniUpdate(1);//自動更新
                }

            }
            else if (!ContainsOtherThanOne(KitakuStateId))
            {
                hour = (KitakuStateId.Length + 10) % 24;
                day = (KitakuStateId.Length + 10) / 24;
                //今、帰宅選択画面を表示しています
                //11時45分,12時45分,...となったら自動で遷移
                if (current.currentTime > new DateTime(1997, 7, 1 + day, hour, 45, 0))
                {
                
                if(updateErapsed >= 1.0f)
                {
                    simulation.onMessage1.AddListener(callbackStatusUpdate);
                    simulation.SendText($"ACTION:{userId}:{KitakuStateId + status}");//メッセージを送信する
                    updateErapsed = 0;
                }
                //同期処理
                
                }

            }
            else
            {
                hour = (KitakuStateId.Length + 10) % 24;
                day = (KitakuStateId.Length + 10) / 24;
                //今、帰宅状況の画像を表示しています
                //11時,12時,...となったら自動で遷移
                if (current.currentTime > new DateTime(1997, 7, 1 + day, hour, 0, 0))
                {
                    if (!string.IsNullOrEmpty(KitakuStateId))
                    {
                        num = (int)char.GetNumericValue(KitakuStateId[KitakuStateId.Length - 1]);
                        Debug.Log($"Character '{KitakuStateId[KitakuStateId.Length - 1]}' converted to int: {num}");
                        KitakuSenniUpdate(num);
                    }
                    else
                    {

                        throw new System.ArgumentException("The string cannot be null or empty.");
                    }

                }

            }

        //24時で会社内に残るを選択した人は自動で画像を表示
        if (current.currentTime > new DateTime(1997, 7, 2, 0, 0, 0) && !ContainsOtherThanOne(KitakuStateId) && !IsResult)
        {
            KitakuSenniUpdate(0);
        }
        //25時で自動的にシミュレーションを終了させる
        if (current.currentTime > new DateTime(1997, 7, 2, 1, 0, 0))
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

    private void callbackStatusUpdate(string messages)//書き込み後そのまま反映させるメソッド
    {
        //自動遷移の処理を行う
        string result = messages.Split(':')[0];
        string before = messages.Split(':')[2];
        string update = messages.Split(':')[3];
        if (before == userId)//id一致のみ
        {
            if ("your selected simulation status was updated" == result)//書き込み成功時のみ
            {
                //コールバック関数を削除
                simulation.onMessage1.RemoveListener(callbackStatusUpdate);
                //帰宅状況を遷移
                if (update[update.Length - 1].ToString() == "1") KitakuSenniUpdate(0);//自動更新 0は空文字列を意味する
                else KitakuSenniUpdate(update[update.Length - 1].ToString());
            }
        }
    }

    //帰宅遷移状況を表示するゲームオブジェクトを更新する
    public void KitakuSenniUpdate(int SelectNumber)
    {
        //ジェネリックTの型がintかstringかによって処理を分ける.
        

        //帰宅遷移状況を更新
        KitakuStateId += SelectNumber == 0 ? string.Empty : SelectNumber.ToString();
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


        child = instance.GetComponent<KitakuSenniInitialize>();
        child.userName = userName;
        child.gameObject.name = KitakuStateId;
        child.ActiveScreenObject = gameObject;
        child.parent = gameObject.GetComponent<KitakuSenniManager>();//親要素の詳細情報を得る

        // インスタンスをこのオブジェクトの子要素として設定
        instance.transform.SetParent(this.transform, false);
        instance.SetActive(true);

        



    }

    public void KitakuSenniUpdate(string SelectNumber)
    {
        //ジェネリックTの型がintかstringかによって処理を分ける.


        //帰宅遷移状況を更新
        KitakuStateId += SelectNumber;
        IsResult = !IsResult;
        //プレハブのインスタンスを破棄
        foreach (Transform child in transform)

        {
            //子要素を破棄
            Destroy(child.gameObject);

        }

        if (!ContainsOtherThanOne(KitakuStateId) && IsResult)
        {
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
        initiailze?.Invoke(KitakuStateId, userName, IsResult, gameObject);

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


        child = instance.GetComponent<KitakuSenniInitialize>();
        child.userName = userName;
        child.gameObject.name = KitakuStateId;
        child.ActiveScreenObject = gameObject;
        child.parent = gameObject.GetComponent<KitakuSenniManager>();//親要素の詳細情報を得る

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

    public void Initialized(string name, string id)
    {
        userName = name;//参加者の氏名を格納
        userId = id;//参加者のパスワードを格納

    }
}

