using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class augmentCheck
{
    [UnityTest]
    public IEnumerator InitializeAugments()
    {
        var load = SceneManager.LoadSceneAsync("Test1", LoadSceneMode.Single);
        yield return load;

        GameObject augment = new GameObject();
<<<<<<< HEAD
=======
        GameObject button = new GameObject();
        var augmentInst = augment.AddComponent<AugmentInstantiator>();
        var augmentObj = new AugmentObject();
        var augmentTest = augment.AddComponent<Augments>();
        var augmentMeth = new AugmentMethods();
        
        Button buttonObj = button.AddComponent<Button>();

        augmentTest.rerollButton = buttonObj;
        augmentTest.augmentDebugButton = buttonObj;
        augmentTest.augmentOverlayCloseButton = buttonObj;
        augmentTest.augmentCards[0] = buttonObj;

>>>>>>> 511d8e6086b328369ae923b7bea5d30bec300d3a
        yield return null;
    }
}