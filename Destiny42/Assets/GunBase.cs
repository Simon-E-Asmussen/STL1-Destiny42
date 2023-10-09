using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class GunBase : NetworkBehaviour
{
    public int gunDamage;

    public float fireRate;

    public float weaponRange;

    public float hitForce;

    public Transform gunEnd;





    private Camera fpsCam;

    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);

    private AudioSource gunAudio;

    private LineRenderer laserLine;

    private float nextFire;



    void Start()

    {

        laserLine = GetComponentInChildren<LineRenderer>();

        gunAudio = GetComponent<AudioSource>();

        fpsCam = GetComponent<Camera>();

    }

    void Update()

    {

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

           // *Not implemented* StartCoroutine(ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        }

    }

}

