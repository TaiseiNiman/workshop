using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class expainTextButton : MonoBehaviour
{
    //�ϐ��̒�`
    private bool clickOn = false;//�N���b�N���ꂽ��true�ɂ���.
    private DateTime clickTimeStamp = new DateTime(2024, 1, 1, 0, 0, 0);//�N���b�N���ꂽ�������i�[.�����l�͓K���ɐݒ肵��
    public Button myButton;
    //������(�R���X�g���N�^�̑���)
    public void utilize(string text)
    {
        this.gameObject.GetComponent<Text>().text = text;//�e�L�X�g��������
        myButton.onClick.AddListener(OnButtonClick);//�N���b�N�C�x���g�̐ݒ�
    }
    void OnButtonClick()
    {
        clickTimeStamp = DateTime.Now;//�N���b�N���̎������L�^
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
