using Unity.Netcode;
using UnityEngine;

public class ClientToServer : NetworkBehaviour
{
    // �N���C�A���g����T�[�o�[�ɑ��M���郁�\�b�h
    [ServerRpc]
    public void SendMessageToServerRpc(string message, ServerRpcParams rpcParams = default)
    {
        Debug.Log($"Received message from client: {message}");
        // �T�[�o�[���ł̏����������ɒǉ�
        ProcessReceivedMessage(message);
    }

    // �T�[�o�[���Ŏ�M�������b�Z�[�W���������郁�\�b�h
    private void ProcessReceivedMessage(string message)
    {
        // �����ɃT�[�o�[���ł̏���������
        Debug.Log($"Processing message on server: {message}");
    }

    // �N���C�A���g���Ń��b�Z�[�W�𑗐M���郁�\�b�h
    public void SendMessageToServer(string message)
    {
        if (IsClient)
        {
            SendMessageToServerRpc(message);
        }
    }
}
