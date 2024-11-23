using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KitakuSenniNotification : MonoBehaviour
{
    public TMP_Text timerText; // TextMeshPro�̃e�L�X�g�R���|�[�l���g���A�^�b�`
    public GameObject SceneObject;
    private DateTimeSync currentWatch;
    public Image myImage;
    public float elapsedTime = 0f;
    bool limit = false;

    void OnEnable()
    {
        // ������
        if (timerText == null)
        {
            UnityEngine.Debug.LogError("TextMeshPro�̃e�L�X�g�R���|�[�l���g���A�^�b�`����Ă��܂���I");
           
        }
        else
        {
            
            UnityEngine.Debug.Log("�I�u�W�F�N�g���A�N�e�B�u�ɂȂ�܂����I");
        }
        
        
    }

    void Update()
    {
        currentWatch = GameObject.Find("SimulationWatch").GetComponent<DateTimeSync>();
        DateTime current = currentWatch.currentTime;
        Color currentColor;

        // �o�ߎ��Ԃ��X�V
        elapsedTime += Time.deltaTime;

        if (current.Hour >= 11 && current.Hour < 13 && current.Minute >= 52.5)
        {
            if (!limit)
            {
                limit = true;
                elapsedTime = 0f;//�o�ߎ��Ԃ�������
                currentColor = myImage.color;

                // �A���t�@�l��ύX�i0.0f�͊��S�ɓ����A1.0f�͊��S�ɕs�����j
                currentColor.a = 0.5f; // �������ɂ���

                // �ύX�����F��Image�R���|�[�l���g�ɓK�p
                myImage.color = currentColor;

            }
            
        }
        else if (current.Hour >= 13 && current.Hour < 18 && current.Minute >= 50)
        {
            if (!limit)
            {
                limit = true;
                elapsedTime = 0f;//�o�ߎ��Ԃ�������
                currentColor = myImage.color;

                // �A���t�@�l��ύX�i0.0f�͊��S�ɓ����A1.0f�͊��S�ɕs�����j
                currentColor.a = 0.5f; // �������ɂ���

                // �ύX�����F��Image�R���|�[�l���g�ɓK�p
                myImage.color = currentColor;

            }
        }
        else if (current.Hour >= 18 && current.Hour < 24 && current.Minute >= 45)
        {
            if (!limit)
            {
                limit = true;
                elapsedTime = 0f;//�o�ߎ��Ԃ�������
                currentColor = myImage.color;

                // �A���t�@�l��ύX�i0.0f�͊��S�ɓ����A1.0f�͊��S�ɕs�����j
                currentColor.a = 0.5f; // �������ɂ���

                // �ύX�����F��Image�R���|�[�l���g�ɓK�p
                myImage.color = currentColor;

            }
        }
        else
        {
            
        }

        if (Mathf.FloorToInt(elapsedTime) % 1 == 0 && limit)
        {
            timerText.text = "�c��" + (15 - Mathf.FloorToInt(elapsedTime)).ToString() + "�b�Ŏ��̋A��󋵂ɑJ�ڂ��܂�";
        }

    }


}