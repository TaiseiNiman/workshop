using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TagChecker : MonoBehaviour
{
    //実行するメソッドを指定
    [SerializeField]
    public UnityEvent eventMethod;
    //Tagを設定
    public string tagName;//タグ名を指定します.unityエディタのタグマネージャーに追加する必要があります
    private void Awake()
    {
        gameObject.tag = tagName;
    }
    //public delegate void MyDelegate();
    public void TagCheck()
    {
        // "PrefabInstance"タグを持つすべてのオブジェクトを検索
        GameObject[] prefabInstances = GameObject.FindGameObjectsWithTag(tagName);

        // 検索結果をログに出力
        foreach (GameObject obj in prefabInstances)
        {
            Debug.Log("Found object with tag 'PrefabInstance': " + obj.name);
            //処理を実行
            obj.GetComponent<TagChecker>()?.callBack();
        }
    }
    public void callBack()
    {
        eventMethod?.Invoke();
    }
}