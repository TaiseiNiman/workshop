using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitakuSceneButton : MonoBehaviour
{

    public GameObject submitaa;

    // Start is called before the first frame update
    public void KitakuSceneMoverOnClick()
    {

        submitaa.GetComponent<KitakuSceneManager>().eh(gameObject.name);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
