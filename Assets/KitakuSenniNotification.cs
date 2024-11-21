using System;
using UnityEngine;
using TMPro;

public class KitakuSenniNotification : MonoBehaviour
{
    public TMP_Text timerText; // TextMeshPro�̃e�L�X�g�R���|�[�l���g���A�^�b�`
    public GameObject SceneObject;
    private DateTimeSync currentWatch;

    public float elapsedTime = 0f;

    void OnEnable()
    {
        // ������
        if (timerText == null)
        {
            UnityEngine.Debug.LogError("TextMeshPro�̃e�L�X�g�R���|�[�l���g���A�^�b�`����Ă��܂���I");
            elapsedTime = 0f;
        }
        UnityEngine.Debug.Log("�I�u�W�F�N�g���A�N�e�B�u�ɂȂ�܂����I");
    }

    void Start()
    {
        // �K�v�ȏ���������������΂����ɋL�q
    }

    void Update()
    {
        currentWatch = GameObject.Find("SimulationWatch").GetComponent<DateTimeSync>();
        if (SceneObject != null && SceneObject.name.Length <= 9)
        {
            if (currentWatch != null && new DateTime(1997, 7, 1, 10 + SceneObject.name.Length, 55, 0) <= currentWatch.currentTime)
            {
                // ����̏����������ɋL�q
                // �o�ߎ��Ԃ��X�V
                elapsedTime += Time.deltaTime;

                // 1�b���ƂɃe�L�X�g���X�V
                if (Mathf.FloorToInt(elapsedTime) % 1 == 0)
                {
                    timerText.text = "�c��" + (15 - Mathf.FloorToInt(elapsedTime)).ToString() + "�b�Ŏ��̋A��󋵂ɑJ�ڂ��܂�";
                }
            }
            else
            {

            }
        }
        else
        {
            // ���̑��̏����������ɋL�q
            if (currentWatch != null && new DateTime(1997, 7, 1, 10 + SceneObject.name.Length, 54, 0) <= currentWatch.currentTime)
            {
                // ����̏����������ɋL�q
                // �o�ߎ��Ԃ��X�V
                elapsedTime += Time.deltaTime;

                // 1�b���ƂɃe�L�X�g���X�V
                if (Mathf.FloorToInt(elapsedTime) % 1 == 0)
                {
                    timerText.text = "�c��" + (9 - Mathf.FloorToInt(elapsedTime)).ToString() + "�b�ł�";
                }
            }
            else
            {

            }
        }

    }
}