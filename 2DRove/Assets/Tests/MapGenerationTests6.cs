using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class MapGenerationTests6
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LoadCityMap()
    {
        // Load a proper scene that is like a scene we would load normally
        var load = SceneManager.LoadSceneAsync("Test6", LoadSceneMode.Single);
        // Yield next frame to scene
        yield return load;
        // Unload the scene
    }
}