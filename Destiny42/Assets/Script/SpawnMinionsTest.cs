using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using FishNet;
using FishNet.Object;
using FishNet.Connection;
using UnityEngine;

public class SpawnMinionsTest : NetworkBehaviour
{

    public GameObject min1;

    public GameObject min2;

    private Vector3 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = this.gameObject.transform.position;
        spawnPoint.y++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject go1 = Instantiate(min1,spawnPoint,Quaternion.identity);
            InstanceFinder.ServerManager.Spawn(go1, null);
            GameObject go2 = Instantiate(min2,spawnPoint,Quaternion.identity);
            InstanceFinder.ServerManager.Spawn(go2, null);
        }
    }

  
    
}


