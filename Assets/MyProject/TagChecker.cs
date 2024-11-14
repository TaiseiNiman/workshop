using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TagChecker : MonoBehaviour
{
    //���s���郁�\�b�h���w��
    [SerializeField]
    public UnityEvent eventMethod;
    //Tag��ݒ�
    public string tagName;//�^�O�����w�肵�܂�.unity�G�f�B�^�̃^�O�}�l�[�W���[�ɒǉ�����K�v������܂�
    private void Awake()
    {
        gameObject.tag = tagName;
    }
    //public delegate void MyDelegate();
    public void TagCheck()
    {
        // "PrefabInstance"�^�O�������ׂẴI�u�W�F�N�g������
        GameObject[] prefabInstances = GameObject.FindGameObjectsWithTag(tagName);

        // �������ʂ����O�ɏo��
        foreach (GameObject obj in prefabInstances)
        {
            Debug.Log("Found object with tag 'PrefabInstance': " + obj.name);
            //���������s
            obj.GetComponent<TagChecker>()?.callBack();
        }
    }
    public void callBack()
    {
        eventMethod?.Invoke();
    }
}