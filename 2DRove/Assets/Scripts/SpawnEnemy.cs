using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnXRange;
    [SerializeField] private float spawnYRange;

    void Start(){ 
        StartCoroutine(SpawnEnemyRoutine());
    }

    //Repeatedly spawns the enemy specified in the enemy variable
    private IEnumerator SpawnEnemyRoutine(){
        yield return new WaitForSeconds(spawnRate); //Delay based on spawnRate
        Instantiate(enemy, generateSpawnPosition(), Quaternion.identity); //Spawns the enemy in the enemy variable
        StartCoroutine(SpawnEnemyRoutine()); //Reruns the function
    }

    //Randomizes the position the enemy spawns by modifying the spawner's position
    private Vector2 generateSpawnPosition(){
        return new Vector2(Random.Range(-spawnXRange, spawnXRange), Random.Range(-spawnYRange, spawnYRange)) + (Vector2)transform.position;
    }
}
