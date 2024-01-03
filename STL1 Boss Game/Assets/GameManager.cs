using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GeneticAlgorithm geneticAlgorithm;

    void Start()
    {
        geneticAlgorithm = new GeneticAlgorithm();
        geneticAlgorithm.InitializePopulation(5); // Adjust population size as needed
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            geneticAlgorithm.EvolvePopulation();
            // Update the game with the evolved population
        }
    }
}
