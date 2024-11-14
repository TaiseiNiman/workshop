using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ButtonClickHandler : MonoBehaviour
{
    public Button myButton;
    [SerializeField]
    public UnityEvent<int> method;

    // パブリックプロパティで設定するメソッド

    void Start()
    {
        // ボタンのクリックイベントにリスナーを追加
        myButton.onClick.AddListener(() => HandleButtonClick(myButton));
    }

    void HandleButtonClick(Button button)
    {
        // ボタンの名前を数値に変換
        int buttonNameAsNumber;
        if (int.TryParse(button.name, out buttonNameAsNumber))
        {
            // パブリックプロパティで設定したメソッドを呼び出し
            method?.Invoke(buttonNameAsNumber);
        }
        else
        {
            Debug.LogError("Button name is not a valid number: " + button.name);
        }
    }
}