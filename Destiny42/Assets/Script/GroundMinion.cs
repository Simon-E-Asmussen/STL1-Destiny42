using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMinion : Minion
{
    public BoxCollider Hurtbox;
    public Material mat1;
    public Material mat2;
    MeshRenderer mesh;
    float cooldown = 3f;
    float timestamp;
    // Start is called before the first frame update
    new void Start()
    {
        mesh = this.GetComponent<MeshRenderer>();
        mesh.material = mat1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (strike)
        {
            timestamp = Time.time + cooldown;
        }

        if(timestamp <= Time.time)
        {
            Attack();
        }
        
    }

    new void MinionAttack()
    {
        Hurtbox.enabled = true;
        mesh.material = mat2;
        StartCoroutine(Wait());
        Hurtbox.enabled = false;
        mesh.material = mat1;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.2f);
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("FPSplayer"))
        {
            //apply damage
        }
    }
}
