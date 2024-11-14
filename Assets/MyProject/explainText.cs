using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class explainText : MonoBehaviour
{
    //変数の定義

    //初期化(コンストラクタの代わり)
    public void utilize(string text)
    {
        this.gameObject.GetComponent<Text>().text = text;//テキストを初期化
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
