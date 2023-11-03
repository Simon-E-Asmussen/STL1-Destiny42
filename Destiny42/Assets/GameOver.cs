using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject player;
    public GameObject GameOverMenu;
    public GameObject Healhbar;
    public float Health = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //GameOverMenu = GameObject.Find("GameOverMenu");
        GameOverMenu.SetActive(false);
    }

    private void Update()
    {
        // Checks the players current health
        Health = Healhbar.GetComponent<Healthbar>().health;
        // Gives the player a game over panel when health reach 0 or less
        if (Health <= 0)
        {
            GameOverMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
        Healhbar.GetComponent<Healthbar>().health = Healhbar.GetComponent<Healthbar>().maxHealth;
        player.GetComponent<Player>().Alive(true);
    }

    public void PlayerReference(GameObject Player)
    {
        player = Player;
    }
}
