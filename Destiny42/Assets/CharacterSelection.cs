using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class CharacterSelection : NetworkBehaviour
{
    [SerializeField] private List<GameObject> beans = new List<GameObject>();
    [SerializeField] private GameObject characterSelectionPanel;
    [SerializeField] private GameObject canvas;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            canvas.SetActive(false);
        }
    }

    public void SpawnPlayer()
    {
        characterSelectionPanel.SetActive(false);
        Spawn(0, LocalConnection);
    }
    
    public void SpawnBoss()
    {
        characterSelectionPanel.SetActive(false);
        Spawn(1, LocalConnection);
    }

    [ServerRpc(RequireOwnership = false)]
    void Spawn(int spawnIndex, NetworkConnection conn)
    {
        GameObject player = Instantiate(beans[spawnIndex], SpawnPoints.instance.transform.position, Quaternion.identity);
        Spawn(player, conn);
    }
}
