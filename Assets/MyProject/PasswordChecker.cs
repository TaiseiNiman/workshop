using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PasswordChecker : MonoBehaviour
{
    public TMP_InputField inputField; // �e�L�X�g�t�B�[���h
    public string correctPassword; // �\�ߗ^���Ă������p�X���[�h
    [SerializeField]
    public UnityEvent correctM;//�p�X���[�h���͐������Ɏ��s���郁�\�b�h��ǉ�

    void Start()
    {
        // �e�L�X�g�t�B�[���h�Ƀ��X�i�[��ǉ�
        inputField.onValueChanged.AddListener(CheckPassword);
    }

    void CheckPassword(string input)
    {
        // ���͂��ꂽ�e�L�X�g���p�X���[�h�ƈ�v���邩�m�F
        if (input == correctPassword)
        {
            ExecuteMethod();
        }
    }

    void ExecuteMethod()
    {
        // �C�ӂ̃��\�b�h�����s
        Debug.Log("�p�X���[�h����v���܂����I");
        // �����Ɏ��s�������������L�q
        correctM?.Invoke();
        
    }
}