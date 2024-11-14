using UnityEngine;
using UnityEngine.Events;
using TMPro;
using MyProject;

public class ChildEventHandler : MonoBehaviour
{
    [SerializeField]
    public UnityEvent<string> onChildAddedStr;//ここに子要素が追加されたときに実行されるメソッドを追加する.
    [SerializeField]
    public UnityEvent<GameObject> onChildAddedGam;//ここに子要素が追加されたときに実行されるメソッドを追加する.
    [SerializeField]
    public UnityEvent onChildAdded;//ここに子要素が追加されたときに実行されるメソッドを追加する.
    [SerializeField]
    public UnityEvent<string> onChildRemoved;
    public GameObject prefab; // 追加するプレハブ
    //子要素を追加するとき,一度だけ新しく生成された子要素の初期化を行うメソッドを指定
    public class onChildInitialize : UnityEvent<GameObject,GameObject> { }
    [SerializeField]
    public onChildInitialize onChildInitialized;


    // プレハブを子要素として追加するメソッド
    public void AddChild(GameObject child)
    {
            if (prefab != null)
        {
            
            GameObject newChild = Instantiate(prefab, transform);
            //newChild.transform.localPosition = Vector3.zero; // 必要に応じて位置を調整
            newChild.SetActive(true);
            Debug.Log("プレハブが追加されました: " + newChild.name);
            onChildInitialized?.Invoke(newChild,child);//初期化を行う.

            onChildAdded?.Invoke();//子要素が追加イベントを実行
            onChildAddedStr?.Invoke(newChild.GetComponentInChildren<TMP_Text>()?.text as string);
            onChildAddedGam?.Invoke(newChild);
        }
            else
            {
                Debug.LogWarning("プレハブが設定されていません");
            }
    }
    public void AddChild()
    {
        if (prefab != null)
        {
            GameObject newChild = Instantiate(prefab, transform);
            //newChild.transform.localPosition = Vector3.zero; // 必要に応じて位置を調整
            newChild.SetActive(true);
            Debug.Log("プレハブが追加されました: " + newChild.name);
            onChildAdded?.Invoke();//子要素が追加イベントを実行
            onChildAddedStr?.Invoke(newChild.GetComponentInChildren<TMP_Text>()?.text as string);
            onChildAddedGam?.Invoke(newChild);
        }
        else
        {
            Debug.LogWarning("プレハブが設定されていません");
        }
    }
    // 特定の子要素オブジェクトを削除するメソッド
    public void RemoveChild(GameObject child)
    {
        if (child != null && child.transform.parent == transform)
        {
            Destroy(child);
            Debug.Log("子要素が削除されました: " + child.name);
            onChildRemoved?.Invoke(child.GetComponentInChildren<TMP_Text>().text);//子要素が削除イベントを実行
        }
        else
        {
            Debug.LogWarning("指定されたオブジェクトは子要素ではありません");
        }
    }
}
