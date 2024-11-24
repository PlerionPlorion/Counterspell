using UnityEngine;

public class PuddleScript : MonoBehaviour
{
    public GameObject reflectionEnemyPrefab; // Reference to the enemy prefab
    public Transform spawnPoint; // Optional: specific spawn location

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the puddle
        if (other.CompareTag("Player"))
        {
            SpawnReflectionEnemy();
        }
    }

    private void SpawnReflectionEnemy()
    {
        if (reflectionEnemyPrefab != null)
        {
            // Determine the spawn position
            Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position;

            // Spawn the enemy
            Instantiate(reflectionEnemyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Reflection enemy prefab is not assigned!");
        }
    }
}
