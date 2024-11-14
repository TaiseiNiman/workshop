using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;

public class NetworkControl : MonoBehaviour
{
    private NetworkManager networkManager;
    public ExampleNetworkDiscovery networkDiscovery;
    [SerializeField]
    public UnityEvent networkPrefab;
    [SerializeField]
    public UnityEvent healthAdd;



    void Start()
    {
        networkManager = GetComponent<NetworkManager>();
    }

    void OnGUI()
    {
        // �T�[�o�[�N���{�^���̈ʒu�ƃT�C�Y��ݒ�
        Rect serverButtonRect = new Rect(10, 10, 150, 50);
        // �N���C�A���g�쐬�{�^���̈ʒu�ƃT�C�Y��ݒ�
        Rect clientButtonRect = new Rect(10, 70, 150, 50);
        //�l�b�g���[�N�v���n�u�̃C���X�^���X�𐶐�����{�^��
        Rect networkButtonRect = new Rect(70, 70, 150, 50);
        //health�v���p�e�B�̒l��+1����{�^��
        Rect healthAddButtonRect = new Rect(70, 10, 150, 50);


        // �T�[�o�[�N���{�^����`�悵�A�N���b�N���ꂽ�ꍇ�ɃT�[�o�[���N��
        if (GUI.Button(serverButtonRect, "Start Server"))
        {
            if (networkManager != null && !networkManager.IsServer)
            {
                networkManager.StartServer();
                Debug.Log("Server started");
            }
        }

        // �N���C�A���g�쐬�{�^����`�悵�A�N���b�N���ꂽ�ꍇ�ɃN���C�A���g���쐬
        if (GUI.Button(clientButtonRect, "Start Client"))
        {

            networkDiscovery.StartClient();
            networkDiscovery.ClientBroadcast(new DiscoveryBroadcastData() { });
            Debug.Log("Client started");
            
        }
        //�l�b�g���[�N�v���n�u�̃C���X�^���X�𐶐�����{�^��
        if (GUI.Button(networkButtonRect, "networkInstanceCreate"))
        {

            networkPrefab?.Invoke();



        }
        //health�v���p�e�B�̒l��+1����{�^��
        if (GUI.Button(healthAddButtonRect, "HealthAddButton"))
        {

            healthAdd?.Invoke();

        }
    }
}


