using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine.UI;


public class HealthSync : NetworkBehaviour
{
    [SyncVar] public float health;
    private Healthbar healthbar;
    
    // variables from healthbar script
    public Slider healthslider;
    public Slider easeHealthSlider;
    //public float maxHealth = 100f;
    //public float health;
    private float lerpSpeed = 0.01f;

    private void Start()
    {
        healthbar = GameObject.Find("Health Bar").GetComponent<Healthbar>();
        switch (gameObject.name)
        {
            case "FPSPlayer(Clone)":
                healthbar.maxHealth = 100.0f;
                break;
            case "BossPlayer(Clone)":
                healthbar.maxHealth = 1000.0f;
                break;
        }
        healthbar.health = healthbar.maxHealth;
        
        if (healthslider.value != health)
        {
            healthslider.value = health;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
        
        if (healthslider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }
    }

    private void Update()
    {
        if (!base.IsOwner)
            return;
        
        
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
