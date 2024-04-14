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
    public class commonAugmentMethods
    {}
    public class rareAugmentMethods
    {}
    public class epicAugmentMethods
    {}
    public class legendaryAugmentMethods
    {}
    
}
