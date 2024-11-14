using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimulationWatch : MonoBehaviour
{

    private Stopwatch stopwatch;
    public TMP_Text timerText; // �o�ߎ��Ԃ�\������UI�e�L�X�g
    private DateTime startTime;
    public TimeSpan elapsed;
    public DateTime currentTime;

    void OnEnable()
    {
        // �X�g�b�v�E�H�b�`���J�n
        stopwatch = new Stopwatch();
        stopwatch.Start();

        // ����̎�����ݒ�i11���j
        startTime = new DateTime(1997, 7, 1, 11, 0, 0);
    }

    void Update()
    {
        // �o�ߎ��Ԃ��擾
        elapsed = stopwatch.Elapsed;
        
        // ����̎����Ɍo�ߎ��Ԃ����Z
        if (new DateTime(1997, 7, 1, 20, 0, 0) <= startTime.Add(elapsed * 20))
        {
            currentTime = startTime.Add((elapsed*20 - new TimeSpan(9, 0, 0)) * (40/20) + new TimeSpan(9, 0, 0));
        }
        else
        {
            currentTime = startTime.Add(elapsed * 20);
        }

        // ������\��
        timerText.text = $"���݂̎���: {currentTime:HH��mm��ss�b}";
    }

    void OnDestroy()
    {
        // �X�g�b�v�E�H�b�`���~
        if (stopwatch != null)
        {
            stopwatch.Stop();
        }
    }

}