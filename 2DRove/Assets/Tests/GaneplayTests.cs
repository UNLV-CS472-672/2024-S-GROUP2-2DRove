using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class GameplayTests
{
    // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator LoadProperMap()
    // {
    //     // Load a proper scene that is like a scene we would load normally
    //     SceneManager.LoadScene("Test1", LoadSceneMode.Single);
    //     // Yield next frame to scene
    //     yield return null;

    // }

    // [UnityTest]
    // public IEnumerator LoadNotEnoughPotentialTiles()
    // {
    //     // Assert log message (we are checking for errors)
    //     LogAssert.Expect(LogType.Error, "Cannot request more tiles than available");
    //     // Load scene with too few potential tiles
    //     SceneManager.LoadScene("Test2", LoadSceneMode.Single);
    //     // Yield next frame to scene
    //     yield return null;
    // }
    // [UnityTest]
    // public IEnumerator LoadNotEnoughTiles()
    // {
    //     // Assert log message (we are checking for errors)
    //     LogAssert.Expect(LogType.Error, "must create 1 or more tiles");
    //     // Load scene with not enough tiles to load scene
    //     SceneManager.LoadScene("Test3", LoadSceneMode.Single);
    //     // Yield next frame to scene
    //     yield return null;
    // }
    // [UnityTest]
    // public IEnumerator LoadNotEnoughSize()
    // {
    //     // Assert log message (we are checking for errors)
    //     LogAssert.Expect(LogType.Error, "Scale must be greater than 0");
    //     // Load scene with impossible scale
    //     SceneManager.LoadScene("Test4", LoadSceneMode.Single);
    //     // Yield next frame to scene
    //     yield return null;
    // }
    // /*
    // [UnityTest]
    // public IEnumerator LoadingScreenTest()
    // {
    //     //SceneManager.LoadScene("Test1", LoadSceneMode.Single);

    //     GameObject thing = new GameObject();

    //     LoadingScreenManager loadingScreenManager = thing.AddComponent<LoadingScreenManager>();

    //     loadingScreenManager.LoadScene(5);

    //     yield return new WaitForSeconds(10);
    // }
    // */
    // [UnityTest]
    // public IEnumerator ExitTest()
    // {
    //     // Load a scene so loadingScreenManager can actually load something
    //     SceneManager.LoadScene("Test1", LoadSceneMode.Single);

    //     // Initialize the GameObjects that we need
    //     var triggerObject = new GameObject();
    //     var playerObject = new GameObject("Player");
    //     var loadingObject = new GameObject("LoadingScreenManager");
    //     var mapGenDLAObject = new GameObject("Start");
    //     // Initialize the loading screen manager (this is important in Exit)
    //     var loadingScreenManager = loadingObject.AddComponent<LoadingScreenManager>();
    //     // Set the player object to have the tag so that Exit can detect it
    //     playerObject.tag = "Player";
    //     // Transform the position to the starting spot
    //     triggerObject.transform.position = Vector3.zero;
    //     triggerObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    //     // Initialize an exit component
    //     var exit = triggerObject.AddComponent<Exit>();
    //     // Set our collider in the object to triggered
    //     var collider = triggerObject.AddComponent<BoxCollider2D>();
    //     collider.isTrigger = true;
    //     // Add components to the player object so it can trigger the exit
    //     playerObject.AddComponent<Rigidbody2D>();
    //     playerObject.AddComponent<PolygonCollider2D>();
    //     // Move the player's position to the exit object
    //     playerObject.transform.position = triggerObject.transform.position;

    //     yield return new WaitForSeconds(0.5f);
    // }
    [UnityTest]
    public IEnumerator GameplayTest()
    {
        GameObject enemy = new GameObject();
        enemy.AddComponent<GhoulStateManager>();
        GhoulStateManager stateManager = enemy.GetComponent<GhoulStateManager>();
        stateManager.Testing();

        // yield return new WaitForSeconds(.5f);
        yield return null;
    }
}