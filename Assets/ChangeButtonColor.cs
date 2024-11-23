using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour
{
    public Button myButton;

    public void OnClicked()
    {
        // 16�i���̐F�R�[�h���w�� (��: #FF5733)
        string hexColor = "#ABE1F1";
        Color newColor;

        // ColorUtility.TryParseHtmlString���g����16�i���̐F�R�[�h��Color�^�ɕϊ�
        if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
        {
            // Button��Image�R���|�[�l���g�̐F��ύX
            myButton.image.color = newColor;
        }
        else
        {
            Debug.LogError("Invalid hex color code");
        }
    }
    public void NotClicked()
    {
        // 16�i���̐F�R�[�h���w�� (��: #FF5733)
        string hexColor = "#FFFFFF";//�w�i�F�ɖ߂�
        Color newColor;

        // ColorUtility.TryParseHtmlString���g����16�i���̐F�R�[�h��Color�^�ɕϊ�
        if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
        {
            // Button��Image�R���|�[�l���g�̐F��ύX
            myButton.image.color = newColor;
        }
        else
        {
            Debug.LogError("Invalid hex color code");
        }
    }
}