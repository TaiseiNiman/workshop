using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OnButtonWithErrorMessage : MonoBehaviour
{
    public ExampleScript Example;
    [SerializeField]
    public UnityEvent<string, bool> move;
    public string MoveSceneNumber;//
    public GameObject SceneObject;//
    private bool isResult = true;

    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        foreach (char c in SceneObject.name)
        {
            if (c != '1')
            {
                isResult = false;
            }
        }
        if (true)//Example != null && Example.elapsedTime > 0f
        {
            move?.Invoke(SceneObject.name+ MoveSceneNumber, isResult);
        }
    }
}