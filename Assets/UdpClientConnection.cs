using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.Events;

public class UdpClientConnection : MonoBehaviour
{
    private UdpClient udpClient;
    private int UdpClientPort;
    private IPEndPoint remoteEP;
    public string ServerId;//???T?[?o?[???F?????B?T?[?o?[ID??????????:port???????.?{????ssh?F???s?????
    public string serverIp;
    public string port;
    [SerializeField]
    public UnityEvent<string, string> UdpConnected;

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

                // ???????b?Z?[?W???`?F?b?N
                if (message.StartsWith(ServerId))
                {
                    var parts = message.Split(':');
                    serverIp = remoteEP.Address.ToString();
                    port = parts[1];
                    UdpConnected?.Invoke(serverIp,port);//udp???????????????????????C?x???g???X?i?[?????s????
                    break; // ??????m???????à}?[?v?????
                }
            }
            yield return null; // ?t???[????????@
        }
    }

    void OnDestroy()
    {
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }
}