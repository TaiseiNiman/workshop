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
    private Queue<string> sendQueue;

    [SerializeField]
    public UnityEvent<string> onMessage1;

    private Queue<string> messageQueue = new Queue<string>();

    void Start()
    {
        // 初期化コードが必要であればここに追加
        
        sendQueue = new Queue<string>();
}

    void Update()
    {
        object obj = new object();//lockオブジェクト
        if(ws != null)
        {
            if(ws.ReadyState == WebSocketState.Open)
            {
                if(sendQueue.Count > 0)
                {
                    lock (obj)
                    {
                        string message = sendQueue.Peek();
                        try
                        {
                            ws.Send(message);
                            sendQueue.Dequeue();
                        }
                        catch(Exception ex)
                        {
                            Debug.LogError($"{message} is not sended,exception: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                ws.ConnectAsync();//再接続
            }
        }

        // メインスレッドでメッセージを処理
        while (messageQueue.Count > 0)
        {
            string message;
            lock (messageQueue)
            {
                message = messageQueue.Dequeue();
                onMessage1?.Invoke(message);
                Debug.Log("Dequeued message: " + message);
            }            

        }
    }

    public void ReceiveBroadcast(string ServerIp, string Port)
    {
        Debug.Log($"Connecting to WebSocket server at {ServerIp}:{Port}");

        if (ws != null)
        {
            ws.Close();
            ws = null;
        }

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
            //StartCoroutine(Reconnect(ServerIp, Port));
        };

        ws.OnClose += (sender, e) =>
        {
            
            Debug.Log($"Connection closed! Reason: {e.Reason}, Code: {e.Code}");
            //StartCoroutine(Reconnect(ServerIp, Port));
        };

        ws.ConnectAsync();
        Debug.Log("Enter a message to send to the server:");
        //string messageToSend = "Hello, server!"; // ここは適宜変更
        //if (isConnecting) ws.Send(messageToSend);
    }

    IEnumerator Reconnect(string ip, string port)
    {
        if (isConnecting) yield break;

        Debug.Log("Attempting to reconnect...");
        isConnecting = true;
        yield return new WaitForSeconds(reconnectDelay);
        ReceiveBroadcast(ip, port);
        isConnecting = false;
        Debug.Log("Reconnection attempt finished.");
    }

    public void SendText(string message)
    {
        sendQueue.Enqueue(message); // メッセージをキューに追加
    }

    void OnDestroy()
    {
        if (ws != null)
        {
            ws.Close();
        }
    }
}