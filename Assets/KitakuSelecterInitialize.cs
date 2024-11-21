using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitakuSelecterInitialize : MonoBehaviour
{
    public GameObject SceneObject;//
    // Start is called before the first frame update
    void Start()
    {
        if (SceneObject.name.Length < 4) gameObject.SetActive(false);//14Žž‚É‚È‚é‚Ü‚Å‘I‚×‚È‚¢‚æ‚¤‚É‚·‚é
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
