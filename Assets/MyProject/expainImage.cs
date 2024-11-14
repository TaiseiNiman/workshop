using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expainImage : MonoBehaviour
{

    //初期化
    public void utilize(string imageUrl)
    {
        // Imageコンポーネントを取得
        Image imageComponent = this.gameObject.GetComponent<Image>();

        // Resourcesフォルダから画像を読み込む
        Sprite newSprite = Resources.Load<Sprite>(imageUrl);

        // 画像を設定
        imageComponent.sprite = newSprite;
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
