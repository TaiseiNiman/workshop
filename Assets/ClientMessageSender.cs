using UnityEngine;

public class ClientMessageSender : MonoBehaviour
{
    public ClientToServer clientToServer;

    void Start()
    {
        // �N���C�A���g���T�[�o�[�ɐڑ�������Ƀ��b�Z�[�W�𑗐M
        clientToServer.SendMessageToServer("Hello, Server!");
    }
}
