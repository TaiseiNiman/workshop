using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GroupInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    public KitakuSenniInitialize Senni;
    public TMP_Text title;

    void Start()
    {
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialized()//‰Šú‰»‚ğs‚¤
    {
        title.text = Senni.userName;
    }
}
