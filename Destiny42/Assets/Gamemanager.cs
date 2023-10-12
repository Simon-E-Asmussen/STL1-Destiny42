using System.Collections;
using System.Collections.Generic;
using FishNet.Component.Spawning;
using FishNet.Object;
using UnityEngine;

public class Gamemanager : NetworkBehaviour
{

    private PlayerSpawner _playerSpawner;
    private GameObject NetworkManager;

    public bool IsBoss;
    public NetworkObject player;
    public NetworkObject boss;
    
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager = GameObject.Find("NetworkManager");
        _playerSpawner = NetworkManager.GetComponent<PlayerSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBoss)
        {
            _playerSpawner.ChangePlayerPrefab(boss);
        }
        else
        {
            _playerSpawner.ChangePlayerPrefab(player);
        }
    }
}
