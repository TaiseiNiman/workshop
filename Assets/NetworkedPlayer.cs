using System;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NetworkedPlayer : NetworkBehaviour
{
    // Health�v���p�e�B��NetworkVariable�Ƃ��Ē�`
    public NetworkVariable<int> Health = new NetworkVariable<int>(100);
    void Start()
    {
        // �����l�̐ݒ�
        if (IsServer)
        {
            
        }
    }

    public void healthResult(Text tex)
    {
        // �f�o�b�O�p��Health�̒l��\��
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

    // Health��ύX���郁�\�b�h
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