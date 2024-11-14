using System;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;


public class ServerController : MonoBehaviour
{
    public ExampleNetworkDiscovery networkDiscovery;

    void Start()
    {
        Debug.Log($"serverStart ok");
        NetworkManager.Singleton.StartServer();
        //networkDiscovery.StartServer();
        networkDiscovery.StartClient();
    }
}
