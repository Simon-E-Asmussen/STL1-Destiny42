using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public abstract class Minion : NetworkBehaviour
{
    public int hp;
    public float speed;
    public float weight;
    public int dmg;
    public float firerate;
    public GameObject[] players;
    public bool strike = false;


    // Start is called before the first frame update
    public void Start()
    {
        players = (GameObject[])GameObject.FindGameObjectsWithTag("FPSplayer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 target)
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public void Attack()
    {
        
        foreach(GameObject play in players)
        {
            float distance = Vector3.Distance(this.transform.position, play.transform.position);

            if (distance <= 1.2f)
            {
                strike = true;
            }

            if (strike)
            {
                strike = false;
                MinionAttack();
                return;
            }
        }
    }

    public void MinionAttack()
    {

    }
}
