using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Augments : MonoBehaviour
{
    public Button[] augmentCards;
    public Button rerollButton;
    public Button augmentDebugButton;
    public Button augmentOverlayCloseButton;
    public Canvas augmentOverlayCanvas;
    public static List<string> chosenAugments;
    private List<string> allAugments;
    private List<string> currentChoiceAugments;

    void Start()
    {
        //List of all augments
        allAugments = new List<string> { "Augment #1", "Augment #2", "Augment #3", "Augment #4", "Augment #5", "Augment #6", "Augment #7", "Augment #8", "Augment #9", "Augment #10" };
        //List of augments chosen by the randomizer
        currentChoiceAugments = new List<string>();
        // List of augments chosen by the player
        chosenAugments = new List<string>();
        //List of augments chosen by the player
        rerollButton.onClick.AddListener(Reroll);
        // Augment debug button
        augmentDebugButton.onClick.AddListener(showAugmentOverlay);
        // Augment overlay close button
        augmentOverlayCloseButton.onClick.AddListener(closeAugmentOverlay);
        // initial reroll call to populate the first three augments
        Reroll();
    }
    // Shows augment display
    void showAugmentOverlay()
    {
        pause();
        augmentOverlayCanvas.gameObject.SetActive(true);
    }
    // Closes augment display 
    void closeAugmentOverlay()
    {
        unpause();
        augmentOverlayCanvas.gameObject.SetActive(false);
    }
    //Adds augment to list of chosen augments
    void addAugment(Button button)
    {
        TMPro.TextMeshProUGUI augmentText = button.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        string augmentName = augmentText.text;
        Debug.Log("Adding augment: " + augmentName);
        // Add the selected augment to the list of chosen augments
        chosenAugments.Add(augmentName);
        // Remove the selected augment from the list of current choice augments
        augmentOverlayCanvas.gameObject.SetActive(false);
        //Removes chosen augments
        for(int i = 0; i < allAugments.Count; i++)
        {
            if(chosenAugments.Contains(allAugments[i]))
            {
                allAugments.RemoveAt(i);
            }
        }
        Reroll();
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

    // Populates augments from the list of choice augments available
    void Reroll()
    {
        currentChoiceAugments.Clear();
        
        //Shuffle the augments
        for(int i = 0; i < allAugments.Count; i++)
        {
            string temp = allAugments[i];
            int randomIndex = Random.Range(i, allAugments.Count);
            allAugments[i] = allAugments[randomIndex];
            allAugments[randomIndex] = temp;
        }

        // Assign the first three augments to the buttons
        for(int i = 0; i < 3; i++)
        {
            currentChoiceAugments.Add(allAugments[i]);
            //Changes text to fit augment
            augmentCards[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = allAugments[i];
            //Removes all previous listeners
            augmentCards[i].onClick.RemoveAllListeners();
            //Adds new listener to button
            int index = i;
            augmentCards[i].onClick.AddListener(() => addAugment(augmentCards[index]));
        }
    }
}
