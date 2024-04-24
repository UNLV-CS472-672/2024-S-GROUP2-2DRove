using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class MapGenerationTests4
{
    [UnityTest]
    public IEnumerator ExitTest()
    {
        // Load a scene so loadingScreenManager can actually load something
        SceneManager.LoadScene("Test1", LoadSceneMode.Single);

        // Initialize the GameObjects that we need
        var triggerObject = new GameObject();
        var playerObject = new GameObject("Player");
        var loadingObject = new GameObject("LoadingScreenManager");
        var mapGenDLAObject = new GameObject("Start");
        // Initialize the loading screen manager (this is important in Exit)
        var loadingScreenManager = loadingObject.AddComponent<LoadingScreenManager>();
        // Set the player object to have the tag so that Exit can detect it
        playerObject.tag = "Player";
        // Transform the position to the starting spot
        triggerObject.transform.position = Vector3.zero;
        triggerObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        // Initialize an exit component
        var exit = triggerObject.AddComponent<Exit>();
        // Set our collider in the object to triggered
        var collider = triggerObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        // Add components to the player object so it can trigger the exit
        playerObject.AddComponent<Rigidbody2D>();
        playerObject.AddComponent<PolygonCollider2D>();
        // Move the player's position to the exit object
        playerObject.transform.position = triggerObject.transform.position;

        yield return new WaitForSeconds(0.5f);
    }
}