// using System.Collections;
// using System.Collections.Generic;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
// using UnityEngine.SceneManagement;

// public class MapGenerationTests2
// {
//     // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
//     // `yield return null;` to skip a frame.
//     [UnityTest]
//     public IEnumerator LoadNotEnoughTiles()
//     {
//         // Assert log message (we are checking for errors)
//         LogAssert.Expect(LogType.Error, "must create 1 or more tiles");
//         // Load scene with not enough tiles to load scene
//         SceneManager.LoadSceneAsync("Test3", LoadSceneMode.Single);
//         // Yield next frame to scene
//         yield return null;
//     }
// }