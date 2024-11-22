using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OnButtonWithErrorMessage : MonoBehaviour
{
    public ExampleScript Example;
    [SerializeField]
    public UnityEvent<int> move;
    public string MoveSceneNumber;//
    public GameObject SceneObject;//
    private bool isResult = true;
    //public GameObject ActiveScreenObject;

    void Awake()
    {
        //���X�i�[�̐ݒ�
        move.AddListener(SceneObject.GetComponent<KitakuSenniInitialize>().ActiveScreenObject.GetComponent<KitakuSenniManager>().KitakuSenniUpdate);
    }

    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        int num;
        foreach (char c in SceneObject.name)
        {
            if (c != '1')
            {
                isResult = false;
            }
        }
        if (true)//Example != null && Example.elapsedTime > 0f
        {
            // int.TryParse���g�p������@
            if (int.TryParse(MoveSceneNumber, out num))
            {
                Debug.Log("int.TryParse: " + num);
                move?.Invoke(num);//
            }
            else
            {
                Debug.Log("�ϊ��Ɏ��s���܂���");
            }
            
        }
    }
}