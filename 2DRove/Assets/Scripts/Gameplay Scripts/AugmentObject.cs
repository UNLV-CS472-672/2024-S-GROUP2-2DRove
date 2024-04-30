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
        public static int burnStackCounter = 0;
        public void healthBoost()
        {
            PlayerController player = getPlayer();
            player.increaseHealth(10); // Increase health by 10.
        }
        public void healthRegen()
        {
            PlayerController player = getPlayer();
            player.IncreaseHealthRegen(1); // Increase health regen by 1.
        }
        // Increases player speed by 1
        public void speedBoost()
        {
            PlayerController player = getPlayer();
            player.IncreaseSpeed(player.getSpeed()*0.1f); // Increase speed by 1. Not sure how this is scaled but will need to be scaled.
        }
        //Increases base mana by 10
        public void manaBoost()
        {
            PlayerController player = getPlayer();
            player.IncreaseMana(10); // Increases players mana by 10.
        }
        public void damageBoost()
        {
            PlayerController player = getPlayer();
            player.IncreaseDamage(1); // Increases players damage by 1.
        }
        public void criticalStrike()
        {
            PlayerController player = getPlayer();
            player.IncreaseCrit(1); // Increases players crit chance by 1.
        }
        // Burning Damage method
        public void burningDamage()
        {
            PlayerController player = getPlayer();
            if(burnStackCounter == 0)
            {
                player.EnableBurning();
                burnStackCounter++;
            }
            else
            {
                player.IncreaseBurn(1f);
            }       
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
