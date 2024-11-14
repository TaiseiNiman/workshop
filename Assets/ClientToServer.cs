using Unity.Netcode;
using UnityEngine;

public class ClientToServer : NetworkBehaviour
{
    // クライアントからサーバーに送信するメソッド
    [ServerRpc]
    public void SendMessageToServerRpc(string message, ServerRpcParams rpcParams = default)
    {
        Debug.Log($"Received message from client: {message}");
        // サーバー側での処理をここに追加
        ProcessReceivedMessage(message);
    }

    // サーバー側で受信したメッセージを処理するメソッド
    private void ProcessReceivedMessage(string message)
    {
        // ここにサーバー側での処理を実装
        Debug.Log($"Processing message on server: {message}");
    }

    // クライアント側でメッセージを送信するメソッド
    public void SendMessageToServer(string message)
    {
        if (IsClient)
        {
            SendMessageToServerRpc(message);
        }
    }
}
