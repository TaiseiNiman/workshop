using UnityEngine;
using UnityEngine.Events;
using TMPro;
using MyProject;

public class ChildEventHandler : MonoBehaviour
{
    [SerializeField]
    public UnityEvent<string> onChildAddedStr;//�����Ɏq�v�f���ǉ����ꂽ�Ƃ��Ɏ��s����郁�\�b�h��ǉ�����.
    [SerializeField]
    public UnityEvent<GameObject> onChildAddedGam;//�����Ɏq�v�f���ǉ����ꂽ�Ƃ��Ɏ��s����郁�\�b�h��ǉ�����.
    [SerializeField]
    public UnityEvent onChildAdded;//�����Ɏq�v�f���ǉ����ꂽ�Ƃ��Ɏ��s����郁�\�b�h��ǉ�����.
    [SerializeField]
    public UnityEvent<string> onChildRemoved;
    public GameObject prefab; // �ǉ�����v���n�u
    //�q�v�f��ǉ�����Ƃ�,��x�����V�����������ꂽ�q�v�f�̏��������s�����\�b�h���w��
    public class onChildInitialize : UnityEvent<GameObject,GameObject> { }
    [SerializeField]
    public onChildInitialize onChildInitialized;


    // �v���n�u���q�v�f�Ƃ��Ēǉ����郁�\�b�h
    public void AddChild(GameObject child)
    {
            if (prefab != null)
        {
            
            GameObject newChild = Instantiate(prefab, transform);
            //newChild.transform.localPosition = Vector3.zero; // �K�v�ɉ����Ĉʒu�𒲐�
            newChild.SetActive(true);
            Debug.Log("�v���n�u���ǉ�����܂���: " + newChild.name);
            onChildInitialized?.Invoke(newChild,child);//���������s��.

            onChildAdded?.Invoke();//�q�v�f���ǉ��C�x���g�����s
            onChildAddedStr?.Invoke(newChild.GetComponentInChildren<TMP_Text>()?.text as string);
            onChildAddedGam?.Invoke(newChild);
        }
            else
            {
                Debug.LogWarning("�v���n�u���ݒ肳��Ă��܂���");
            }
    }
    public void AddChild()
    {
        if (prefab != null)
        {
            GameObject newChild = Instantiate(prefab, transform);
            //newChild.transform.localPosition = Vector3.zero; // �K�v�ɉ����Ĉʒu�𒲐�
            newChild.SetActive(true);
            Debug.Log("�v���n�u���ǉ�����܂���: " + newChild.name);
            onChildAdded?.Invoke();//�q�v�f���ǉ��C�x���g�����s
            onChildAddedStr?.Invoke(newChild.GetComponentInChildren<TMP_Text>()?.text as string);
            onChildAddedGam?.Invoke(newChild);
        }
        else
        {
            Debug.LogWarning("�v���n�u���ݒ肳��Ă��܂���");
        }
    }
    // ����̎q�v�f�I�u�W�F�N�g���폜���郁�\�b�h
    public void RemoveChild(GameObject child)
    {
        if (child != null && child.transform.parent == transform)
        {
            Destroy(child);
            Debug.Log("�q�v�f���폜����܂���: " + child.name);
            onChildRemoved?.Invoke(child.GetComponentInChildren<TMP_Text>().text);//�q�v�f���폜�C�x���g�����s
        }
        else
        {
            Debug.LogWarning("�w�肳�ꂽ�I�u�W�F�N�g�͎q�v�f�ł͂���܂���");
        }
    }
}
