using UnityEngine;
using TMPro;

public class TMPButtonManager : MonoBehaviour
{
    public TMP_Text buttonText; // TextMeshProのテキスト子要素の参照

    // ボタンの文字列を変更するメソッド
    public void SetButtonText(string newText)
    {
        if (buttonText != null)
        {
            buttonText.text = newText;
            Debug.Log("ボタンのテキストが変更されました: " + newText);
        }
        else
        {
            Debug.LogWarning("ボタンのテキストコンポーネントが設定されていません");
        }
    }
    public void test1() {
        Debug.Log("桑名"+ buttonText.text);
    }
}
