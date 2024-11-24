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
    public string path; // path�����w�肷��
    private bool isConnecting = false;
    private float reconnectDelay = 5.0f; // �Đڑ��̊Ԋu�i�b�j

    [SerializeField]
    public UnityEvent<string> onMessage1;

    private Queue<string> messageQueue = new Queue<string>();

    void Start()
    {
        // �������R�[�h���K�v�ł���΂����ɒǉ�
        

    }

    void Update()
    {
        // ���C���X���b�h�Ń��b�Z�[�W������
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
        string messageToSend = "Hello, server!"; // �����͓K�X�ύX
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
        if (isConnecting) ws.Send(message); // ���b�Z�[�W�𑗂�
    }

    void OnDestroy()
    {
        if (ws != null)
        {
            ws.Close();
        }
    }
}