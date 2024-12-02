using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class KitakuSceneButton : MonoBehaviour
{

    public OnButtonWithErrorMessage submitaa;
    public UnityEvent<string,string,string> kitakuSelectButtonOnClick;
    public KitakuSenniInitialize parent;

    // Start is called before the first frame update
    public void KitakuSceneMoverOnClick()
    {
        string message;
        if(gameObject.name == "1")
        {
            message = $"{parent.parent.KitakuStateId}{gameObject.name}";
        }
        else
        {
            message = $"{parent.parent.KitakuStateId}{gameObject.name}";
        }
        //
        kitakuSelectButtonOnClick?.Invoke("ACTION", parent.parent.userId, message);
        //submitaa.MoveSceneNumber = (gameObject.name == "1")? "0" : gameObject.name;
    }

    public void TestFault(string status)
    {
        if (status == gameObject.name) gameObject.SetActive(false);
    }
    public void TestSuccess(string status)
    {
        if (status == gameObject.name) gameObject.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
