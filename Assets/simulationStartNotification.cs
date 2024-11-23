using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Text.RegularExpressions;

public class simulationStartNotification : MonoBehaviour
{
    public TMP_Text tt;
    public TMP_Text title;
    [SerializeField]
    public UnityEvent<int> simulation;

    public void notification(string text) {

        string pattern = @"^�c��\d+�b�ŃV�~�����[�V�������J�n���܂�\.$";
        Regex regex = new Regex(pattern);

        if (regex.IsMatch(text)) {
            title.text = string.Empty;//�^�C�g�����󕶎���ɂ���.
            tt.text = text;
        }

    }

    public void StartNotification(string text)
    {

        if (text == "WorkshopSimulationStart") {
            simulation.Invoke(1);//�V���~���[�V�������J�n����.
            gameObject.SetActive(false);
        }

    }
}