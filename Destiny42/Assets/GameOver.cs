using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : NetworkBehaviour
{
    public GameObject player;
    public GameObject GameOverMenu;
    
    public Slider Healhbar;

    private void Awake()
    {
        Healhbar.value = 100f;
    }

    void Start()
    {
        GameOverMenu.SetActive(false);
    }

    private void Update()
    {
        // Gives the player a game over panel when health reach 0 or less
        
        if (Healhbar.value <= 0) 
        { 
            GameOverMenu.SetActive(true); 
            Cursor.visible = true; 
            Cursor.lockState = CursorLockMode.None;
        }
        
        if (Healhbar.value > 0)
        {
            //ve Debug.Log("Alive!");
            GameOverMenu.SetActive(false);
        }
    }
    
    // Quits application 
    public void Quit()
    {
        Application.Quit();
    }
    
    // Respawns the player when the Respawn button is pressed
    // [ServerRpc (RequireOwnership = false)]
    public void Respawn()
    {
        //GameOverMenu.SetActive(false);
        player.SetActive(true);
        player.GetComponent<HealthSync>().health = player.GetComponent<HealthSync>().maxHealth;
        player.GetComponent<Player>().Alive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //player.SetActive(true);
    }

    // Is used to get a reference to the player e.g. used in in player script to set the player variable to a specific player
    // [ServerRpc (RequireOwnership = false)]
    public void PlayerReference(GameObject Player)
    {
        player = Player;
    }
}
