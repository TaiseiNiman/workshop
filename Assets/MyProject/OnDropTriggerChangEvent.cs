using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject;

public class OnDropTriggerChangEvent : MonoBehaviour
{
    //���̃I�u�W�F�N�g�̗̈�ɃC�x���g���̃I�u�W�F�N�g�������Ă����Ƃ��̃C�x���g�n���h���[��ݒ肷����̂ł���.

    void HandleDragAndDrop(GameObject triggerObject)
    {
        //�g���K�[���ꂽ�I�u�W�F�N�g�Ƃ��̃I�u�W�F�N�g�������Ȃ�,�C�x���g���̃I�u�W�F�N�g����V���ȃI�u�W�F�N�g��ǉ�����
        if (triggerObject == transform.gameObject)
        {
            //�����ɐV���ȃI�u�W�F�N�g��ǉ�����.
        }
    }
}
