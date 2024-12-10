using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using WebSocketSharp;

public class KitakuSceneButton : MonoBehaviour
{

    public OnButtonWithErrorMessage submitaa;
    public UnityEvent<string,string,string> kitakuSelectButtonOnClick;
    public KitakuSenniInitialize parent;
    public UnityEvent doubleCliked;
    public UnityEvent oneCliked;
    public UnityEvent invalidClicked;

    // Start is called before the first frame update
    public void KitakuSceneMoverOnClick()
    {
        string message;
        if(gameObject.name == submitaa.MoveSceneNumber)
        {
            //選択されているものをもう一度押したときの動作
            message = $"{parent.parent.KitakuStateId}";
            kitakuSelectButtonOnClick?.Invoke("DELETE", parent.parent.userId, message);
            //doubleCliked?.Invoke();
        }
        else
        {
            message = $"{parent.parent.KitakuStateId}{gameObject.name}";
            kitakuSelectButtonOnClick?.Invoke("ACTION", parent.parent.userId, message);
            //oneCliked?.Invoke();
        }
        
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

    public void ActionSuccess(string status){
        if (status.IsNullOrEmpty()) doubleCliked?.Invoke();
        else if (status == gameObject.name) oneCliked?.Invoke();
        else invalidClicked?.Invoke();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
