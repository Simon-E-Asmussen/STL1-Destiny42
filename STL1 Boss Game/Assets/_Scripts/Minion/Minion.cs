using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    // Minion attributes
    public float health;
    public int damage;

    // Array to store all player objects
    private GameObject[] players;

    // Speed at which the minion moves towards the player
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;
    
    public float damageCooldown = 1f; // Time between each damage instance
    private float lastDamageTime; // Time when the last damage was dealt
    
    // stats
    public int damageDone;
    
    void Start()
    {
        // Find all player objects in the scene
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {
            Debug.LogError("No players found! Make sure players have the appropriate tag.");
        }
    }

    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {
            Debug.LogError("No players found! Make sure players have the appropriate tag.");
        }
        // Move towards the closest player
        MoveTowardsClosestPlayer();
    }
    
    void OnTriggerStay(Collider other)
    {
        // Check if the collider is the player and if enough time has passed since the last damage
        if (other.CompareTag("Player") && Time.time - lastDamageTime > damageCooldown)
        {
            Debug.Log("found player tagged object");
            // Deal damage to the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                damageDone += damage;
                lastDamageTime = Time.time;
                Debug.Log(this + " has done " + damageDone + " Damage in total to the player");
            }
        }
    }

    // Method to initialize the minion with specific attributes
    public void Initialize(float initialHealth, int initialDamage)
    {
        health = initialHealth;
        damage = initialDamage;
        // Add other attribute initializations
    }

    // Method to make the minion move towards the closest player
    private void MoveTowardsClosestPlayer()
    {
        if (players.Length > 0)
        {
            Transform closestPlayer = FindClosestPlayer();

            if (closestPlayer != null)
            {
                Vector3 direction = closestPlayer.position - transform.position;

                // Lock vertical movement by setting the Y component to zero
                direction.y = 0;

                // Normalize the direction vector
                direction.Normalize();

                // Rotate towards the closest player (optional)
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);

                // Move towards the closest player using Transform.position
                transform.position += direction * movementSpeed * Time.deltaTime;
            }
        }
    }

    // Method to find the closest player
    private Transform FindClosestPlayer()
    {
        Transform closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (var player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.transform;
            }
        }

        return closestPlayer;
    }
}
