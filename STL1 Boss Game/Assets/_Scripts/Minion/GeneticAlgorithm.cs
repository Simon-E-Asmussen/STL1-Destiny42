using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour
{
    
    public string minionPrefabPath = "Minion"; // Example path
    public List<Minion> population;

    public void InitializePopulation(int populationSize)
    {
        population = new List<Minion>();

        for (int i = 0; i < populationSize; i++)
        {
            // Load Minion prefab from Resources folder
            GameObject minionObject = InstantiateMinionPrefab(minionPrefabPath);

            // Get Minion component from the instantiated object
            Minion minion = minionObject.GetComponent<Minion>();

            // Initialize minion attributes randomly or using some logic
            minion.Initialize(Random.Range(10, 30), Random.Range(10, 30), Random.Range(4f, 6f), Random.Range(3f, 6f));

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

        // Ensure the spawned Minion is active
        minionObject.SetActive(true);

        return minionObject;
    }
    
    private Vector3 RandomSpawnPosition()
    {
        // Spawn close to 0,0,0 within a specified range
        float spawnRange = 5f;
        return new Vector3(Random.Range(-spawnRange, spawnRange), 2.6f, Random.Range(-spawnRange, spawnRange));
    }

    public void EvolvePopulation()
    {
        List<Minion> newPopulation = new List<Minion>();

        // Selection: Choose individuals from the current population based on fitness
        List<Minion> selectedMinions = SelectBestFitMinions(population);

        // Crossover: Combine attributes of selected individuals to create new minions
        for (int i = 0; i < population.Count; i++)
        {
            Minion parent1 = selectedMinions[Random.Range(0, selectedMinions.Count)];
            Minion parent2 = selectedMinions[Random.Range(0, selectedMinions.Count)];

            Minion child = Crossover(parent1, parent2);

            newPopulation.Add(child);
        }

        // Mutation: Introduce random changes in attributes of some minions
        for (int i = 0; i < newPopulation.Count; i++)
        {
            Mutate(newPopulation[i]);
            Debug.Log("Mutates the children");
        }

        // Update the population with the new one
        population = newPopulation;
        Debug.Log("Mutated population: " + population);
    }

    private List<Minion> SelectBestFitMinions(List<Minion> minions)
    {
        // Sort minions based on a custom fitness score (health + damage dealt + survival time)
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
        // Customize this formula based on your fitness criteria
        // For example, fitness = remaining health + damage dealt + survival time
        float fitness = minion.healthLost + minion.damageDone + (Time.time - minion.timeSurvived);

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
        // Compute child values (for example, taking the average)
        int childHealth = (parent1.health + parent2.health) / 2;
        int childDamage = (parent1.damage + parent2.damage) / 2;
        float childMovement = (parent1.movementSpeed + parent2.movementSpeed) / 2f;
        float childRotation = (parent1.rotationSpeed + parent2.rotationSpeed) / 2f;

        // Instantiate and initialize the child minion
        GameObject childObject = InstantiateMinionPrefab("Minion"); // Adjust the prefab path
        Minion child = childObject.GetComponent<Minion>();
        child.Initialize(childHealth, childDamage, childMovement, childRotation);
        Debug.Log("Instantiate and initialize the child minion");

        return child;
    }

    private void Mutate(Minion minion)
    {
        // Implement your mutation logic here
        // This could involve randomly changing some attributes of the minion
        // For simplicity, just adding/subtracting a small random value in this example
        minion.health += Random.Range(-2, 2);
        minion.damage += Random.Range(-2, 2);
        minion.movementSpeed += Random.Range(-0.5f, 1f);
        minion.rotationSpeed += Random.Range(-0.5f, 0.5f);
    }
    
}
