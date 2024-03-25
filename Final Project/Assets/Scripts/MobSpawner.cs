using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public string platformTag = "Platforms";
    public Transform player;
    public float spawnInterval = 2f;
    public float spawnRadius = 2f;
    private bool canSpawn = true;



    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (canSpawn)
        {
            // Wait for spawn interval
            yield return new WaitForSeconds(spawnInterval);

            // Get a random platform position
            Vector2 spawnPosition = GetRandomPlatformPosition();

            // Randomly select an enemy prefab
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Spawn the enemy
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Set the target of EnemyAI script to the player's position
            EnemyAI enemyAI = spawnedEnemy.GetComponent<EnemyAI>();
            if (enemyAI != null && player != null)
            {
                enemyAI.target = player;
            }
        }
    }

    Vector2 GetRandomPlatformPosition()
    {
        // Find all game objects with the specified platform tag
        GameObject[] platforms = GameObject.FindGameObjectsWithTag(platformTag);

        if (platforms.Length == 0)
        {
            Debug.LogWarning("No platforms found with tag: " + platformTag);
            return Vector2.zero;
        }

        // Choose a random platform
        GameObject platform = platforms[Random.Range(0, platforms.Length)];

        // Get the platform's bounds
        Collider2D platformCollider = platform.GetComponent<Collider2D>();
        if (platformCollider == null)
        {
            Debug.LogWarning("Platform " + platform.name + " does not have a Collider2D component.");
            return Vector2.zero;
        }

        // Get a random position within the platform bounds
        float minX = platform.transform.position.x - platformCollider.bounds.size.x / 2f;
        float maxX = platform.transform.position.x + platformCollider.bounds.size.x / 2f;
        float yPos = platform.transform.position.y + platformCollider.bounds.size.y / 2f; // Spawn on top of the platform
        float randomX = Random.Range(minX, maxX);

        return new Vector2(randomX, yPos);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }
}
