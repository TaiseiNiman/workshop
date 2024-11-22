using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GroupInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    public KitakuSenniInitialize Senni;

    void Awake()
    {
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialized()//‰Šú‰»‚ğs‚¤
    {
        gameObject.GetComponent<TMP_Text>().text = Senni.userName;
    }
}
