using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ButtonClickHandler : MonoBehaviour
{
    public Button myButton;
    [SerializeField]
    public UnityEvent<int> method;

    // �p�u���b�N�v���p�e�B�Őݒ肷�郁�\�b�h

    void Start()
    {
        // �{�^���̃N���b�N�C�x���g�Ƀ��X�i�[��ǉ�
        myButton.onClick.AddListener(() => HandleButtonClick(myButton));
    }

    void HandleButtonClick(Button button)
    {
        // �{�^���̖��O�𐔒l�ɕϊ�
        int buttonNameAsNumber;
        if (int.TryParse(button.name, out buttonNameAsNumber))
        {
            // �p�u���b�N�v���p�e�B�Őݒ肵�����\�b�h���Ăяo��
            method?.Invoke(buttonNameAsNumber);
        }
        else
        {
            Debug.LogError("Button name is not a valid number: " + button.name);
        }
    }
}