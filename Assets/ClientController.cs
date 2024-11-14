using System;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClientController : MonoBehaviour
{
    public ExampleNetworkDiscovery networkDiscovery;
    public Text text;

    void Start()
    {
        Debug.Log("Starting client discovery...");
        text.text = "ahoaaaaa";
        //networkDiscovery.StartClient();
        //var test = new DiscoveryBroadcastData() { };
        //networkDiscovery.ClientBroadcast(test);
    }

    void OnEnable()
    {
        Debug.Log("Enabling server found listener...");
        //networkDiscovery.OnServerFound.AddListener(OnServerFound);
    }

    void OnDisable()
    {
        Debug.Log("Disabling server found listener...");
        //networkDiscovery.OnServerFound.RemoveListener(OnServerFound);
    }

    public void OnServerFound(IPEndPoint sender, DiscoveryResponseData response)
    {
        Debug.Log($"Found server at {response.ServerName} ({sender.Address}:{response.Port})");
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = sender.Address.ToString();
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port = response.Port;
        NetworkManager.Singleton.StartClient();
        text.text = $"Found server at {response.ServerName} ({sender.Address}:{response.Port})";
    }


}
