using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PlayerShoot : NetworkBehaviour
{

    public float damage = 10.0f;

    public float timeBetweenFire;
    private float fireTimer;

    private void Update()
    {
        if (!base.IsOwner)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            if (fireTimer <= 0)
            {
                ShootServer(damage, Camera.main.transform.position, Camera.main.transform.forward);
                fireTimer = timeBetweenFire;
            }
        }

        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
    }

    [ServerRpc (RequireOwnership = false)]
    private void ShootServer(float damageToGive, Vector3 position, Vector3 direction)
    {
        if (Physics.Raycast(position, direction, out RaycastHit hit) && hit.transform.TryGetComponent(out HealthSync enemyHealth))
        {
            enemyHealth.health -= damageToGive;
        }
    }
}
