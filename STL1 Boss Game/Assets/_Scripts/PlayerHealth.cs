using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int _currentHealth;

    private void Awake()
    {
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
        _currentHealth -= damage;

        Debug.Log($"New Player Health: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log("Player Died!");
    }
}
