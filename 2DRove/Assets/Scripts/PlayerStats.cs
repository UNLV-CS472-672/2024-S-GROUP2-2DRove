using System.Xml.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;
    public GameObject player;
    public TMP_Text healthText;
    public TMP_Text GoldValue;
    public Slider healthSlider; 
    public float health;
    public float maxHealth;
    public static event Action OnPlayerDeath;
    public int coins;

    void Awake(){
        if (playerStats != null && playerStats != this){
            Destroy(gameObject); // Destroy duplicate PlayerStats
        }
        else{
            playerStats = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start(){
        health = maxHealth;
        SetHealthUI();
    }

    public void DealDamage(float damage){
        if (playerStats != null){ 
            health -= damage;
            CheckDeath();
            SetHealthUI();
        }
    }

    public void HealCharacter(float heal){
        if (playerStats != null){ 
            health += heal;
            CheckOverheal();
            SetHealthUI();
        }
    }

    private void CheckOverheal(){
        if (health > maxHealth){
            health = maxHealth;
        }
    }

    private void CheckDeath(){
        if (health <= 0){
            health = 0;
            Destroy(gameObject);
            OnPlayerDeath?.Invoke();
        }
    }

    private float CalculateHealthPercentage(){
        return health / maxHealth;
    }

    private void SetHealthUI(){
        healthSlider.value = CalculateHealthPercentage();
        if (healthSlider.value < 0.3f){
            healthSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
        else{
            healthSlider.fillRect.GetComponent<Image>().color = Color.green;
        }

        if (healthSlider.value <= 0){
            healthSlider.value = 0;
            health = 0;
        }
        healthText.text = Mathf.Ceil(health).ToString() + "/" + Mathf.Ceil(maxHealth).ToString();
    }

    public void AddCoin(ItemPickUp item){ // called from ItemPickUp.cs
        if (item.currentObject == ItemPickUp.pickupObject.COIN){
            coins += item.coinQuantity;
            GoldValue.text = "Gold: " + coins.ToString();
        }
    }
}
