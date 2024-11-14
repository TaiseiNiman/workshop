using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject;
using System;

public class GameBehavor : MonoBehaviour
{
    //変数の設定
    private DateTime startTime = DateTime.Now;//開始時刻を設定
    public GameObject kitakuSeni;
    public GameObject UserSettings;
    //初期化
    TransitionClass Trasition0 = new TransitionClass();
    // Start is called before the first frame update
    void Start()
    {
        GameObject canvasP = Instantiate(kitakuSeni, new Vector3(0, 0, 0), Quaternion.identity);
        canvasP.transform.SetParent(this.gameObject.transform, false);

        GameObject karaP = Instantiate(UserSettings, new Vector3(0, 0, 0), Quaternion.identity);
        karaP.transform.SetParent(this.gameObject.transform, false);
    }

    // Update is called once per framellss
    void Update()
    {
        
    }
}
