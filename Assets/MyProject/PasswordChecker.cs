using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using MyProject;

public class PasswordChecker : MonoBehaviour
{
    public TMP_InputField inputField; // �e�L�X�g�t�B�[���h
    public string correctPassword; // �\�ߗ^���Ă������p�X���[�h
    public GameObject KitakuSceneObject;
    public GameObject PasswordObject;
    [SerializeField]
    public UnityEvent correctM;//�p�X���[�h���͐������Ɏ��s���郁�\�b�h��ǉ�

    void Start()
    {
        //�p�X���[�h��ini�t�@�C������ǂݍ���
        IniReader iniReader = new IniReader();
        iniReader.LoadIniFile("workshopSimulationPassword");
        switch ((int) char.GetNumericValue(PasswordObject.name[PasswordObject.name.Length-1]))
        {
            case 1:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length+10).ToString(), "���");
                break;
            case 2:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length + 10).ToString(), "JR");
                break;
            case 3:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length + 10).ToString(), "�n���S+��Ƃ�[�ƃ��C��+�k��");
                break;
            case 4:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length + 10).ToString(), "���S+�^�N�V�[");
                break;
            case 5:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length + 10).ToString(), "�n���S+�^�N�V�[");
                break;
            case 6:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length + 10).ToString(), "�Зp��");
                break;
            default:
                Debug.Log("�p�X���[�h�ݒ�I�u�W�F�N�g�̖��O���s���ł�.");
                break;

        }
        
        Debug.Log($"correctPassword: {correctPassword}");
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