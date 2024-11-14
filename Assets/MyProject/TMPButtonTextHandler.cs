using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MyProject;

public class TMPButtonTextHandler : MonoBehaviour
{
    public Button button; // �{�^���̎Q��
    [SerializeField]
    public StringEvent Onclick; // �J�X�^���C�x���g

    void Start()
    {
        if (button != null)
        {
            // �{�^���̃N���b�N�C�x���g�Ƀ��X�i�[��ǉ�
            button.onClick.AddListener(() =>
            {
                string buttonText = button.GetComponentInChildren<TMP_Text>().text;
                Debug.Log("�{�^�����N���b�N����܂���: " + buttonText);
                Onclick?.Invoke(buttonText);
            });
        }
        else
        {
            Debug.LogWarning("Button�R���|�[�l���g���ݒ肳��Ă��܂���");
        }
    }
}
