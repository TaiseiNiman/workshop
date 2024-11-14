using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OnButtonWithErrorMessage : MonoBehaviour
{
    public ExampleScript Example;
    [SerializeField]
    public UnityEvent move;

    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        if (Example != null && Example.elapsedTime > 0f)
        {
            move?.Invoke();
        }
        else
        {
          
        }
    }
}