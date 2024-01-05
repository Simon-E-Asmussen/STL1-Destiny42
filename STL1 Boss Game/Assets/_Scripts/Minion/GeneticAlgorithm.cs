using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour
{
    
    public string minionPrefabPath = "Minion";
    public List<Minion> population;

    public void InitializePopulation(int populationSize)
    {
        population = new List<Minion>();

        for (int i = 0; i < populationSize; i++)
        {
            // Load Minion prefab from Resources folder
            GameObject minionObject = InstantiateMinionPrefab(minionPrefabPath);

            // Get Minion Script from the instantiated object
            Minion minion = minionObject.GetComponent<Minion>();

            // Initialize minion attributes randomly
            minion.Initialize(Random.Range(10, 30), Random.Range(10, 30), 
                Random.Range(4f, 6f), Random.Range(3f, 6f));

            // Add the instantiated Minion to the population
            population.Add(minion);
        }
    }
    
    private GameObject InstantiateMinionPrefab(string path)
    {
        // Load Minion prefab from Resources folder
        GameObject minionPrefab = Resources.Load<GameObject>(path);

        if (minionPrefab == null)
        {
            Debug.LogError("Minion prefab not found at path: " + path);
            return null;
        }

        // Instantiate Minion prefab
        GameObject minionObject = Instantiate(minionPrefab, RandomSpawnPosition(), Quaternion.identity);
        
        minionObject.SetActive(true);

        return minionObject;
    }
    
    private Vector3 RandomSpawnPosition()
    {
        float spawnRange = 5f;
        return new Vector3(Random.Range(-spawnRange, spawnRange), 2.6f, Random.Range(-spawnRange, spawnRange));
    }

    public void EvolvePopulation()
    {
        List<Minion> newPopulation = new List<Minion>();

        // Choose individuals from the current population based on fitness
        List<Minion> selectedMinions = SelectBestFitMinions(population);

        // Combines attributes of selected individuals to create new minions
        for (int i = 0; i < population.Count; i++)
        {
            Minion parent1 = selectedMinions[Random.Range(0, selectedMinions.Count)];
            Minion parent2 = selectedMinions[Random.Range(0, selectedMinions.Count)];

            Minion child = Crossover(parent1, parent2);

            newPopulation.Add(child);
        }

        // Random changes in attributes of some minions
        for (int i = 0; i < newPopulation.Count; i++)
        {
            Mutate(newPopulation[i]);
            Debug.Log("Mutates the children");
        }

        // Update the population with the new population
        population = newPopulation;
        Debug.Log("Mutated population: " + population);
    }

    private List<Minion> SelectBestFitMinions(List<Minion> minions)
    {
        // Sort minions based on a custom fitness score
        minions.Sort((minion1, minion2) =>
        {
            float score1 = CalculateMinionFitness(minion1);
            float score2 = CalculateMinionFitness(minion2);

            // Sort in descending order
            return score2.CompareTo(score1);
        });

        // Select the two best-fit minions
        List<Minion> bestFitMinions = new List<Minion>();
        if (minions.Count > 0)
        {
            bestFitMinions.Add(minions[0]);

            if (minions.Count > 1)
            {
                bestFitMinions.Add(minions[1]);
            }
        }

        return bestFitMinions;
    }
    
    private float CalculateMinionFitness(Minion minion)
    {
        // Weights
        float healthWeight = 0.1f;
        float damageWeight = 1f;
        float timeWeight = 0.3f;

        // Calculates the fitness score
        float fitness = (healthWeight * minion.healthLost) + (damageWeight * minion.damageDone) + (timeWeight * minion.timeSurvived);
        
        return fitness;
    }

    public Minion Crossover(Minion parent1, Minion parent2)
    {
        // Create a new child minion
        Minion child = CreateChildMinion(parent1, parent2);
        return child;
    }

    private Minion CreateChildMinion(Minion parent1, Minion parent2)
    {
        // Compute child values
        int childHealth = (parent1.startingHealth + parent2.startingHealth) / 2;
        int childDamage = (parent1.damage + parent2.damage) / 2;
        float childMovement = (parent1.movementSpeed + parent2.movementSpeed) / 2f;
        float childRotation = (parent1.rotationSpeed + parent2.rotationSpeed) / 2f;

        // Instantiate and initialize the child minion
        GameObject childObject = InstantiateMinionPrefab("Minion");
        Minion child = childObject.GetComponent<Minion>();
        child.Initialize(childHealth, childDamage, childMovement, childRotation);
        Debug.Log("Instantiate and initialize the child minion");

        return child;
    }

    private void Mutate(Minion minion)
    {
        // Logic for mutating health
        if (minion.health <= 20)
        {
            minion.health += Random.Range(0, 2);
        }
        else
        {
            minion.health += Random.Range(-2, 2);
        }
        
        // Logic for mutating damage
        if (minion.damage <= 20)
        {
            minion.damage += Random.Range(0, 2);
        }
        else
        {
            minion.damage += Random.Range(-2, 2);
        }
        
        // Logic for mutating movement speed
        if (minion.movementSpeed <= 2)
        {
            minion.movementSpeed += Random.Range(0, 1f);
        }
        else if (minion.movementSpeed >= 11)
        {
            minion.movementSpeed += Random.Range(-2f, 0.5f);
        }
        else
        {
            minion.movementSpeed += Random.Range(-1f, 2f);
        }
        
        // Logic for mutating minion rotation speed
        if (minion.rotationSpeed <= 3)
        {
            minion.rotationSpeed += Random.Range(0, 0.5f);
        }
        else
        {
            minion.rotationSpeed += Random.Range(-0.5f, 0.5f);
        }
    }
}
