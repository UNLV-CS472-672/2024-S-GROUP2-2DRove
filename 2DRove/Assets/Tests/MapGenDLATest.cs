using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class MapGenDLATest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LoadProperMap()
    {
        // Load a proper scene that is like a scene we would load normally
        SceneManager.LoadScene("Tests/Test1", LoadSceneMode.Single);
        // Yield next frame to scene
        yield return null;

    }
    [UnityTest]
    public IEnumerator LoadNotEnoughPotentialTiles()
    {
        // Assert log message (we are checking for errors)
        LogAssert.Expect(LogType.Error, "Cannot request more tiles than available");
        // Load scene with too few potential tiles
        SceneManager.LoadScene("Test2", LoadSceneMode.Single);
        // Yield next frame to scene
        yield return null;
    }
    [UnityTest]
    public IEnumerator LoadNotEnoughTiles()
    {
        // Assert log message (we are checking for errors)
        LogAssert.Expect(LogType.Error, "Must create 1 or more tiles");
        // Load scene with not enough tiles to load scene
        SceneManager.LoadScene("Test3", LoadSceneMode.Single);
        // Yield next frame to scene
        yield return null;
    }
    [UnityTest]
    public IEnumerator LoadNotEnoughSize()
    {
        // Assert log message (we are checking for errors)
        LogAssert.Expect(LogType.Error, "Scale must be greater than 0");
        // Load scene with impossible scale
        SceneManager.LoadScene("Test4", LoadSceneMode.Single);
        // Yield next frame to scene
        yield return null;
    }
}
