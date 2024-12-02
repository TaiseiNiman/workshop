using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class snsUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    float delta;
    int point;
    string allmessage;
    int length;
    int scaleLength;
    void Start ()
    {
        delta = 0f;
        point = 0;
        allmessage = string.Empty;
        length = 0;
        scaleLength = length;
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta >= 0.3f)
        {
            if(point <= length - (scaleLength))
            {
                gameObject.GetComponent<TMP_Text>().text = allmessage.Substring(point, scaleLength);
            }
            else
            {
                gameObject.GetComponent<TMP_Text>().text = allmessage.Substring(point, length - point) + allmessage.Substring(0, point - length + (scaleLength));
            }
            
            point++;
            if (point >= length) point = 0;//再スタート
            delta = 0f;//再スタート
            
        }
    }

    public void textChange(string message) {
        if(message != allmessage)
        {
            allmessage = message;//メッセージを更新
            length = allmessage.Length;
            if (18 >= length) scaleLength = length;
            else scaleLength = 18;//一番綺麗に出る数字
            delta = 0f;
            point = 0;
        }
    }
}
