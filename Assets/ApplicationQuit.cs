using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationQuit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplicationQuitOnClick()
    {
        Application.Quit();
        Debug.Log("アプリケーションを終了しました");
    }
}
