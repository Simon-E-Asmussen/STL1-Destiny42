using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class ShootableTarget : NetworkBehaviour
{
    //The box's current health point total

    public int currentHealth = 3;

    public void Damage(int damageAmount)

    {

        //subtract damage amount when Damage function is called

        currentHealth -= damageAmount;

        //Check if health has fallen below zero

        if (currentHealth <= 0)

        {

            //if health has fallen below zero, deactivate it 

            gameObject.SetActive(false);

        }

    }

}

