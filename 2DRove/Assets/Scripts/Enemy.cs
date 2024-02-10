using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20.0f;
    [SerializeField] private float changeDirectionInterval = 2.0f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject player;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] Slider healthBarSlider;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject coinDrop;
    [SerializeField] private GameObject healthPotionDrop;
    [SerializeField] private float potionDropRate;
    private Rigidbody2D rb;
    private float timer;
    private Vector2 randomDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        timer = changeDirectionInterval; //Sets up the timer to randomly change enemy movement
        if(GameObject.Find("Player") != null){
            player = GameObject.Find("Player"); //Finds the player
        }
        getRandomDirection();
        StartCoroutine(AttackPlayer());

        health = maxHealth;
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime; //Counts the timer down based on the time from last frame update
        if (timer <= 0)
        {
            getRandomDirection();
            timer = changeDirectionInterval; //Resets the timer
        }

        move();
    }

    //Randomizes the direction
    private void getRandomDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    //Lets the enemy move by adding a force with the random direction with the intensity of the speed
    private void move()
    {
        rb.AddForce(randomDirection * moveSpeed);
    }

    //The attack player is an IEnumerator because it uses yield return new WaitForSeconds(float seconds); which delays the code after for the specified time
    //After the delay it shoots at the player and runs the function again
    private IEnumerator AttackPlayer(){
        yield return new WaitForSeconds(cooldown); //Delays the attack by the cooldown

        if (player != null){
            GameObject obj = Instantiate(projectilePrefab, transform.position, Quaternion.identity); //Creates a Projectile from the enemy projectile prefab, the same process as the player's attack
            Projectile projectile = obj.GetComponent<Projectile>();

            Vector2 direction = (player.GetComponent<Transform>().position - transform.position).normalized; //Instead of mouse position it uses the player's transform to aim at the player

            projectile.setDirection(direction);

            StartCoroutine(AttackPlayer()); //Starts the function over
        }
        
    }

    //Deals damage to the enemy's health
    public void dealDamage(float damage){
        healthBar.SetActive(true); //Displays the health bar since it is hidden before the 1st hit
        health -= damage;
        checkDeath();
        healthBarSlider.value = health / maxHealth; //Sets the enemy's health bar size
    }

    //Checks if the enemy dies
    private void checkDeath(){
        if (health <= 0){
            Destroy(gameObject);
            Instantiate(coinDrop, transform.position, Quaternion.identity); //The enemy always spawns a coin on death
            if (Random.Range(0f, 1f) <= potionDropRate){ //If the random meets the potion drop rate, spawns a potion
                Instantiate(healthPotionDrop, transform.position, Quaternion.identity);
            }
        }
    }

    //If the enemy collides with something, randomizes the direction so the enemy doesnt keep walking into the object
    private void OnCollisionEnter2D(){
        getRandomDirection();
    }
}