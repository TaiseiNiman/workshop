using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class explainText : MonoBehaviour
{
    //�ϐ��̒�`

    //������(�R���X�g���N�^�̑���)
    public void utilize(string text)
    {
        this.gameObject.GetComponent<Text>().text = text;//�e�L�X�g��������
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
