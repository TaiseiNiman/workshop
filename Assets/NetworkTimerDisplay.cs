using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NetworkTimerDisplay : MonoBehaviour
{
    [SerializeField]
    public UnityEvent<Text> timerDisplay;
    Text tet;
    // Start is called before the first frame update
    void Start()
    {
        tet = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        timerDisplay.Invoke(tet);
        //tet.text = "aaaa";
    }
}
