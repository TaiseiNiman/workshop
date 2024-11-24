using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZikokuAuto : MonoBehaviour
{
    public TMP_Text tex;
    public GameObject kitakuScene;
    // Start is called before the first frame update
    void Start()
    {
        tex.text = (kitakuScene.name.Length + 10).ToString() + "時台";
    }

    void OnEnable()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
