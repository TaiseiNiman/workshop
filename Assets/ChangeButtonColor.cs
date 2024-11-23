using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour
{
    public Button myButton;

    public void OnClicked()
    {
        // 16進数の色コードを指定 (例: #FF5733)
        string hexColor = "#ABE1F1";
        Color newColor;

        // ColorUtility.TryParseHtmlStringを使って16進数の色コードをColor型に変換
        if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
        {
            // ButtonのImageコンポーネントの色を変更
            myButton.image.color = newColor;
        }
        else
        {
            Debug.LogError("Invalid hex color code");
        }
    }
    public void NotClicked()
    {
        // 16進数の色コードを指定 (例: #FF5733)
        string hexColor = "#FFFFFF";//背景色に戻す
        Color newColor;

        // ColorUtility.TryParseHtmlStringを使って16進数の色コードをColor型に変換
        if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
        {
            // ButtonのImageコンポーネントの色を変更
            myButton.image.color = newColor;
        }
        else
        {
            Debug.LogError("Invalid hex color code");
        }
    }
}