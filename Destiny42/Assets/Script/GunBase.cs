using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

        fpsCam = Camera.main;

        gunDamage = 1;
        fireRate = 0.25f;
        weaponRange = 50f;
        hitForce = 100f;

    }

    void Update()
    {
        if (base.IsOwner)
        {
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                StartCoroutine(ShotEffect());

                Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

                RaycastHit hit;

                laserLine.SetPosition(0, gunEnd.position);

                if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
                {
                    laserLine.SetPosition(1, hit.point);
                    ShootableTarget health = hit.collider.GetComponent<ShootableTarget>();

                    if (health != null)
                    {
                        health.Damage(gunDamage);
                    }

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitForce);
                    }
                }
            }

            IEnumerator ShotEffect()
            {
                // gunAudio.Play();

                laserLine.enabled = true;

                yield return shotDuration;

                laserLine.enabled = false;
            }
        }
    }
}
    


  

