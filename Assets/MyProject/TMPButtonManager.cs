using UnityEngine;
using TMPro;

public class TMPButtonManager : MonoBehaviour
{
    public TMP_Text buttonText; // TextMeshPro�̃e�L�X�g�q�v�f�̎Q��

    // �{�^���̕������ύX���郁�\�b�h
    public void SetButtonText(string newText)
    {
        if (buttonText != null)
        {
            buttonText.text = newText;
            Debug.Log("�{�^���̃e�L�X�g���ύX����܂���: " + newText);
        }
        else
        {
            Debug.LogWarning("�{�^���̃e�L�X�g�R���|�[�l���g���ݒ肳��Ă��܂���");
        }
    }
    public void test1() {
        Debug.Log("�K��"+ buttonText.text);
    }
}
