using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Camera fpsCam;
    private bool primed;
    private float abilityRange;
    GameObject indicator;

    // Start is called before the first frame update
    void Start()
    {
        fpsCam = Camera.main;
        primed = false;
        abilityRange = 30f;
        UnityEngine.Object indPrefab = Resources.Load("Prefabs/GrenadeIndicator");
        indicator = (GameObject)indPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for ability activation
        if (Input.GetButtonDown("Fire2"))
        {
            NoobLaunch();

        }
    }

    void NoobLaunch()
    {
        StartCoroutine(wait());
        Debug.Log("Click");
        //Sets parameter for ability to load 3D indicator
        primed = true;
        GameObject.Instantiate(indicator, Vector3.zero, Quaternion.identity);
        
        //Insert public bool on owner playerscript that blocks gunfire
        while (primed == true)
        {
            Debug.Log("Entered Loop");

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;
            

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, abilityRange))
            {
                Debug.Log("Am doing");
                GameObject six = GameObject.Find("GrenadeIndicator(Clone)");
                six.GetComponent<reposIndicator>().ReposIndicator(hit.transform);
                Debug.Log("Did do");
            }

            //Registers final activation of ability and disables 3D indicators when activating the ability
            if (Input.GetButtonDown("Fire1"))
            {
                //Activates ability, sending hit transform to network object and instantiates

                //Deactivates indicators
                primed = false;
                //Deactivate fire blocker bool
            }
            else if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Exiting");
                //Registers deactivation of ability and returns to standard state

                //Deactivate fireblocker bool

                GameObject six = GameObject.Find("GrenadeIndicator(Clone)");
                six.GetComponent<reposIndicator>().SweetRelease();
                Debug.Log("kill");
                primed = false;
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        yield return null;

    }
}
    
