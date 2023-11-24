using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class FIREBALL : NetworkBehaviour
{

    
    Vector3 target;
    bool recieved = false;
    readonly float speed = 10f;
    bool hasGone = false;
    SphereCollider col;
    public GameObject myself;
    int damage = 5;
    float cd;


    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("I started");
        cd = 1.5f;
        //Finds collider and initiates the particle system
        col = GetComponent<SphereCollider>();
        this.GetComponent<ParticleSystem>().Play();
        col.enabled = !col.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if(cd != 0f)
        {
            cd -= Time.deltaTime;
        }else
        {
            col.enabled = col.enabled;
        }
        Debug.LogWarning("I updated");
        //Moves the fireball toward the target and initiates the collider. Collider is disabled by default to prevent it from exploding on launch.
        if (recieved && !hasGone)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            
        }

        if(transform.position == target)
        {
            
            this.transform.localScale = (new Vector3(8, 8, 8));
            hasGone = true;
            Explode();
        }

        


    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Trigger collision");
        Debug.LogWarning("Triggered by" + other);
        if (other.name != "FPSlayer(clone)")
        {
            if (hasGone)
            {


                this.transform.localScale = (new Vector3(8, 8, 8));
                Explode();
            }
            else if (!hasGone)
            {
                hasGone = true;
                target = this.transform.position;
            }

            if (other.GetComponent<ShootableTarget>() != null)
            {
                other.GetComponent<ShootableTarget>().Damage(damage);

            }
        }
    }
    IEnumerator Wait()
    {
       yield return new WaitForEndOfFrame();

    }
    public void SetTarget(Vector3 indicatorPos)
    {
        Debug.LogWarning("Target was set");
        target = indicatorPos;
        recieved = true;
    }

    void Explode()
    {
        Debug.LogWarning("I blew up");
       

        Destroy(this.gameObject);
    }

    
}
