using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using TMPro;


public class SetSpriteImage : MonoBehaviour
{
    public Image imageComponent; // Imageコンポーネントをアタッチするためのフィールド
    public DateTimeSync current;//シミュレーション時刻クラスを取得
    private Sprite[] sprites;//全スプライトを取得
    private List<DateTime> spriteTime;//スプライト画像の時刻を取得
    private Dictionary<DateTime, Sprite> spriteNames;
    public TMP_Text timeText;
    void Start()
    {
        if (sprites != null) return;
        // Resourcesフォルダーからスプライトをロード
        sprites = Resources.LoadAll<Sprite>("image/amagumo");//全てロード
        //初期化
        spriteTime = new List<DateTime>();
        spriteNames = new Dictionary<DateTime, Sprite>();

        // スプライトがロードできたか確認
        foreach (Sprite sp in sprites)
        {
            int hour;
            int minute;
            if (sp != null)
            {
                //雨雲の時刻を取得
                try {
                    hour = int.Parse(sp.name.Substring(sp.name.Length-4,2));
                    minute = int.Parse(sp.name.Substring(sp.name.Length - 2, 2));
                    //Listに格納
                    spriteTime.Add(new DateTime(1997, 7, 1, hour, minute,0));
                    //スプライト画像の名前と合わせる
                    spriteNames[spriteTime[spriteTime.Count-1]] = sp;
                }
                catch (Exception ex) {
                    Debug.LogError($":{ex.Message}");

                }
            }
            else
            {
                Debug.LogError("スプライトが見つかりませんでした。パスを確認してください。");
            }
        }
        //Listを昇順にソート
        spriteTime.Sort();
        //Listを逆順にソート
        spriteTime.Reverse();
    }
    private void Update()
    {
        int i;
        for(i = 0; i < spriteTime.Count; i++)
        {
            if(spriteTime[i] <= current.currentTime)
            {
                foreach(var sp in spriteNames)
                {
                    if(sp.Key == spriteTime[i])
                    {
                        if(sp.Value != null)
                        {
                            imageComponent.sprite = sp.Value;
                            //文字列を変える
                            timeText.text = $"2011/9/20 {sp.Key:HH時mm分}頃";
                            return;//処理を終わる
                        }
                        else
                        {
                            Debug.Log("スプライト画像が見つかりません");
                        }
                    }
                }
                
            }
        }
    }
}
