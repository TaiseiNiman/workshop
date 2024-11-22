using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitakuSenniInitialize : MonoBehaviour
{
    public string userName;
    public GameObject ActiveScreenObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initialize(string id, string user, bool isResult,GameObject obj)
    {
        //������
        userName = user;
        gameObject.name = id;
        ActiveScreenObject = obj;
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
