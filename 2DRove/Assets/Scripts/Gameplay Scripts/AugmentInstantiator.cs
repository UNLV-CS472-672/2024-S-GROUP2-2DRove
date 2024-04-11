using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class AugmentInstantiator : MonoBehaviour
{
    public static Dictionary<string, AugmentObject> commonAugmentDictionary = new Dictionary<string, AugmentObject>();
    public static Dictionary<string, AugmentObject> rareAugmentDictionary = new Dictionary<string, AugmentObject>();
    public static Dictionary<string, AugmentObject> epicAugmentDictionary = new Dictionary<string, AugmentObject>();
    public static Dictionary<string, AugmentObject> legendaryAugmentDictionary = new Dictionary<string, AugmentObject>();
    // Start is called before the first frame update
    void Start()
    {
        populateCommonAugmentDictionary();
        populateRareAugmentDictionary();
        populateEpicAugmentDictionary();
        populateLegendaryAugmentDictionary();
    }

    void populateCommonAugmentDictionary()
    {
        // Common Augments
        // Health Boost Augment
        AugmentObject augment = commonAugments.healthBoost();
        commonAugmentDictionary.Add(augment.augmentName, augment);
        // Speed Boost Augment
        augment = commonAugments.speedBoost();
        commonAugmentDictionary.Add(augment.augmentName, augment);
        // Mana Boost Augment
        augment = commonAugments.manaBoost();
        commonAugmentDictionary.Add(augment.augmentName, augment);
        // Health Regen Augment
        augment = commonAugments.healthRegen();
        commonAugmentDictionary.Add(augment.augmentName, augment);
        // Damage Boost Augment
        augment = commonAugments.damageBoost();
        commonAugmentDictionary.Add(augment.augmentName, augment);
        // Critical Strike Augment
        augment = commonAugments.criticalStrike();
        commonAugmentDictionary.Add(augment.augmentName, augment);
        // Burning Damage Augment
        augment = commonAugments.burningDamage();
        commonAugmentDictionary.Add(augment.augmentName, augment);
        
    }

    void populateRareAugmentDictionary()
    {
        // Rare Augments
        AugmentObject augment = rareAugments.placeholder();
        rareAugmentDictionary.Add(augment.augmentName, augment);
    }

    void populateEpicAugmentDictionary()
    {
        // Epic Augments
        AugmentObject augment = epicAugments.placeholder();
        rareAugmentDictionary.Add(augment.augmentName, augment);
    }

    void populateLegendaryAugmentDictionary()
    {
        // Legendary Augments
        AugmentObject augment = legendaryAugments.placeholder();
        rareAugmentDictionary.Add(augment.augmentName, augment);
    }

    void callAugmentMethod(AugmentObject augment)
    {
        Type type = null;
        // Call the augment method
        switch(augment.augmentRarity)
        {
            case "Common":
                type = typeof(AugmentMethods.commonAugmentMethods);
                break;
            case "Rare":
                type = typeof(AugmentMethods.rareAugmentMethods);
                break;
            case "Epic":
                type = typeof(AugmentMethods.epicAugmentMethods);
                break;
            case "Legendary":
                type = typeof(AugmentMethods.legendaryAugmentMethods);
                break;
        }

        MethodInfo method = type.GetMethod(augment.augmentMethodName);

        method?.Invoke(this, null);
    }


}

public class commonAugments : MonoBehaviour
{
    public static AugmentObject healthBoost()
    {
        // Health Boost Augment
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Health Boost";
        augmentObject.augmentDescription = "Increases the player's health by 10 points. This effect can stack.";
        augmentObject.augmentType = "Defensive";
        augmentObject.augmentRarity = "Common";
        augmentObject.augmentMethodName = "healthBoost";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/01_healthBoost");
        augmentObject.isStackable = true;
        AugmentInstantiator.commonAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }

    public static AugmentObject speedBoost()
    {
        // Speed Boost Augment
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Speed Boost";
        augmentObject.augmentDescription = "Increases the player's speed by 10%. This effect can stack.";
        augmentObject.augmentType = "Utility";
        augmentObject.augmentRarity = "Common";
        augmentObject.augmentMethodName = "speedBoost";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/02_speedBoost");
        augmentObject.isStackable = true;
        AugmentInstantiator.commonAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }

    public static AugmentObject manaBoost()
    {
        // Mana Boost Augment
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Mana Boost";
        augmentObject.augmentDescription = "Increases the player's mana by 10 points. This effect can stack.";
        augmentObject.augmentType = "Utility";
        augmentObject.augmentRarity = "Common";
        augmentObject.augmentMethodName = "manaBoost";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/03_manaBoost");
        augmentObject.isStackable = true;
        AugmentInstantiator.commonAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }

    public static AugmentObject healthRegen()
    {
        // Health Regen Augment
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Health Regen";
        augmentObject.augmentDescription = "Regenerates the player's health by 1 point every 5 seconds. This effect can stack.";
        augmentObject.augmentType = "Defensive";
        augmentObject.augmentRarity = "Common";
        augmentObject.augmentMethodName = "healthRegen";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/04_healthRegen");
        AugmentInstantiator.commonAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }

    public static AugmentObject damageBoost()
    {
        // Damage Boost Augment
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Damage Boost";
        augmentObject.augmentDescription = "Increases the player's damage by 10%. This effect can stack.";
        augmentObject.augmentType = "Offensive";
        augmentObject.augmentRarity = "Common";
        augmentObject.augmentMethodName = "damageBoost";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/05_damageBoost");
        augmentObject.isStackable = true;
        AugmentInstantiator.commonAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }

    public static AugmentObject criticalStrike()
    {
        // Critical Strike Augment
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Critical Strike";
        augmentObject.augmentDescription = "Increases the player's critical strike chance by 10%. This effect can stack.";
        augmentObject.augmentType = "Offensive";
        augmentObject.augmentRarity = "Common";
        augmentObject.augmentMethodName = "criticalStrike";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/06_criticalStrike");
        augmentObject.isStackable = true;
        AugmentInstantiator.commonAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }

    public static AugmentObject burningDamage()
    {
        // Burning Damage Augment
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Burning Damage";
        augmentObject.augmentDescription = "Applies a burning effect to the player's attacks, dealing 1 damage per second for 5 seconds. This effect can stack.";
        augmentObject.augmentType = "Offensive";
        augmentObject.augmentRarity = "Common";
        augmentObject.augmentMethodName = "burningDamage";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/07_burningDamage");
        augmentObject.isStackable = true;
        AugmentInstantiator.commonAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }
}

public class rareAugments : MonoBehaviour
{
    public static AugmentObject placeholder()
    {
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Placeholder";
        augmentObject.augmentDescription = "This is a placeholder augment.";
        augmentObject.augmentType = "Utility";
        augmentObject.augmentRarity = "Rare";
        augmentObject.augmentMethodName = "placeholder";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/placeholder");
        AugmentInstantiator.rareAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }
}
public class epicAugments : MonoBehaviour
{
    public static AugmentObject placeholder()
    {
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Placeholder";
        augmentObject.augmentDescription = "This is a placeholder augment.";
        augmentObject.augmentType = "Utility";
        augmentObject.augmentRarity = "Rare";
        augmentObject.augmentMethodName = "placeholder";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/placeholder");
        AugmentInstantiator.rareAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }
}
public class legendaryAugments : MonoBehaviour
{
    public static AugmentObject placeholder()
    {
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Placeholder";
        augmentObject.augmentDescription = "This is a placeholder augment.";
        augmentObject.augmentType = "Utility";
        augmentObject.augmentRarity = "Rare";
        augmentObject.augmentMethodName = "placeholder";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/placeholder");
        AugmentInstantiator.rareAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }
}
