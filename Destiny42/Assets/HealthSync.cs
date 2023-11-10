using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine.UI;


public class HealthSync : NetworkBehaviour
{
    [SyncVar] public float health = 100.0f;
    private Healthbar healthbar;
    
    // variables from healthbar script
    private Slider healthslider;
    private Slider easeHealthSlider;
    public float maxHealth = 100f;
    private float lerpSpeed = 0.01f;

    private void Start()
    {
        healthslider = GameObject.Find("Healthbar").GetComponent<Slider>();
        easeHealthSlider = GameObject.Find("EaseHealthBar").GetComponent<Slider>();

        SetHealth();
    }

    private void Update()
    {
        if (!base.IsOwner)
            return;
        
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
    
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    [ServerRpc]
    public void SetHealth()
    {
        switch (gameObject.name)
        {
            case "FPSPlayer(Clone)":
                maxHealth = 100.0f;
                healthslider.maxValue = 100.0f;
                easeHealthSlider.maxValue = 100.0f;
                break;
            case "BossPlayer(Clone)":
                maxHealth = 200.0f;
                healthslider.maxValue = 200.0f;
                easeHealthSlider.maxValue = 200.0f;
                break;
        }
        health = maxHealth;
    }
}
