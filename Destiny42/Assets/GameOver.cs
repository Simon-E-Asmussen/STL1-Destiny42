using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : NetworkBehaviour
{
    
    [SerializeField] private GameObject GameOverPanel;
    public GameObject playerHealth;
    public float Health;
    
    // Start is called before the first frame update
    void Start()
    {
        GameOverPanel = GameObject.Find("Game Over Panel");
        GameOverPanel.SetActive(false);
    }

    private void Update()
    {
        // Checks the players current health
        Health = playerHealth.GetComponent<Healthbar>().health;
        // Gives the player a game over panel when health reach 0 or less
        if (Health <= 0)
        {
            GameOverPanel.SetActive(true);
        }

        // These two if statements are placeholders for when we can use the cursor when game over
        if (Health <= 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
        if (Health <= 0 && Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }
    
    // Quits application 
    public void Quit()
    {
        Application.Quit();
    }
    
    // Should respawn the player after death (mainly a placeholder method atm
    public void Respawn()
    {
        GameOverPanel.SetActive(false);
    }
}
