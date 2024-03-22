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
    public IEnumerator MapGenerated()
    {
        //yield return SceneManager.LoadSceneAsync("Maps/Floor1", LoadSceneMode.Single);


        GameObject mapGenObject = GameObject.Find("Start");
        MapGenDLA.MapGenDLA mapGenerator = mapGenObject.GetComponent<MapGenDLA.MapGenDLA>();
        Assert.IsNotNull(mapGenerator, "MapGenDLA component not found");



        yield return null;


    }

}
