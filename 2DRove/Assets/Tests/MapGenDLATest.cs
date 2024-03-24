using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class MapGenDLATest
{
    //private MapGenDLA.MapGenDLA testDLA;
    //private GameObject testObject;
    // A Test behaves as an ordinary method
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load the test scene
        yield return null;
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LoadProperMap()
    {
        yield return SceneManager.LoadSceneAsync("Tests/Test1");

        //GameObject mapGenObject = GameObject.Find("Start");
        //MapGenDLA.MapGenDLA mapGenerator = mapGenObject.GetComponent<MapGenDLA.MapGenDLA>();
        //Assert.IsNotNull(mapGenerator, "MapGenDLA component not found");

        yield return null;

        yield return SceneManager.UnloadSceneAsync("Tests/Test1");

        

    }
    [UnityTest]
    public IEnumerator LoadImproperMap()
    {
        yield return SceneManager.LoadSceneAsync("Tests/Test2");

        yield return null;

        yield return SceneManager.UnloadSceneAsync("Tests/Test2");

        yield return SceneManager.LoadSceneAsync("Tests/Test3");

        yield return null;

        yield return SceneManager.UnloadSceneAsync("Tests/Test3");

        yield return SceneManager.LoadSceneAsync("Tests/Test4");

        yield return null;

        yield return SceneManager.UnloadSceneAsync("Tests/Test4");
    }
}
