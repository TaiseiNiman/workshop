using System;
using UnityEngine;
using TMPro;

public class ExampleScript : MonoBehaviour
{
    public TMP_Text timerText; // TextMeshPro�̃e�L�X�g�R���|�[�l���g���A�^�b�`
    public GameObject SceneObject;
    private DateTimeSync currentWatch;

    public float elapsedTime = 0f;

    void OnEnable()
    {
        
    }

    void Start()
    {
        // ������
        if (timerText == null)
        {
            UnityEngine.Debug.LogError("TextMeshPro�̃e�L�X�g�R���|�[�l���g���A�^�b�`����Ă��܂���I");
            
        }
        UnityEngine.Debug.Log("�I�u�W�F�N�g���A�N�e�B�u�ɂȂ�܂����I");
    }

    void Update()
    {
        currentWatch = GameObject.Find("SimulationWatch").GetComponent<DateTimeSync>();
        DateTime current = currentWatch.currentTime;
        bool limit = false;
        // �o�ߎ��Ԃ��X�V
        elapsedTime += Time.deltaTime;

        if (current.Hour >= 11 && current.Hour < 13 && current.Minute > 37.5)
        {
            limit = true; // 1���Ԃ�������2���Ői��
        }
        else if (current.Hour >= 13 && current.Hour < 18 && current.Minute > 35)
        {
            limit = true; // 1���Ԃ�������1.5���Ői��
        }
        else if (current.Hour >= 18 && current.Hour < 24 && current.Minute > 30)
        {
            limit = true; // 1���Ԃ�������1���Ői��
        }
        else
        {
            elapsedTime = 0f;//������������
        }

        if (Mathf.FloorToInt(elapsedTime) % 1 == 0 && limit)
        {
            timerText.text = "�c��" + (14 - Mathf.FloorToInt(elapsedTime)).ToString() + "�b�ł��@�@���ɐi��ŉ�����.";
        }

    }
}