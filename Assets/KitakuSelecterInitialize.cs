using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitakuSelecterInitialize : MonoBehaviour
{
    public GameObject SceneObject;//
    // Start is called before the first frame update
    void Start()
    {
        if (SceneObject.name.Length < 4) gameObject.SetActive(false);//14時になるまで選べないようにする
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
