using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public abstract class ABossWeapon : NetworkBehaviour
{
    public int damage;

    //public float maxRange = 20f;
    public float fireRate = 0.5f;

    private float _lastTimeFire;

    public LayerMask weaponHitLayer;
    private Transform _cameraTransform;

    //public GameObject impactEffect;
    public GameObject AOEMarker;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
        //AOEMarker.GetComponent<Collider>().enabled = false;
        AOEMarker.SetActive(false);
    }

    public void Fire()
    {
        if (Time.time < _lastTimeFire + fireRate)
        {
            return;
        }
        
        _lastTimeFire = Time.time;

        
        AOEMarker.GetComponent<AOETrigger>().canDamage = true;
        
        Debug.Log("Uses an attack");
    }

    public abstract void AnimateWeapon();
}
