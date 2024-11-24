using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace MyProject
{
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        //�w�肳�ꂽUI�I�u�W�F�N�g�̗̈�ɓ������Ƃ��ɂ��̏���ϐ��Ɋi�[����B�ϐ���set�L�[���[�h��p���ĕϐ��̒l���ς�����Ƃ��ɌĂяo�����C�x���g��ݒ肷��.
        [System.Serializable]
        public class MyEvent : UnityEvent<GameObject, Canvas, RectTransform> { }
        [SerializeField]
        public MyEvent OnDropTrigger;//�C�x���g��ݒ�
        /* �h���b�O�I�����ɔC�ӂ̃I�u�W�F�N�g�̗̈�ɏ����ł������Ă��邩���ׂ邽�߂�
         *  ���ׂ����I�u�W�F�N�g���w�肷��.������,�����̗̈�ɓ����Ă���ꍇ�͂��̂����̂ǂꂩ��̗̈�ɓ������ƔF�肵,���ꂪ���ł��邩�͌��i�ɋK�肵�Ȃ�.
        */
        public List<GameObject> triggerObjects;//�̈攻���UI�I�u�W�F�N�g���w��.��RectTransform�R���|�[�l���g�������Ă��邱��.
        //�h���b�O���h���b�v�ł���canvas�I�u�W�F�N�g���w��.���̃R���|�[�l���g�𗘗p������UI�I�u�W�F�N�g�͕K�������Ŏw�肷��canvas�I�u�W�F�N�g�̎q�v�f�⑷�v�f��Б��v�f,...�ȉ������ɂȂ��Ă��Ȃ��Ƃ����Ȃ�.
        public Canvas moveCanvas;
        private RectTransform rectTransform;
        private Canvas canvas;
        private Vector2 originalPosition;
        private GameObject duplicateObject;
        private RectTransform duplicateRectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // �h���b�O�J�n���̏���
            originalPosition = rectTransform.anchoredPosition;

            // UI�I�u�W�F�N�g�𕡐����AOpeningUI Canvas�̎q�v�f�Ƃ��Ēǉ�
            duplicateObject = Instantiate(gameObject, moveCanvas.GetComponent<Canvas>().transform, false);
            duplicateRectTransform = duplicateObject.GetComponent<RectTransform>();

            // �����I�u�W�F�N�g�̈ʒu�ƃT�C�Y�����̃I�u�W�F�N�g�Ɠ����ɐݒ�
            duplicateRectTransform.position = rectTransform.position;
            duplicateRectTransform.rotation = rectTransform.rotation;
            duplicateRectTransform.localScale = rectTransform.localScale;
            duplicateRectTransform.sizeDelta = rectTransform.sizeDelta;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // �h���b�O���̏���
            if (duplicateRectTransform != null)
            {
                duplicateRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // �h���b�O�I�����̏���
            if (duplicateObject != null)
            {
                OnDropTrigger?.Invoke(duplicateObject, canvas, duplicateRectTransform);//�C�x���g���X�i�[�����s
                Destroy(duplicateObject);//�����I�u�W�F�N�g��j��
            }
        }

        void ExecuteSpecificObjectMethod(GameObject specificObject)
        {
            if (OnDropTrigger == null || OnDropTrigger.GetPersistentEventCount() == 0)
            {
                Debug.Log("�C�x���g�Ƀ���b�h���ǉ�����Ă����B");
                return;
            }

            for (int i = 0; i < OnDropTrigger.GetPersistentEventCount(); i++)
            {
                var target = OnDropTrigger.GetPersistentTarget(i) as Component;
                if (target != null && target.gameObject == specificObject)
                {
                    string methodName = OnDropTrigger.GetPersistentMethodName(i);
                    if (!string.IsNullOrEmpty(methodName))
                    {
                        // ���\�b�h�̈����̌^���w�肵�Ď擾
                        MethodInfo method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(GameObject) }, null);
                        if (method != null)
                        {
                            method.Invoke(target, new object[] { gameObject });
                        }
                    }
                }
            }
        }



    }

}