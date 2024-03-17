using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemy : MonoBehaviour
{

    public float maxHealth = 100;
    float currentHealth;
    public Animator animator;  
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hit");
    }

    public void TakeRangedDamage(float rangedDamage)
    {
        currentHealth -= rangedDamage;
        animator.SetTrigger("hit");
    }
    
    public float CurrentHeath()
    {
        return currentHealth;
    }
}
