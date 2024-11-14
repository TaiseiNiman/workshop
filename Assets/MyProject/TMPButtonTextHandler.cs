using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MyProject;

public class TMPButtonTextHandler : MonoBehaviour
{
    public Button button; // ボタンの参照
    [SerializeField]
    public StringEvent Onclick; // カスタムイベント

    void Start()
    {
        if (button != null)
        {
            // ボタンのクリックイベントにリスナーを追加
            button.onClick.AddListener(() =>
            {
                string buttonText = button.GetComponentInChildren<TMP_Text>().text;
                Debug.Log("ボタンがクリックされました: " + buttonText);
                Onclick?.Invoke(buttonText);
            });
        }
        else
        {
            Debug.LogWarning("Buttonコンポーネントが設定されていません");
        }
    }
}
