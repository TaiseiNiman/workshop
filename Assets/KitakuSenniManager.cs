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
        //�����J�ڂ���������
        if (!ContainsOtherThanOne(kitakuStateId) && isResult)
        {
            //���A���U���g��ʂ�\�����Ă��܂�
            //12��,13��...�ƂȂ����玩���őJ��
            if (current.currentTime > new DateTime(1997, 7, 1, KitakuStateId.Length + 11, 0, 0))
            {
                KitakuSenniUpdate(KitakuStateId);//�����X�V
            }
            IsResult = !IsResult;
        }
        else if (!ContainsOtherThanOne(kitakuStateId))
        {
            //���A�A��I����ʂ�\�����Ă��܂�
            //11��45��,12��45��,...�ƂȂ����玩���őJ��
            if (current.currentTime > new DateTime(1997, 7, 1, KitakuStateId.Length + 11, 0, 0))
            {
                KitakuSenniUpdate(KitakuStateId);//�����X�V
            }
            IsResult = !IsResult;
        }
        else
        {
            //���A�A��󋵂̉摜��\�����Ă��܂�
            //11��,12��,...�ƂȂ����玩���őJ��
            if (current.currentTime > new DateTime(1997, 7, 1, KitakuStateId.Length + 11, 0, 0))
            {
                if (string.IsNullOrEmpty(str))
                {
                    throw new System.ArgumentException("The string cannot be null or empty.");
                }
                
                KitakuSenniUpdate(kitakuStateId + kitakuStateId[kitakuStateId.Length - 1]);//�����X�V
            }
            IsResult = !IsResult;
        }

        
    }
    //�A��J�ڏ󋵂�\������Q�[���I�u�W�F�N�g���X�V����
    public void KitakuSenniUpdate(string SelectNumber)
    {
        //�A��J�ڏ󋵂��X�V
        KitakuStateId += SelectNumber;
        //�v���n�u�̃C���X�^���X��j��
        foreach (Transform child in transform)

        { 
           //�q�v�f��j��
           Destroy(child.gameObject);
            
        }

        if (!ContainsOtherThanOne(KitakuStateId) && isResult) {
            activePrefab = selectorResultPrefab;
            IsResult = false;
        }
        else if (!ContainsOtherThanOne(KitakuStateId))
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

