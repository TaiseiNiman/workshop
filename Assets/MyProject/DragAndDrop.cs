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
        //指定されたUIオブジェクトの領域に入ったときにその情報を変数に格納する。変数はsetキーワードを用いて変数の値が変わったときに呼び出されるイベントを設定する.
        [System.Serializable]
        public class MyEvent : UnityEvent<GameObject, Canvas, RectTransform> { }
        [SerializeField]
        public MyEvent OnDropTrigger;//イベントを設定
        /* ドラッグ終了時に任意のオブジェクトの領域に少しでも入っているか調べるために
         *  調べたいオブジェクトを指定する.ただし,複数の領域に入っている場合はそのうちのどれか一つの領域に入ったと認定し,それが何であるかは厳格に規定しない.
        */
        public List<GameObject> triggerObjects;//領域判定のUIオブジェクトを指定.※RectTransformコンポーネントを持っていること.
        //ドラッグ＆ドロップできるcanvasオブジェクトを指定.このコンポーネントを利用したいUIオブジェクトは必ずここで指定するcanvasオブジェクトの子要素や孫要素やひ孫要素,...以下続くになっていないといけない.
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
            // ドラッグ開始時の処理
            originalPosition = rectTransform.anchoredPosition;

            // UIオブジェクトを複製し、OpeningUI Canvasの子要素として追加
            duplicateObject = Instantiate(gameObject, moveCanvas.GetComponent<Canvas>().transform, false);
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
                OnDropTrigger?.Invoke(duplicateObject, canvas, duplicateRectTransform);//イベントリスナーを実行
                Destroy(duplicateObject);//複製オブジェクトを破壊
            }
        }

        void ExecuteSpecificObjectMethod(GameObject specificObject)
        {
            if (OnDropTrigger == null || OnDropTrigger.GetPersistentEventCount() == 0)
            {
                Debug.Log("イベントにメソッドが追加されていません。");
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
                        // メソッドの引数の型を指定して取得
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