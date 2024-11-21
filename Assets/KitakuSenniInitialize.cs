using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitakuSenniInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initialize(string id, string userName, bool isResult)
    {
        //
        //�A��󋵂�I��button�I�u�W�F�N�g�̏�����
        var kitaku = GetChildrenWithTag("KitakuSelector");

        //���[�U�[�̖��O�̏�����
        var users = GetChildrenWithTag("UserName");
        foreach (GameObject user in users)
        {
            user.GetComponent<TMP_Text>().text = userName;//���[�U�[�����w��
        }
        //���ԑт̏�����
        var dateTime = GetChildrenWithTag("KitakuDateTime");
        foreach (GameObject time in dateTime)
        {
            //time.GetComponent<TMP_Text>().text = (id.Length+10).ToString() + "����";//���ԑт��w��
        }
    }
    List<GameObject> GetChildrenWithTag(string tag)
    {
        List<GameObject> taggedChildren = new List<GameObject>();

        // �q�v�f�����ׂĎ擾
        foreach (Transform child in transform)
        {
            // �q�v�f�̃^�O���`�F�b�N
            if (child.CompareTag(tag))
            {
                // �^�O����v����q�v�f�����X�g�ɒǉ�
                taggedChildren.Add(child.gameObject);
            }
        }

        return taggedChildren;
    }
}
