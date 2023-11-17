using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMinion : Minion
{
    public BoxCollider Hurtbox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MinionAttack()
    {
        Hurtbox.enabled = true;
        StartCoroutine(Wait());
        Hurtbox.enabled = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("FPSplayer"))
        {
            //apply damage
        }
    }
}
