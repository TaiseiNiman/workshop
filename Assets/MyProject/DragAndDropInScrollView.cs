using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragAndDropInScrollView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /* ドラッグ終了時に任意のオブジェクトの領域に少しでも入っているか調べるために
     *  調べたいオブジェクトを指定する.ただし,複数の領域に入っている場合はそのうちのどれか一つの領域に入ったと認定し,それが何であるかは厳格に規定しない.
    */
    public List<Object> triggerObject;//領域判定のオブジェクトを指定.
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
        // ドラッグ開始時の処理
        originalPosition = rectTransform.anchoredPosition;

        // UIオブジェクトを複製し、OpeningUI Canvasの子要素として追加
        duplicateObject = Instantiate(gameObject, openingUICanvas.transform, false);
        duplicateRectTransform = duplicateObject.GetComponent<RectTransform>();

        // 複製オブジェクトの位置とサイズを元のオブジェクトと同じに設定
        duplicateRectTransform.position = rectTransform.position;
        duplicateRectTransform.rotation = rectTransform.rotation;
        duplicateRectTransform.localScale = rectTransform.localScale;
        duplicateRectTransform.sizeDelta = rectTransform.sizeDelta;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグ中の処理
        if (duplicateRectTransform != null)
        {
            duplicateRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ドラッグ終了時の処理
        if (duplicateObject != null)
        {
            // 複製オブジェクトがtestcanvasの領域内にあるかチェック
            if (RectTransformUtility.RectangleContainsScreenPoint(testCanvasRectTransform, duplicateRectTransform.position, canvas.worldCamera))
            {
                // 領域内にある場合、testcanvasの子要素として追加し、サイズを調整
                duplicateObject.transform.SetParent(testCanvas.transform, false);
                duplicateRectTransform.anchorMin = new Vector2(0, 0);
                duplicateRectTransform.anchorMax = new Vector2(1, 1);
                duplicateRectTransform.offsetMin = Vector2.zero;
                duplicateRectTransform.offsetMax = Vector2.zero;

                // 元のオブジェクトを削除
                Destroy(gameObject);
            }
            else
            {
                // 領域外にある場合、複製オブジェクトを削除
                Destroy(duplicateObject);
            }
        }
    }
}
