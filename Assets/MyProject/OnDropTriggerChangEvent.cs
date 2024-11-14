using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject;

public class OnDropTriggerChangEvent : MonoBehaviour
{
    //このオブジェクトの領域にイベント元のオブジェクトが入ってきたときのイベントハンドラーを設定するものである.

    void HandleDragAndDrop(GameObject triggerObject)
    {
        //トリガーされたオブジェクトとこのオブジェクトが同じなら,イベント元のオブジェクトから新たなオブジェクトを追加する
        if (triggerObject == transform.gameObject)
        {
            //ここに新たなオブジェクトを追加する.
        }
    }
}
