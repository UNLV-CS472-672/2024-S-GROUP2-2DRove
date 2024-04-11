using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AugmentObject : MonoBehaviour
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
}

public class AugmentMethods : MonoBehaviour
{
    public class commonAugmentMethods : MonoBehaviour
    {}
    public class rareAugmentMethods : MonoBehaviour
    {}
    public class epicAugmentMethods : MonoBehaviour
    {}
    public class legendaryAugmentMethods : MonoBehaviour
    {}
    
}
