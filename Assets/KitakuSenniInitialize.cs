using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitakuSenniInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initialize(string id, string userName, bool isResult)
    {
        //
        //帰宅状況を選ぶbuttonオブジェクトの初期化
        var kitaku = GetChildrenWithTag("KitakuSelector");

        //ユーザーの名前の初期化
        var users = GetChildrenWithTag("UserName");
        foreach (GameObject user in users)
        {
            user.GetComponent<TMP_Text>().text = userName;//ユーザー名を指定
        }
        //時間帯の初期化
        var dateTime = GetChildrenWithTag("KitakuDateTime");
        foreach (GameObject time in dateTime)
        {
            //time.GetComponent<TMP_Text>().text = (id.Length+10).ToString() + "時台";//時間帯を指定
        }
    }
    List<GameObject> GetChildrenWithTag(string tag)
    {
        List<GameObject> taggedChildren = new List<GameObject>();

        // 子要素をすべて取得
        foreach (Transform child in transform)
        {
            // 子要素のタグをチェック
            if (child.CompareTag(tag))
            {
                // タグが一致する子要素をリストに追加
                taggedChildren.Add(child.gameObject);
            }
        }

        return taggedChildren;
    }
}
