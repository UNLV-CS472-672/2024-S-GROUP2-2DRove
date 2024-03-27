using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;
using UnityEngine.SceneManagement;

public class GamePlayTests
{

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator SpawnEnemyTest()
    {
        // Basic enemy template
        var enemy = new GameObject();
        // Component that spawns enemies
        var enemySpawner = new GameObject();
        var spawnEnemies = enemySpawner.AddComponent<SpawnEnemy>();
        // Get the private serialized field and set it
        var fieldInfo = typeof(SpawnEnemy).GetField("enemy", BindingFlags.NonPublic | BindingFlags.Instance);
        // Set field to our basic enemy GameObject
        fieldInfo?.SetValue(spawnEnemies, enemy);

        Assert.IsNotNull(spawnEnemies);
        // Wait for spawner to finish 
        yield return new WaitForSeconds(0.5f);
    }

    [UnityTest]
    public IEnumerator CodeCoverageTest()
    {
        SceneManager.LoadScene("Scenes/Level_00");

        yield return new WaitForSeconds(30);
    }
}
