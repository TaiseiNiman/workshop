using UnityEngine;

public class ClientMessageSender : MonoBehaviour
{
    public ClientToServer clientToServer;

    void Start()
    {
        // クライアントがサーバーに接続した後にメッセージを送信
        clientToServer.SendMessageToServer("Hello, Server!");
    }
}
