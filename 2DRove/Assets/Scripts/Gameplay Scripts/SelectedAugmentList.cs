using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedAugmentList : MonoBehaviour
{
    public Button closeAugmentList;
    public Button augmentList;
    public Canvas augmentListOverlay;
    public Canvas augmentChooseDisplay;
    public GameObject augmentPrefab;
    public Transform augmentListTransform;
    void Start()
    {
        closeAugmentList.onClick.AddListener(CloseAugmentList);
        augmentList.onClick.AddListener(ShowAugmentList);
    }

    void ShowAugmentList()
    {
        pause();
        //Closes other augment display if the other is active. Does so vice versa as well.
        if(augmentChooseDisplay.gameObject.activeSelf)
        {
            augmentChooseDisplay.gameObject.SetActive(false);
        }

        foreach(Transform child in augmentListTransform)
        {
            Destroy(child.gameObject);
        }

        foreach(string augment in Augments.chosenAugments)
        {
            GameObject augmentCard = Instantiate(augmentPrefab, augmentListTransform);
            augmentCard.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = augment;
        }
        augmentListOverlay.gameObject.SetActive(true);
    }
    void CloseAugmentList()
    {
        
        augmentListOverlay.gameObject.SetActive(false);
        unpause();
    }

    void pause()
    {
        Time.timeScale = 0;
    }

    void unpause()
    {
        Time.timeScale = 1;
    }
}
