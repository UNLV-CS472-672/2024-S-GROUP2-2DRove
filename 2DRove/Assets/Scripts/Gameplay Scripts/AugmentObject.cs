using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AugmentObject
{
    public string augmentName;
    // Name will be the name of the augment
    public string augmentDescription;
    // Description will be a brief description of what the augment does
    public string augmentType;
    // Types will be outlined in three categories: Offensive, Defensive, and Utility
    public string augmentRarity;
    // Rarity will be outlined in four categories: Common, Rare, Epic, and Legendary
    public string augmentMethodName;
    public Sprite AugmentImage;
    public Boolean isStackable = false;
    public AugmentObject(){}
}


public class AugmentMethods
{
    // These are the classes you need to work with @voxelit and I gave an example using speedBoost.
    // If you want to find the names of the methods, please reference the AugmentInstntiator.cs file after line 106. Each instance of an augment hasa an "augmentMethodName".
    // The front end however should automatically be linked to the this part now though if you add what it's supposed to do and it should update within each scene to be the same now!
    // If you want to see how the method calls occur, please debug selecting Speedboost and looking at the addAugment method in Augments.cs
    public class commonAugmentMethods
    {
        public void speedBoost()
        {
            PlayerController player = getPlayer();
            player.IncreaseSpeed(1); // I want this to be 10%, I don't know the math @voxelit
        }
    }
    public class rareAugmentMethods
    {}
    public class epicAugmentMethods
    {}
    public class legendaryAugmentMethods
    {}

    private static PlayerController getPlayer()
    {
        GameObject player = GameObject.Find("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        return playerController;
    }
}
