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

        string pattern = @"^�c��\d+�b�ŃV�~�����[�V�������J�n���܂�\.$";
        Regex regex = new Regex(pattern);

        if (regex.IsMatch(text))  tt.text = text;

    }

    public void StartNotification(string text)
    {

        if (text == "WorkshopSimulationStart") {
            simulation.Invoke(1);//�V���~���[�V�������J�n����.
            gameObject.SetActive(false);
        }

    }
}