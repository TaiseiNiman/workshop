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
    public string ServerId;//どのサーバーか認証する。サーバーIDは識別文字列:port番号となる.本当はssh認証で行うべき
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

                // 特定のメッセージをチェック
                if (message.StartsWith(ServerId))
                {
                    var parts = message.Split(':');
                    serverIp = remoteEP.Address.ToString();
                    port = parts[1];
                    UdpConnected?.Invoke(serverIp,port);//udp接続が完了したときに設定したイベントリスナーを実行する
                    break; // 接続が確立されたらループを抜ける
                }
            }
            yield return null; // フレームごとに待機
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