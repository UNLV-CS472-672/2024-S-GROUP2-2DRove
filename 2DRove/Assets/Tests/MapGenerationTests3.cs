// using System.Collections;
// using System.Collections.Generic;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
// using UnityEngine.SceneManagement;

// public class MapGenerationTests3
// {
//     [UnityTest]
//     public IEnumerator LoadNotEnoughSize()
//     {
//         // Assert log message (we are checking for errors)
//         LogAssert.Expect(LogType.Error, "Scale must be greater than 0");
//         // Load scene with impossible scale
//         SceneManager.LoadSceneAsync("Test4", LoadSceneMode.Single);
//         // Yield next frame to scene
//         yield return null;
//     }
// }