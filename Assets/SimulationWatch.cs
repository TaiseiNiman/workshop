using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimulationWatch : MonoBehaviour
{

    private Stopwatch stopwatch;
    public TMP_Text timerText; // 経過時間を表示するUIテキスト
    private DateTime startTime;
    public TimeSpan elapsed;
    public DateTime currentTime;

    void OnEnable()
    {
        // ストップウォッチを開始
        stopwatch = new Stopwatch();
        stopwatch.Start();

        // 特定の時刻を設定（11時）
        startTime = new DateTime(1997, 7, 1, 11, 0, 0);
    }

    void Update()
    {
        // 経過時間を取得
        elapsed = stopwatch.Elapsed;
        
        // 特定の時刻に経過時間を加算
        if (new DateTime(1997, 7, 1, 20, 0, 0) <= startTime.Add(elapsed * 20))
        {
            currentTime = startTime.Add((elapsed*20 - new TimeSpan(9, 0, 0)) * (40/20) + new TimeSpan(9, 0, 0));
        }
        else
        {
            currentTime = startTime.Add(elapsed * 20);
        }

        // 時刻を表示
        timerText.text = $"現在の時刻: {currentTime:HH時mm分ss秒}";
    }

    void OnDestroy()
    {
        // ストップウォッチを停止
        if (stopwatch != null)
        {
            stopwatch.Stop();
        }
    }

}