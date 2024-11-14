using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonOnOff : MonoBehaviour
{
    public Button myButton; // �{�^���̎Q��
    [SerializeField]
    public UnityEvent method;

    // onClick�C�x���g�Ƀ��\�b�h��ǉ����郁�\�b�h
    public void onClick()
    {
        myButton.onClick.AddListener(() => method?.Invoke());
    }

    // onClick�C�x���g���烁�\�b�h���폜���郁�\�b�h
    public void RemoveMethodFromOnClick()
    {
        myButton.onClick.RemoveAllListeners();
    }

    // �{�^�����N���b�N���ꂽ�Ƃ��Ɏ��s����郁�\�b�h
    void MyMethod()
    {
        Debug.Log("�{�^�����N���b�N����܂����I");
    }
}