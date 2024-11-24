using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DateTimeSync : MonoBehaviour
{
    public TMP_Text timerText;
    public DateTime currentTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GetTimer(string time)
    {
        string dateString = time;
        DateTime dateTime;
        bool success = DateTime.TryParse(dateString, out dateTime);

        if (success)
        {
            Debug.Log("Converted DateTime: " + dateTime);
            timerText.text = $"現在の時刻: {dateTime:HH時mm分}";
            currentTime = dateTime;
        }
        else
        {
            Debug.Log("Invalid date string.");
        }
    }
}
