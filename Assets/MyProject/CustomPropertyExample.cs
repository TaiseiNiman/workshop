using UnityEngine;
using UnityEngine.Events;

public class CustomPropertyExample : MonoBehaviour
{
    [SerializeField]
    public UnityEvent customProperty;

    public void CustomMethod()
    {
        Debug.Log("CustomMethod called");
    }
}
