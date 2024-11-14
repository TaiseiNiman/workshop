using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragAndDropInScrollView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /* �h���b�O�I�����ɔC�ӂ̃I�u�W�F�N�g�̗̈�ɏ����ł������Ă��邩���ׂ邽�߂�
     *  ���ׂ����I�u�W�F�N�g���w�肷��.������,�����̗̈�ɓ����Ă���ꍇ�͂��̂����̂ǂꂩ��̗̈�ɓ������ƔF�肵,���ꂪ���ł��邩�͌��i�ɋK�肵�Ȃ�.
    */
    public List<Object> triggerObject;//�̈攻��̃I�u�W�F�N�g���w��.
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;
    private GameObject duplicateObject;
    private RectTransform duplicateRectTransform;
    private Canvas openingUICanvas;
    private Canvas testCanvas;
    private RectTransform testCanvasRectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        openingUICanvas = GameObject.Find("OpeningUI").GetComponent<Canvas>();
        testCanvas = GameObject.Find("testcanvas").GetComponent<Canvas>();
        testCanvasRectTransform = testCanvas.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �h���b�O�J�n���̏���
        originalPosition = rectTransform.anchoredPosition;

        // UI�I�u�W�F�N�g�𕡐����AOpeningUI Canvas�̎q�v�f�Ƃ��Ēǉ�
        duplicateObject = Instantiate(gameObject, openingUICanvas.transform, false);
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
            // �����I�u�W�F�N�g��testcanvas�̗̈���ɂ��邩�`�F�b�N
            if (RectTransformUtility.RectangleContainsScreenPoint(testCanvasRectTransform, duplicateRectTransform.position, canvas.worldCamera))
            {
                // �̈���ɂ���ꍇ�Atestcanvas�̎q�v�f�Ƃ��Ēǉ����A�T�C�Y�𒲐�
                duplicateObject.transform.SetParent(testCanvas.transform, false);
                duplicateRectTransform.anchorMin = new Vector2(0, 0);
                duplicateRectTransform.anchorMax = new Vector2(1, 1);
                duplicateRectTransform.offsetMin = Vector2.zero;
                duplicateRectTransform.offsetMax = Vector2.zero;

                // ���̃I�u�W�F�N�g���폜
                Destroy(gameObject);
            }
            else
            {
                // �̈�O�ɂ���ꍇ�A�����I�u�W�F�N�g���폜
                Destroy(duplicateObject);
            }
        }
    }
}
