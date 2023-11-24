using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class GroundMinion : Minion
{
    public BoxCollider hurtbox;
    public Material mat1;
    public Material mat2;
    MeshRenderer mesh;
    float cooldown = 3f;
    float timestamp1;
    float timestamp2;

    Vector3 Target;
    
    // Start is called before the first frame update
    void Awake()
    {
        mesh = this.GetComponent<MeshRenderer>();
        mesh.material = mat1;
        Minion.minion = this.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (strike)
        {
            timestamp1 = Time.time + cooldown;
        }

        if(timestamp1 <= Time.time)
        {
            Attack();
        }

        if (timestamp2 <= Time.time) 
        {
            Target = FindClosestEnemy().transform.position;
            timestamp2 = Time.time + 5;
            Debug.LogWarning("Choosing a target");
        }
        
        Move(Target);
       
    }

    public override void MinionAttack()
    {
        hurtbox.enabled = true;
        mesh.material = mat2;
        StartCoroutine(Wait());
        hurtbox.enabled = false;
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
