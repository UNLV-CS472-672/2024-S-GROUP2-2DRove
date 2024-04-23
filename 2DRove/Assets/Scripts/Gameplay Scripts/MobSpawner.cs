using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyTypes;
    [SerializeField] private float spawnRate;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float minSpawnDistance = 5f;  
    [SerializeField] private float maxSpawnDistance = 15f;
    [SerializeField] private int[] maxPerType; 

    private Dictionary<GameObject, int> currentEnemies = new Dictionary<GameObject, int>();
    private GameObject player;
    private int totalEnemies = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        // Initialize the dictionary to track per type
        foreach (GameObject type in enemyTypes)
        {
            currentEnemies[type] = 0;
        }

        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            if (totalEnemies < maxEnemies)
            {
                SpawnEnemyIfPossible();
            }
        }
    }

    private void SpawnEnemyIfPossible()
    {
        GameObject enemyToSpawn = enemyTypes[Random.Range(0, enemyTypes.Length)];
        int enemyIndex = Array.IndexOf(enemyTypes, enemyToSpawn);
        if (currentEnemies[enemyToSpawn] < maxPerType[enemyIndex])
        {
            Vector3 spawnPosition = GenerateSpawnPosition();
            if (spawnPosition != Vector3.zero) // Ensure we have a valid spawn position
            {
                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                currentEnemies[enemyToSpawn]++;
                totalEnemies++;
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        for (int i = 0; i < 10; i++) // Try multiple times to find a valid position
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, randomDirection, randomDistance, LayerMask.GetMask("Default", "Tilemap")); // Include your Tilemap layer here

            if (hit.collider == null) // If it didn't hit anything, it's a valid spawn position
            {
                return player.transform.position + new Vector3(randomDirection.x, randomDirection.y, 0) * randomDistance;
            }
        }
        return Vector3.zero; // Return zero if a valid position wasn't found after trying
    }


    public void EnemyDied(GameObject enemyType)
    {
        if (currentEnemies.ContainsKey(enemyType))
        {
            currentEnemies[enemyType]--;
            totalEnemies--;
        }
    }
}
