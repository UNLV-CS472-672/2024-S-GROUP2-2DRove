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
        // Get the VerticalLayoutGroup component
        VerticalLayoutGroup layoutGroup = augmentListTransform.GetComponent<VerticalLayoutGroup>();

        // Set the spacing
        layoutGroup.spacing = 10f;

        // Set the child alignment
        layoutGroup.childAlignment = TextAnchor.UpperCenter;

        // Set the child controls size
        layoutGroup.childControlHeight = false;
        layoutGroup.childControlWidth = false;

        // Set the child force expand
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = false;
        //Closes other augment display if the other is active. Does so vice versa as well.
        if(augmentChooseDisplay.gameObject.activeSelf)
        {
            augmentChooseDisplay.gameObject.SetActive(false);
        }

        foreach(Transform child in augmentListTransform)
        {
            Destroy(child.gameObject);
        }

        foreach(AugmentObject augment in Augments.chosenAugments)
        {
            GameObject augmentCard = Instantiate(augmentPrefab, augmentListTransform);
            augmentCard.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = augment.augmentName;
            Button button = augmentCard.GetComponent<Button>();
            Sprite newSprite = null;
            switch(augment.augmentRarity)
            {
                case "Common":
                    augmentCard.GetComponent<Image>().sprite = Resources.Load<Sprite>("Augments/Rarity/common");
                    break;
                    newSprite = Resources.Load<Sprite>("Augments/Rarity/common_clicked");
                case "Rare":
                    augmentCard.GetComponent<Image>().sprite = Resources.Load<Sprite>("Augments/Rarity/rare");
                    break;
                    newSprite = Resources.Load<Sprite>("Augments/Rarity/rare_clicked");
                case "Epic":
                    augmentCard.GetComponent<Image>().sprite = Resources.Load<Sprite>("Augments/Rarity/epic");
                    break;
                    newSprite = Resources.Load<Sprite>("Augments/Rarity/epic_clicked");
                case "Legendary":
                    augmentCard.GetComponent<Image>().sprite = Resources.Load<Sprite>("Augments/Rarity/legendary");
                    break;
                    newSprite = Resources.Load<Sprite>("Augments/Rarity/legendary_clicked");
                
            }
            SpriteState spriteState = new SpriteState();
            spriteState.highlightedSprite = newSprite;
            spriteState.pressedSprite = newSprite;
            spriteState.selectedSprite = newSprite;
            button.spriteState = spriteState;
            button.onClick.AddListener(() => OpenDescriptionWindow(augment.augmentDescription));
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
