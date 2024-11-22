using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Text.RegularExpressions;

public class simulationStartNotification : MonoBehaviour
{
    public TMP_Text tt;
    [SerializeField]
    public UnityEvent<int> simulation;

    public void notification(string text) {

        string pattern = @"^残り\d+秒でシミュレーションを開始します\.$";
        Regex regex = new Regex(pattern);

        if (regex.IsMatch(text))  tt.text = text;

    }

    public void StartNotification(string text)
    {

        if (text == "WorkshopSimulationStart") {
            simulation.Invoke(1);//シュミレーションを開始する.
            gameObject.SetActive(false);
        }

    }
}