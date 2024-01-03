using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] private int maxHealth;

    private int _currentHealth;
    
    [SerializeField] private Slider healthslider;
    [SerializeField] private Slider easeHealthSlider;

    private void Awake()
    {
        SetHealth();
        _currentHealth = maxHealth;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner)
        {
            enabled = false;
            return;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void TakeDamage(int damage)
    {
        Debug.Log($"Old Player Health: {_currentHealth}");
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        
        Debug.Log($"New Player Health: {_currentHealth}");
        
        LocalTakeDamage(Owner, _currentHealth);
    }
    
    private void Die()
    {
        Debug.Log("Player Died!");
    }

    [TargetRpc]
    private void LocalTakeDamage(NetworkConnection conn, int newHealth)
    {
        GameUIManager.SetHealthText(newHealth, healthslider, easeHealthSlider);
    }
    
    public void SetHealth()
    {
        //healthslider = GameObject.Find("Healthbar").GetComponentInChildren<Slider>();
        //easeHealthSlider = GameObject.Find("EaseHealthBar").GetComponentInChildren<Slider>();
        switch (gameObject.name)
        {
            case "Player(Clone)":
                maxHealth = 100;
                healthslider.maxValue = 100.0f;
                easeHealthSlider.maxValue = 100.0f;
                break;
            case "Boss(Clone)":
                maxHealth = 1000;
                healthslider.maxValue = 1000.0f;
                easeHealthSlider.maxValue = 1000.0f;
                break;
            default:
                maxHealth = 100;
                break;
        }
        healthslider.value = maxHealth;
        easeHealthSlider.value = maxHealth;
    }
}
