using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedAugmentList : MonoBehaviour
{
    public Button closeAugmentList;
    public Button augmentList;
    public Button closeAugmentDescription;
    public Canvas augmentListOverlay;
    public Canvas augmentDescription;
    public Canvas augmentChooseDisplay;
    public GameObject augmentPrefab;
    public Transform augmentListTransform;
    void Start()
    {
        closeAugmentList.onClick.AddListener(CloseAugmentList);
        augmentList.onClick.AddListener(ShowAugmentList);
        closeAugmentDescription.onClick.AddListener(closeDescriptionWindow);
    }

    void ShowAugmentList()
    {
        pause();
        // Get the GridLayoutGroup component
        GridLayoutGroup layoutGroup = augmentListTransform.GetComponent<GridLayoutGroup>();

        // Set the cell size
        layoutGroup.cellSize = new Vector2(90, 90); // Adjust this to fit your needs

        // Set the spacing
        layoutGroup.spacing = new Vector2(10, 10);

        // Set the child alignment
        layoutGroup.childAlignment = TextAnchor.UpperLeft;

        // Set the start axis to horizontal
        layoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;

        // Set the constraint to flexible for automatic wrapping
        layoutGroup.constraint = GridLayoutGroup.Constraint.Flexible;
        // // Get the VerticalLayoutGroup component
        // VerticalLayoutGroup layoutGroup = augmentListTransform.GetComponent<VerticalLayoutGroup>();

        // // Set the spacing
        // layoutGroup.spacing = 10f;

        // // Set the child alignment
        // layoutGroup.childAlignment = TextAnchor.UpperCenter;

        // // Set the child controls size
        // layoutGroup.childControlHeight = false;
        // layoutGroup.childControlWidth = false;

        // // Set the child force expand
        // layoutGroup.childForceExpandHeight = false;
        // layoutGroup.childForceExpandWidth = false;
        //Closes other augment display if the other is active. Does so vice versa as well.
        if(augmentChooseDisplay.gameObject.activeSelf)
        {
            augmentChooseDisplay.gameObject.SetActive(false);
        }

        foreach(Transform child in augmentListTransform)
        {
            Destroy(child.gameObject);
        }

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

            // Append the count to the augment name if it's greater than 1
            string augmentName = augment.augmentName;
            if (augmentCounts[augment.augmentName].Item1 > 1)
            {
                augmentName += " x" + augmentCounts[augment.augmentName].Item1;
            }
            augmentCard.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = augmentName;
            
            Button button = augmentCard.GetComponent<Button>();
            //Sprite newSprite = null;
            switch(augment.augmentRarity)
            {
                case "Common":
                    augmentCard.GetComponent<Image>().sprite = augment.AugmentImage;
                    //newSprite = Resources.Load<Sprite>("Augments/Rarity/common_clicked");
                    break; 
                case "Rare":
                    augmentCard.GetComponent<Image>().sprite = augment.AugmentImage;
                    //newSprite = Resources.Load<Sprite>("Augments/Rarity/rare_clicked");
                    break;
                case "Epic":
                    augmentCard.GetComponent<Image>().sprite = augment.AugmentImage;
                    //newSprite = Resources.Load<Sprite>("Augments/Rarity/epic_clicked");
                    break;
                case "Legendary":
                    augmentCard.GetComponent<Image>().sprite = augment.AugmentImage;
                    //newSprite = Resources.Load<Sprite>("Augments/Rarity/legendary_clicked");
                    break;
            }
            SpriteState spriteState = new SpriteState();
            // spriteState.highlightedSprite = newSprite;
            // spriteState.pressedSprite = newSprite;
            // spriteState.selectedSprite = newSprite;
            button.spriteState = spriteState;
            button.onClick.AddListener(() => OpenDescriptionWindow(augment.augmentDescription));

            // Update the GameObject in the dictionary
            augmentCounts[augment.augmentName] = (augmentCounts[augment.augmentName].Item1, augmentCard);
        }

        augmentListOverlay.gameObject.SetActive(true);
    }

    void OpenDescriptionWindow(string description)
    {
        augmentDescription.gameObject.SetActive(true);
        augmentDescription.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = description;
    }

    void closeDescriptionWindow()
    {
        augmentDescription.gameObject.SetActive(false);
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
