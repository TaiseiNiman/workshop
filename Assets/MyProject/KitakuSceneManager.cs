using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitakuSceneManager : MonoBehaviour
{
    public string MoveSceneNumber = "1";//デフォルト値
    public GameObject SceneObject;//

    //
    public List<string> parentObject = new List<string>() {
        "screen1","screen2","screen3","screen4"
    }; // 親オブジェクトを指定

    public bool IsChildOf(GameObject child, GameObject parent)
    {
        Transform current = child.transform;

        while (current != null)
        {
            if (current.gameObject == parent)
            {
                return true;
            }
            current = current.parent;
        }

        return false;
    }
    //
    public void eh(string str)

    {
        Debug.Log("aaaaa" + str);
        MoveSceneNumber = str;
    }
    public void KitakuSceneMove()
    {
        //

        GameObject ischildObject;
        if (IsChildOf(gameObject, GameObject.Find(parentObject[0])))
        {
            ischildObject = GameObject.Find(parentObject[0]);
        }
        else if (IsChildOf(gameObject, GameObject.Find(parentObject[1])))
        {
            ischildObject = GameObject.Find(parentObject[1]);
        }
        else if (IsChildOf(gameObject, GameObject.Find(parentObject[2])))
        {
            ischildObject = GameObject.Find(parentObject[2]);
        }
        else
        {
            ischildObject = GameObject.Find(parentObject[3]);
        }

        //
        string MoveSceneName = SceneObject.name + MoveSceneNumber;
        // 非アクティブのオブジェクトを含むすべてのGameObjectを取得
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == MoveSceneName && IsChildOf(obj, ischildObject))
            {
                Debug.Log("Found inactive object: " + obj.name);
                //
                obj.SetActive(true);
                SceneObject.SetActive(false);
                break;
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
