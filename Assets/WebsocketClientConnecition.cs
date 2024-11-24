using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using WebSocketSharp;
using System.Collections;

public class WebsocketClientConnecition : MonoBehaviour
{
    private WebSocket ws;
    public string path; // path名を指定する
    private bool isConnecting = false;
    private float reconnectDelay = 5.0f; // 再接続の間隔（秒）

    [SerializeField]
    public UnityEvent<string> onMessage1;

    private Queue<string> messageQueue = new Queue<string>();

    void Start()
    {
        // 初期化コードが必要であればここに追加
        

    }

    void Update()
    {
        // メインスレッドでメッセージを処理
        while (messageQueue.Count > 0)
        {
            string message;
            lock (messageQueue)
            {
                message = messageQueue.Dequeue();
            }
            Debug.Log("Dequeued message: " + message);

            if (path == "Timer") {
                GameObject.Find("SimulationWatch").GetComponent<DateTimeSync>().GetTimer(message);
            }
            else if(path == "Notification")
            {
                GameObject.Find("SimulationStartScreen").GetComponent<simulationStartNotification>().notification(message);
                GameObject.Find("SimulationStartScreen").GetComponent<simulationStartNotification>().StartNotification(message);
            }
            
            
        }
    }

    public void ReceiveBroadcast(string ServerIp, string Port)
    {
        Debug.Log($"Connecting to WebSocket server at {ServerIp}:{Port}");

        ws = new WebSocket($"ws://{ServerIp}:{Port}/{path}");

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("Connection open!");
            isConnecting = true;
        };

        ws.OnMessage += (sender, e) =>
        {
            lock (messageQueue)
            {
                Debug.Log("Received message: " + e.Data);
                messageQueue.Enqueue(e.Data);
            }
        };

        ws.OnError += (sender, e) =>
        {
            Debug.LogError("Error: " + e.Message);
            Debug.LogError($"WebSocket Error: {e.Message}, Exception: {e.Exception}");
            Reconnect(ServerIp, Port);
        };

        ws.OnClose += (sender, e) =>
        {
            
            Debug.Log($"Connection closed! Reason: {e.Reason}, Code: {e.Code}");
           Reconnect(ServerIp, Port);
        };

        ws.ConnectAsync();
        Debug.Log("Enter a message to send to the server:");
        string messageToSend = "Hello, server!"; // ここは適宜変更
        if (isConnecting) ws.Send(messageToSend);
    }

    IEnumerator Reconnect(string ip, string port)
    {
        if (isConnecting) yield break;

        isConnecting = true;
        yield return new WaitForSeconds(reconnectDelay);
        ReceiveBroadcast(ip, port);
    }

    public void SendText(string message)
    {
        if (isConnecting) ws.Send(message); // メッセージを送る
    }

    void OnDestroy()
    {
        if (ws != null)
        {
            ws.Close();
        }
    }
}