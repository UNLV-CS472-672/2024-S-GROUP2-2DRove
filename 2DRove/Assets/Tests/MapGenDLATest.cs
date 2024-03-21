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
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("Maps/Floor1");
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MapGenerated()
    {
        //Assert.IsNotNull(testDLA);

        //testDLA.GenerateFirstTile(new Vector2Int(0, 0));
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

}
