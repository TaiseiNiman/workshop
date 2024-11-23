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
    public UnityEvent<string,string,bool,GameObject> initiailze;
    //�V�~�����[�V���������I�����Ɏ��s�����C�x���g���\�b�h���w��
    [SerializeField]
    public UnityEvent simulationOnClose;
    //�v���n�u�������ϐ�
    public string userName;
    public string KitakuStateId;
    public bool IsResult = true;
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
        int num;
        int hour;
        int day;
        //�����J�ڂ���������
        if (!ContainsOtherThanOne(KitakuStateId) && IsResult)
        {
            hour = (KitakuStateId.Length + 11) % 24;
            day = (KitakuStateId.Length + 11) / 24;
            //���A���U���g��ʂ�\�����Ă��܂�
            //12��,13��...�ƂȂ����玩���őJ��
            if (current.currentTime > new DateTime(1997, 7, 1 + day, hour, 0, 0))
            {
                KitakuSenniUpdate(1);//�����X�V
            }
            
        }
        else if (!ContainsOtherThanOne(KitakuStateId))
        {
            hour = (KitakuStateId.Length + 10) % 24;
            day = (KitakuStateId.Length + 10) / 24;
            //���A�A��I����ʂ�\�����Ă��܂�
            //11��45��,12��45��,...�ƂȂ����玩���őJ��
            if (current.currentTime > new DateTime(1997, 7, 1 + day, hour, 45, 0))
            {
                KitakuSenniUpdate(0);//�����X�V 0�͋󕶎�����Ӗ�����
            }
            
        }
        else
        {
            hour = (KitakuStateId.Length + 10) % 24;
            day = (KitakuStateId.Length + 10) / 24;
            //���A�A��󋵂̉摜��\�����Ă��܂�
            //11��,12��,...�ƂȂ����玩���őJ��
            if (current.currentTime > new DateTime(1997, 7, 1 + day, hour, 0, 0))
            {
                if (!string.IsNullOrEmpty(KitakuStateId))
                {
                    num = (int)char.GetNumericValue(KitakuStateId[KitakuStateId.Length - 1]);
                    Debug.Log($"Character '{KitakuStateId[KitakuStateId.Length - 1]}' converted to int: {num}");
                    KitakuSenniUpdate(num);
                }
                else
                {
                    
                    throw new System.ArgumentException("The string cannot be null or empty.");
                }

            }
            
        }
        //25���Ŏ����I�ɃV�~�����[�V�������I��������
        if(current.currentTime > new DateTime(1997, 7, 2, 1, 0, 0))
        {
            //�V�~�����[�V�������I��������̂ŋA��󋵂�j������
            foreach (Transform child in transform)

            {
                //�q�v�f��j��
                Destroy(child.gameObject);

            }
            //�V���~���[�V�����N���[�Y�C�x���g���X�i�[�̎��s
            simulationOnClose?.Invoke();
        } 

        
    }
    //�A��J�ڏ󋵂�\������Q�[���I�u�W�F�N�g���X�V����
    public void KitakuSenniUpdate(int SelectNumber)
    {
        //�W�F�l���b�NT�̌^��int��string���ɂ���ď����𕪂���.
        

        //�A��J�ڏ󋵂��X�V
        KitakuStateId += SelectNumber == 0 ? string.Empty : SelectNumber.ToString();
        IsResult = !IsResult;
        //�v���n�u�̃C���X�^���X��j��
        foreach (Transform child in transform)

        { 
           //�q�v�f��j��
           Destroy(child.gameObject);
            
        }

        if (!ContainsOtherThanOne(KitakuStateId) && IsResult) {
            activePrefab = selectorResultPrefab;
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
        initiailze?.Invoke(KitakuStateId,userName,IsResult,gameObject);

        // �v���n�u���C���X�^���X��
        GameObject instance = Instantiate(activePrefab, gameObject.transform);
        RectTransform rectTransform = instance.GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
        else
        {
            Debug.LogError("RectTransform component not found on prefab.");
        }


        KitakuSenniInitialize test = instance.GetComponent<KitakuSenniInitialize>();
        test.userName = userName;
        test.gameObject.name = KitakuStateId;
        test.ActiveScreenObject = gameObject;

        // �C���X�^���X�����̃I�u�W�F�N�g�̎q�v�f�Ƃ��Đݒ�
        instance.transform.SetParent(this.transform, false);
        instance.SetActive(true);

        



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

