using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class FIREBALL : NetworkBehaviour
{
    Vector3 target;
    bool recieved = false;
    readonly float speed = 50f;
    bool hasGone = false;
    SphereCollider col;
    
    // Start is called before the first frame update
    void Start()
    {
        
        col = GetComponent<SphereCollider>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (recieved && !hasGone)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            col.enabled = true;
        }

        if(transform.position == target)
        {
            hasGone = true;
            Explode();
        }

        


    }
    private void OnTriggerEnter(Collider other)
    {
        if (hasGone)
        {
            StartCoroutine(Wait()) ;
            col.radius = 8;
        }
        else if (!hasGone)
        {
            hasGone = true;
            Explode();
        } 
    }
    IEnumerator Wait()
    {
       yield return new WaitForEndOfFrame();

    }
    public void SetTarget(Vector3 indicatorPos)
    {
        target = indicatorPos;
        recieved = true;
    }

    void Explode()
    {
        hasGone = true;
    }
}
