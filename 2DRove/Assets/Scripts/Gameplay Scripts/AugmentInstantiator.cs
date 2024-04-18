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
            commonAugments.healthBoost();
            // Speed Boost Augment
            commonAugments.speedBoost();
            // Mana Boost Augment
            commonAugments.manaBoost();
            // Health Regen Augment
            commonAugments.healthRegen();
            // Damage Boost Augment
            commonAugments.damageBoost();
            // Critical Strike Augment
            commonAugments.criticalStrike();
            // Burning Damage Augment
            commonAugments.burningDamage();
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

    public static void callAugmentMethod(AugmentObject augment)
    {
        // Call the augment method
        switch(augment.augmentRarity)
        {
            case "Common":
            {
                AugmentMethods.commonAugmentMethods commonAugmentMethods = new AugmentMethods.commonAugmentMethods();
                Type commonType = typeof(AugmentMethods.commonAugmentMethods);
                MethodInfo commonMethod = commonType.GetMethod(augment.augmentMethodName);
                commonMethod.Invoke(commonAugmentMethods, null);
                break;
            }
                
            case "Rare":
            {
                AugmentMethods.rareAugmentMethods rareAugmentMethods = new AugmentMethods.rareAugmentMethods();
                Type rareType= typeof(AugmentMethods.rareAugmentMethods);
                MethodInfo rareMethod = rareType.GetMethod(augment.augmentMethodName);
                rareMethod.Invoke(rareAugmentMethods, null);
                break;
            }
            case "Epic":
            {
                AugmentMethods.epicAugmentMethods epicAugmentMethods = new AugmentMethods.epicAugmentMethods();
                Type epicType = typeof(AugmentMethods.epicAugmentMethods);
                MethodInfo epicMethod = epicType.GetMethod(augment.augmentMethodName);
                epicMethod.Invoke(epicAugmentMethods, null);
                break;
            }
                
            case "Legendary":
            {
                AugmentMethods.legendaryAugmentMethods legendaryAugmentmethods = new AugmentMethods.legendaryAugmentMethods();
                Type legendaryType = typeof(AugmentMethods.legendaryAugmentMethods);
                MethodInfo legendaryMethod = legendaryType.GetMethod(augment.augmentMethodName);
                legendaryMethod.Invoke(legendaryAugmentmethods, null);
                break;
            }
        }
    }
}

public class commonAugments
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

public class rareAugments
{
    public static AugmentObject placeholder()
    {
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Placeholder1";
        augmentObject.augmentDescription = "This is a placeholder augment.";
        augmentObject.augmentType = "Utility";
        augmentObject.augmentRarity = "Rare";
        augmentObject.augmentMethodName = "placeholder";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/placeholder");
        AugmentInstantiator.rareAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }
}
public class epicAugments
{
    public static AugmentObject placeholder()
    {
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Placeholder2";
        augmentObject.augmentDescription = "This is a placeholder augment.";
        augmentObject.augmentType = "Utility";
        augmentObject.augmentRarity = "Rare";
        augmentObject.augmentMethodName = "placeholder";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/placeholder");
        AugmentInstantiator.rareAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }
}
public class legendaryAugments
{
    public static AugmentObject placeholder()
    {
        AugmentObject augmentObject = new AugmentObject();
        augmentObject.augmentName = "Placeholder3";
        augmentObject.augmentDescription = "This is a placeholder augment.";
        augmentObject.augmentType = "Utility";
        augmentObject.augmentRarity = "Rare";
        augmentObject.augmentMethodName = "placeholder";
        augmentObject.AugmentImage = Resources.Load<Sprite>("Augments/placeholder");
        AugmentInstantiator.rareAugmentDictionary.Add(augmentObject.augmentName, augmentObject);
        return augmentObject;
    }
}
