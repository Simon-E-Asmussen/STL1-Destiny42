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
    public static GameObject[] players;
    public static bool strike = false;
    public static GameObject minion;
 


    // Start is called before the first frame update
    public void Start()
    {
        players = (GameObject[])GameObject.FindGameObjectsWithTag("FPSplayer");
    }

  

    public void Move(Vector3 target)
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public void Attack()
    {

        foreach (GameObject play in players)
        {
            float distance = Vector3.Distance(minion.transform.position, play.transform.position);

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
    
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("FPSplayer");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    public abstract void MinionAttack();
}

   
    
    



