using UnityEngine;

public class PuddleScript : MonoBehaviour
{
    public GameObject reflectionEnemyPrefab; // Reference to the enemy prefab
    public float spawnOffsetY = 2f; // Distance above or below the puddle to spawn
    private bool hasSpawned = false; // Flag to track if the enemy has been spawned

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the puddle and no enemy has been spawned yet
        if (!hasSpawned && other.CompareTag("Player"))
        {
            SpawnReflectionEnemy();
        }
    }

    private void SpawnReflectionEnemy()
    {
        if (reflectionEnemyPrefab != null)
        {
            // Set the flag to true to prevent additional spawns
            hasSpawned = true;

            // Calculate the spawn position
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + spawnOffsetY, transform.position.z);

            // Spawn the enemy
            Instantiate(reflectionEnemyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Reflection enemy prefab is not assigned!");
        }
    }
}
