using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour
{
    
    public List<Minion> population;

    public void InitializePopulation(int populationSize)
    {
        population = new List<Minion>();

        for (int i = 0; i < populationSize; i++)
        {
            Minion minion = new Minion();
            minion.Initialize(Random.Range(50f, 100f), Random.Range(10, 20));
            population.Add(minion);
        }
        Debug.Log(population);
    }

    public void EvolvePopulation()
    {
        List<Minion> newPopulation = new List<Minion>();

        // Selection: Choose individuals from the current population based on fitness
        List<Minion> selectedMinions = SelectMinions();

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
        }

        // Update the population with the new one
        population = newPopulation;
        Debug.Log(population);
    }

    private List<Minion> SelectMinions()
    {
        // Implement your selection logic here
        // This could be based on fitness, tournament selection, etc.
        // For simplicity, just returning the entire population in this example
        return new List<Minion>(population);
    }

    private Minion Crossover(Minion parent1, Minion parent2)
    {
        // Implement your crossover logic here
        // This could involve mixing attributes of parent1 and parent2
        // For simplicity, just creating a child with random attributes in this example
        Minion child = new Minion();
        child.Initialize(Random.Range(50f, 100f), Random.Range(10, 20));
        return child;
    }

    private void Mutate(Minion minion)
    {
        // Implement your mutation logic here
        // This could involve randomly changing some attributes of the minion
        // For simplicity, just adding/subtracting a small random value in this example
        minion.health += Random.Range(-5f, 5f);
        minion.damage += Random.Range(-2, 2);
    }
    
}
