using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutAugments : MonoBehaviour
{
    public GameObject augmentPrefab;
    public Transform augmentListTransform;
    
    public void updateBarUI()
    {
        // Clear the augment grid
        foreach (Transform child in augmentListTransform)
        {
            Destroy(child.gameObject);
        }

        if(Augments.chosenAugments == null || Augments.chosenAugments.Count == 0)
        {
            return;
        }
        // Add the augments to the grid
        Dictionary<string, (int, GameObject)> augmentCounts = new Dictionary<string, (int, GameObject)>();
        foreach(AugmentObject augment in Augments.chosenAugments)
        {
            GameObject augmentCard;

            // Increment the count for this augment
            if (augmentCounts.ContainsKey(augment.augmentName))
            {
                var count = augmentCounts[augment.augmentName].Item1;
                var gameObject = augmentCounts[augment.augmentName].Item2;
                augmentCounts[augment.augmentName] = (count + 1, gameObject);
                Destroy(gameObject); // Destroy the old GameObject
                augmentCard = Instantiate(augmentPrefab, augmentListTransform);
            }
            else
            {
                augmentCard = Instantiate(augmentPrefab, augmentListTransform);
                augmentCounts[augment.augmentName] = (1, augmentCard);
            }

            // Instantiate the count text and set the count
            if (augmentCounts[augment.augmentName].Item1 > 1)
            {
                augmentCard.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "x" + augmentCounts[augment.augmentName].Item1;
            }
            else
            {
                augmentCard.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "x1";
            }

            // Set the augment image
            augmentCard.GetComponent<Image>().sprite = augment.AugmentImage;

            // Update the GameObject in the dictionary
            augmentCounts[augment.augmentName] = (augmentCounts[augment.augmentName].Item1, augmentCard);
        }
    }
}
