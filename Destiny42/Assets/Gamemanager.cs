using System.Collections;
using System.Collections.Generic;
using FishNet.Component.Spawning;
using FishNet.Object;
using UnityEngine;

public class Gamemanager : NetworkBehaviour
{

    private PlayerSpawner _playerSpawner;
    private GameObject CharacterSelect;

    public bool IsBoss = false;
    public NetworkObject player;
    public NetworkObject boss;

    // Start is called before the first frame update
    void Start()
    {
        _playerSpawner = GameObject.Find("NetworkManager").GetComponent<PlayerSpawner>();
        
        //Character Select UI
        CharacterSelect = GameObject.FindWithTag("CharacterSelect");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (IsClient)
        {
            SelectPlayerType();
        }
        */
    }

    private void SelectPlayerType()
    {
        if (IsBoss)
        {
            
        }
        else
        {
            
        }
    }

    // Runs when boss button is clicked
    public void SetBossTrue()
    {
        IsBoss = true; //Sets bool to true which change the Main Camera Height
        _playerSpawner.ChangePlayerPrefab(boss); //Sets the spawnable player prefab to the boss prefab
        CharacterSelect.SetActive(false); //Removes the character select textbox
    }
    
    // Runs when player button is clicked
    public void SetBossFalse()
    {
        IsBoss = false; //Sets bool to true which change the Main Camera Height
        _playerSpawner.ChangePlayerPrefab(player); //Sets the spawnable player prefab to the player prefab
        CharacterSelect.SetActive(false); //Removes the character select textbox
    }
}
