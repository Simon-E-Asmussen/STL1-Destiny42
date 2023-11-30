using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoints : MonoBehaviour
{
    public static Spawnpoints instance;
    
    private void Awake()
    {
        instance = this;
    }
}
