using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitakuSenniInitialize : MonoBehaviour
{
    public string userName;
    public GameObject ActiveScreenObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initialize(string id, string user, bool isResult,GameObject obj)
    {
        //初期化
        userName = user;
        gameObject.name = id;
        ActiveScreenObject = obj;
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
