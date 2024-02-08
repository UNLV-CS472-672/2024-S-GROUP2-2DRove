using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGetDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public GameObject lootDrop;
    public GameObject healthPotionDrop;

    void Start(){
        health = maxHealth;
    }

    public void DealDamage(float damage){
        healthBar.SetActive(true);
        health -= damage;
        CheckDeath();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    public void HealCharacter(float heal){
        health += heal;
        CheckOverheal();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    private void CheckOverheal(){
        if (health > maxHealth){
            health = maxHealth;
        }
    }

    private void CheckDeath(){
        if (health <= 0){
            Destroy(gameObject);
            Instantiate(lootDrop, transform.position, Quaternion.identity);
            if (Random.Range(0f, 1f) <= 0.2f){ // 20% chance to drop health potion
                Instantiate(healthPotionDrop, transform.position, Quaternion.identity);
            }
        }
    }

    private float CalculateHealthPercentage(){
        return (health / maxHealth);
    }
}
