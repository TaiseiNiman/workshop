using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class KitakuSenniManager : MonoBehaviour
{
    // �v���n�u���Q�Ƃ��邽�߂̕ϐ�
    public GameObject selectorPrefab;//�A���I��������
    public GameObject selectorResultPrefab;//�I�����ʂ̉��
    public GameObject ImagePrefab;//�A��󋵉摜�̕\�����
    //�v���n�u�̏��������\�b�h��ǉ�
    [SerializeField]
    public UnityEvent<string, string,bool> initiailze;
    //�v���n�u�������ϐ�
    public string userName;
    public string KitakuStateId;
    public bool IsResult;
    //���Ԃ��擾
    public DateTimeSync current;

    //

    //�v���C�x�[�g
    private GameObject activePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current.currentTime > new DateTime(1997, 7, 1, KitakuStateId.Length + 11, 0, 0))
        {
            KitakuSenniUpdate(KitakuStateId + "1", true);//�����X�V
            IsResult = true;
        }
    }
    //�A��J�ڏ󋵂�\������Q�[���I�u�W�F�N�g���X�V����
    public void KitakuSenniUpdate(string kitakuStateId, bool isResult)
    {
        //�v���n�u�̃C���X�^���X��j��
        foreach (Transform child in transform)
        { 
           //�q�v�f��j��
           Destroy(child.gameObject);
            
        }

        if (!ContainsOtherThanOne(kitakuStateId) && isResult) {
            activePrefab = selectorResultPrefab;
        }
        else if (!ContainsOtherThanOne(kitakuStateId))
        {
            activePrefab = selectorPrefab;
        }
        else
        {
            activePrefab = ImagePrefab;
        }

        //�v���n�u�����������C���X�^���X�����������Ă���悤�Ɍ���������
        initiailze?.Invoke(kitakuStateId, userName, isResult);

        // �v���n�u���C���X�^���X��
        GameObject instance = Instantiate(activePrefab);

        //�C���X�^���X�̖��O��ύX
        instance.name = KitakuStateId;

        // �C���X�^���X�����̃I�u�W�F�N�g�̎q�v�f�Ƃ��Đݒ�
        instance.transform.parent = this.transform;

        //�A��J�ڏ󋵂��X�V
        KitakuStateId = kitakuStateId;
        IsResult = isResult;



    }
    //1�ȊO�̕����񂪊܂܂��Ȃ�true�A�����łȂ��Ȃ�false��Ԃ����\�b�h
    public static bool ContainsOtherThanOne(string input)
    {
        foreach (char c in input)
        {
            if (c != '1')
            {
                return true;
            }
        }
        return false;
    }
}

