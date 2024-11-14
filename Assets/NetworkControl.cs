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
        // サーバー起動ボタンの位置とサイズを設定
        Rect serverButtonRect = new Rect(10, 10, 150, 50);
        // クライアント作成ボタンの位置とサイズを設定
        Rect clientButtonRect = new Rect(10, 70, 150, 50);
        //ネットワークプレハブのインスタンスを生成するボタン
        Rect networkButtonRect = new Rect(70, 70, 150, 50);
        //healthプロパティの値を+1するボタン
        Rect healthAddButtonRect = new Rect(70, 10, 150, 50);


        // サーバー起動ボタンを描画し、クリックされた場合にサーバーを起動
        if (GUI.Button(serverButtonRect, "Start Server"))
        {
            if (networkManager != null && !networkManager.IsServer)
            {
                networkManager.StartServer();
                Debug.Log("Server started");
            }
        }

        // クライアント作成ボタンを描画し、クリックされた場合にクライアントを作成
        if (GUI.Button(clientButtonRect, "Start Client"))
        {

            networkDiscovery.StartClient();
            networkDiscovery.ClientBroadcast(new DiscoveryBroadcastData() { });
            Debug.Log("Client started");
            
        }
        //ネットワークプレハブのインスタンスを生成するボタン
        if (GUI.Button(networkButtonRect, "networkInstanceCreate"))
        {

            networkPrefab?.Invoke();



        }
        //healthプロパティの値を+1するボタン
        if (GUI.Button(healthAddButtonRect, "HealthAddButton"))
        {

            healthAdd?.Invoke();

        }
    }
}


