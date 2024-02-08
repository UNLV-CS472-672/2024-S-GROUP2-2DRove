using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public float spawnRate;

    void Start(){
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine(){
        yield return new WaitForSeconds(spawnRate);
        Instantiate(enemies[0], transform.position, Quaternion.identity);
        // enemies[0] is the first element in the enemy list
        StartCoroutine(SpawnEnemyRoutine());
    }
}
