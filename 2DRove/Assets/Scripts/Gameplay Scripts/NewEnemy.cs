using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using UnityEngine;

public class NewEnemy : MonoBehaviour
{

    public float maxHealth = 100;
    [SerializeField]
    float currentHealth;
    public Animator animator;  
    [SerializeField]private bool isBurning = false;
    [SerializeField]private float lastBurn;
    [SerializeField]private float burnCoolDown = 5f;
    [SerializeField]private float burnDamage = 0f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // If player has burning augment, enemy takes damage every 5 seconds
    private void FixedUpdate()
    {
        if(isBurning)
        {
            if(notOnCooldown(lastBurn, burnCoolDown)) 
            {
                burn();
            }
        }
    }

    //Returns true if the values for an action are on cooldown or not
    private bool notOnCooldown(float lastActionTime, float cooldown){
        if(Time.time >= lastActionTime + cooldown){ //Time.time is the time since game started. Adding the last action time with the cooldown results in what Time.time has to be before then next action.
            return true;
        }
        return false;
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

    public void EnableBurning()
    {
        isBurning = true;
    }

    public void setBurnDamage(float burnAmount)
    {
        burnDamage = burnAmount;
    }

    private void burn()
    {
        Debug.Log("burn!!!");
        TakeDamage(burnDamage);
        lastBurn = Time.time;
    }
}
