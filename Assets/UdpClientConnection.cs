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
    public string ServerId;//�ǂ̃T�[�o�[���F�؂���B�T�[�o�[ID�͎��ʕ�����:port�ԍ��ƂȂ�.�{����ssh�F�؂ōs���ׂ�
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

                // ����̃��b�Z�[�W���`�F�b�N
                if (message.StartsWith(ServerId))
                {
                    var parts = message.Split(':');
                    serverIp = remoteEP.Address.ToString();
                    port = parts[1];
                    UdpConnected?.Invoke(serverIp,port);//udp�ڑ������������Ƃ��ɐݒ肵���C�x���g���X�i�[�����s����
                    break; // �ڑ����m�����ꂽ�烋�[�v�𔲂���
                }
            }
            yield return null; // �t���[�����Ƃɑҋ@
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