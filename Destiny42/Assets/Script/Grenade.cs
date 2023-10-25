using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Camera fpsCam;
    private bool primed;
    private float abilityRange;

    // Start is called before the first frame update
    void Start()
    {
        fpsCam = Camera.main;
        primed = false;
        abilityRange = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for ability activation
        if (Input.GetButtonDown("Fire2"))
        {
            //Sets parameter for ability to load 3D indicator
            primed = true;
            //Insert public bool on owner playerscript that blocks gunfire
            while (primed == true)
            {

                Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

                RaycastHit hit;
                
                //Registers final activation of ability and disables 3D indicators when activating the ability
                if (Input.GetButtonDown("Fire1"))
                {
                    //Activates ability, sending hit transform to network object and instantiates

                    //Deactivates indicators
                    primed = false;
                    //Deactivate fire blocker bool
                } else if (Input.GetButtonDown("Escape") | Input.GetButtonDown("Fire2"))
                {
                    //Registers deactivation of ability and returns to standard state
                    primed = false;
                    //Deactivate fireblocker bool
                }
            }
        }
    }
}
