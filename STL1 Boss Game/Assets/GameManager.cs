using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GeneticAlgorithm geneticAlgorithm;

    void Start()
    {
        geneticAlgorithm = new GeneticAlgorithm();
        geneticAlgorithm.InitializePopulation(5);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            geneticAlgorithm.EvolvePopulation();
        }

        if (!GameObject.Find("Minion(Clone)"))
        {
            geneticAlgorithm.EvolvePopulation();
        }
    }
}
