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


    private void Awake()
    {
        Debug.LogWarning("I woke up");
        this.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("I started");
        col = GetComponent<SphereCollider>();
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogWarning("I updated");
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
        Debug.LogWarning("Trigger collision");
        if (hasGone)
        {
            //StartCoroutine(Wait()) ;
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
        Debug.LogWarning("Target was set");
        target = indicatorPos;
        recieved = true;
    }

    void Explode()
    {
        Debug.LogWarning("I blew up");
        hasGone = true;
    }
}
