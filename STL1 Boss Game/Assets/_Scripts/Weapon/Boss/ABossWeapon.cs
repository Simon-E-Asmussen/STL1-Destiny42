using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public abstract class ABossWeapon : NetworkBehaviour
{
    public int damage;

    public float maxRange = 20f;
    public float fireRate = 0.5f;

    private float _lastTimeFire;

    public LayerMask weaponHitLayer;
    private Transform _cameraTransform;

    public GameObject impactEffect;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    public void Fire()
    {
        if (Time.time < _lastTimeFire + fireRate)
        {
            return;
        }
        
        _lastTimeFire = Time.time;
        
        if (!Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, maxRange, weaponHitLayer))
        {
            Debug.Log("Hit Nothing");
            return;
        }
        Debug.Log("Hit Something");

        Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        if (hit.transform.TryGetComponent(out PlayerHealth health))
        {
            health.TakeDamage(damage);
        }
    }

    public abstract void AnimateWeapon();
}
