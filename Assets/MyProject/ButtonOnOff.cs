using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonOnOff : MonoBehaviour
{
    public Button myButton; // ボタンの参照
    [SerializeField]
    public UnityEvent method;

    // onClickイベントにメソッドを追加するメソッド
    public void onClick()
    {
        myButton.onClick.AddListener(() => method?.Invoke());
    }

    // onClickイベントからメソッドを削除するメソッド
    public void RemoveMethodFromOnClick()
    {
        myButton.onClick.RemoveAllListeners();
    }

    // ボタンがクリックされたときに実行されるメソッド
    void MyMethod()
    {
        Debug.Log("ボタンがクリックされました！");
    }
}