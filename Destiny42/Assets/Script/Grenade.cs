using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Camera fpsCam;
    private bool primed;
    private float abilityRange;
    GameObject indicator;
    GameObject fireball;
    bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        fpsCam = Camera.main;
        primed = false;
        abilityRange = 30f;
        UnityEngine.Object indPrefab = Resources.Load("Prefabs/GrenadeIndicator");
        UnityEngine.Object ballPrefab = Resources.Load("Prefabs/FireBallShot");
        indicator = (GameObject)indPrefab;
        fireball = (GameObject)ballPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for ability activation
        if (!active) {
            if (Input.GetButtonDown("Fire2"))
            {
                active = true;

            } }

        if (active)
        {
            NoobLaunch();
        }
    }

    void NoobLaunch()
    {
        if (!primed) {
            StartCoroutine(wait());
            Debug.Log("Click");
            //Sets parameter for ability to load 3D indicator
            primed = true;
            GameObject.Instantiate(indicator, Vector3.zero, Quaternion.identity, this.transform);
        }
        //Insert public bool on owner playerscript that blocks gunfire
        if (primed)
        {
            Debug.Log("Entered Loop");

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;
            

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, abilityRange))
            {
                Debug.Log("Am doing");
                
                GetComponentInChildren<reposIndicator>().ReposIndicator(hit.point);
                Debug.Log("Did do");
            }

            //Registers final activation of ability and disables 3D indicators when activating the ability
            if (Input.GetButtonDown("Fire1"))
            {
                //Activates ability, sending hit transform to network object and instantiates
                Debug.Log("Went her");
                //Deactivates indicators
                GameObject.Instantiate(fireball, new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z + 1), Quaternion.identity);
                GetComponentInChildren<reposIndicator>().SweetRelease();
                Debug.Log("Activate and Kill");
                GameObject.Find("FireBallShot(clone)").GetComponent<FIREBALL>().SetTarget(hit.point);
                primed = false;
                active = false;

                //Deactivate fire blocker bool
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                Debug.Log("Exiting");
                //Registers deactivation of ability and returns to standard state

                //Deactivate fireblocker bool

                
                GetComponentInChildren<reposIndicator>().SweetRelease();
                Debug.Log("kill");
                primed = false;
                active = false;
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        yield return null;

    }
}
    
