using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using WebSocketSharp;

public class WebSocketClient : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint remoteEP;
    private WebSocket ws;

    void Start()
    {
        udpClient = new UdpClient(8888);
        remoteEP = new IPEndPoint(IPAddress.Any, 0);
        StartCoroutine(ReceiveBroadcast());
    }

    private System.Collections.IEnumerator ReceiveBroadcast()
    {
        while (true)
        {
            if (udpClient.Available > 0)
            {
                var data = udpClient.Receive(ref remoteEP);
                var message = Encoding.UTF8.GetString(data);

                // ����̃��b�Z�[�W���`�F�b�N
                if (message.StartsWith("WebSocketServerWorkshop:"))
                {
                    var parts = message.Split(':');
                    var serverIp = remoteEP.Address.ToString();
                    var port = parts[1];

                    Debug.Log($"Connecting to WebSocket server at {serverIp}:{port}");

                    ws = new WebSocket($"ws://{serverIp}:{port}/UserLog");

                    ws.OnOpen += (sender, e) =>
                    {
                        Debug.Log("Connection open!");
                    };

                    ws.OnMessage += (sender, e) =>
                    {
                        if (e.Data == "StartWorkshopSimulation")
                        {
                            // ���[�N�V���b�v�V�~�����[�V�������J�n����
                            Debug.Log("Start: WorkshopSimulation");
                        }
                        else
                        {
                            Debug.Log("Message received from server: " + e.Data);
                        }
                    };

                    ws.OnError += (sender, e) =>
                    {
                        Debug.LogError("Error: " + e.Message);
                    };

                    ws.OnClose += (sender, e) =>
                    {
                        Debug.Log("Connection closed!");
                    };

                    ws.Connect();
                    Debug.Log("Enter a message to send to the server:");
                    string messageToSend = "Hello, server!"; // �����͓K�X�ύX
                    ws.Send(messageToSend);

                    break; // �ڑ����m�����ꂽ�烋�[�v�𔲂���
                }
            }
            yield return null; // �t���[�����Ƃɑҋ@
        }
    }

    void OnDestroy()
    {
        if (ws != null)
        {
            ws.Close();
        }
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }
}