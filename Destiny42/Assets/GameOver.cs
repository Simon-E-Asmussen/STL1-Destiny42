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
    public float Health = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //GameOverMenu = GameObject.Find("GameOverMenu");
        GameOverMenu.SetActive(false);
        //Healhbar = GameObject.Find("Healthbar").GetComponent<Slider>();
        Healhbar.value = 1f;
        Health = 1f;
        Debug.Log(Healhbar.value + "Is Cringe");
    }

    private void Update()
    {
        // Checks the players current health
        Health = Healhbar.value;
        // Gives the player a game over panel when health reach 0 or less
        if (Health <= 0)
        {
            Debug.Log(Health);
            GameOverMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Health > 0)
        {
            GameOverMenu.SetActive(false);
        }
    }
    
    // Quits application 
    public void Quit()
    {
        Application.Quit();
    }
    
    // Should respawn the player after death (mainly a placeholder method atm)
    public void Respawn()
    {
        GameOverMenu.SetActive(false);
        player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<Player>().Alive(true);
        player.GetComponent<HealthSync>().health = player.GetComponent<HealthSync>().maxHealth;
        player.SetActive(true);
    }

    [ServerRpc (RequireOwnership = false)]
    public void PlayerReference(GameObject Player)
    {
        player = Player;
    }
}
