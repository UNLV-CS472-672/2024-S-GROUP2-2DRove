// using System.Collections;
// using System.Collections.Generic;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
// using UnityEngine.SceneManagement;

// public class MapGenerationTests1
// {
//     // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
//     // `yield return null;` to skip a frame.

//     [UnityTest]
//     public IEnumerator LoadNotEnoughPotentialTiles()
//     {
//         // Assert log message (we are checking for errors)
//         LogAssert.Expect(LogType.Error, "Cannot request more tiles than available");
//         // Load scene with too few potential tiles
//         SceneManager.LoadSceneAsync("Test2", LoadSceneMode.Single);
//         // Yield next frame to scene
//         yield return null;
//     }
// }