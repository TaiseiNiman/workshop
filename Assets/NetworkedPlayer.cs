using System;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NetworkedPlayer : NetworkBehaviour
{
    // HealthプロパティをNetworkVariableとして定義
    public NetworkVariable<int> Health = new NetworkVariable<int>(100);
    void Start()
    {
        // 初期値の設定
        if (IsServer)
        {
            
        }
    }

    public void healthResult(Text tex)
    {
        // デバッグ用にHealthの値を表示
        if (IsClient)
        {
            Debug.Log($"Health: {Health.Value}");
            tex.text = Health.Value.ToString();
        }
        else
        {
            tex.text = "Server is not";
        }
    }

    // Healthを変更するメソッド
    [ClientRpc]
    public void TakeDamageClientRpc()
    {
        if (IsServer)
        {
            Debug.Log("rpcseikou");
            Health = new NetworkVariable<int>(101);
        }
    }

    public GameObject networkPrefab;

    public void SpawnObject()
    {
        if (IsServer)
        {
            //GameObject obj = Instantiate(networkPrefab);
            gameObject.GetComponent<NetworkObject>().Spawn();
        }
    }

}