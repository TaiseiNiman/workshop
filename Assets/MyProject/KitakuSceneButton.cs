using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class KitakuSceneButton : MonoBehaviour
{

    public OnButtonWithErrorMessage submitaa;

    // Start is called before the first frame update
    public void KitakuSceneMoverOnClick()
    {

        submitaa.MoveSceneNumber = (gameObject.name == "1")? "0" : gameObject.name;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
