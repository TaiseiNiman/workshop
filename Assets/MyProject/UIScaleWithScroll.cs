using UnityEngine;
using UnityEngine.EventSystems;

public class UIScaleWithScroll : MonoBehaviour
{
    public RectTransform content;
    public float scaleFactor = 0.1f;

    void Update()
    {
        if (IsMouseOverUI())
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0.0f)
            {
                ScaleContent(scroll);
            }
        }
    }

    bool IsMouseOverUI()
    {
        Vector2 localMousePosition;
        bool isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(content, Input.mousePosition, null, out localMousePosition);
        return isInside && content.rect.Contains(localMousePosition);
    }

    void ScaleContent(float scroll)
    {
        float newScale = content.localScale.x + scroll * scaleFactor;
        newScale = Mathf.Clamp(newScale, 0.5f, 3.0f); // 最小・最大スケールを設定
        content.localScale = new Vector3(newScale, newScale, 1);
    }
}
