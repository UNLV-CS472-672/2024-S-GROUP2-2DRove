using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

//Generally a PlayerController class is used to contain most if not all player based stuff in one place. (Movement/Actions)

public class PlayerController : MonoBehaviour
{
    //[SerializeField] allows the variable to be edited/set in the editor without having to make it public and exposing it
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private int coins;
    [SerializeField] private float speedFactor;
    private float currentSpeed = 50;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    private float lastDashedTime;
    [SerializeField] private float shootCooldown;
    private float lastShootTime;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private TMP_Text healthText;
    private TMP_Text goldText;
    //[SerializeField] private Slider healthSlider; 

    //[SerializeField] private Slider manaSlider;
    [SerializeField] private TMP_Text manaText;

    //To store component data
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerInput input;
    private GameOverMenu gameOverMenu;
    private bool flipped;
    [SerializeField]private Transform slashPoint;
    [SerializeField]private float slashRange;
    [SerializeField]private LayerMask enemyLayer;
    private float slashCooldown = 1f; //adjust this to change the cooldown of the slash
    private float nextSlashTime = 0f; //dont change this
    private float playerAttackDamage = 10; //adjust this to change the damage of the slash
    [SerializeField] private float knockbackStrength = 5f; // Adjust this value for knock back 
    private PlayerStateManager playerStateManager;

    //Augment-affected fields
    [SerializeField] private float speedBoost = 1f;
    [SerializeField] private float maxMana = 50f;
    [SerializeField] private float mana = 50f;
    [SerializeField] private float rangeMana = 5f;
    [SerializeField] private float healthRegen = 0f;
    private float lastRegen;
    private float regenCooldown = 5f;
    [SerializeField] private float damageBoost = 1f;
    [SerializeField] private float critRate = 0f;
    [SerializeField] private bool burning = false;
    [SerializeField] private float burnDamage = 0f;
    [SerializeField] private bool isVampire = false;
    [SerializeField] private bool resurrect = false;


    //The Start function is called if the script is enabled before any update functions
    private void Start(){
        health = maxHealth;
        mana = maxMana;
        //setHealthandMana();
        // Restore augments to the player
        if(Augments.chosenAugments != null)
        {
            foreach(var augment in Augments.chosenAugments)
            {
                AugmentInstantiator.callAugmentMethod(augment);
            }
        }
        //Assigning the component to the variables to prevent having to get the component at every instance where you need to edit the values
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        playerStateManager = GetComponent<PlayerStateManager>();
        if (animator == null){
            Debug.LogError("Animator not found on player");
        }
        rb = GetComponent<Rigidbody2D>();

        // speedFactor = currentSpeed;

        //Find the game over menu
        gameOverMenu = GameObject.Find("UI Overlay").GetComponent<GameOverMenu>();

        //Find text fields
        // healthText = GameObject.Find("TextSliderBar/Text (TMP)").GetComponent<TMP_Text>();
        goldText = GameObject.Find("Gold Text").GetComponent<TMP_Text>();

        //Find health slider
        // healthSlider = GameObject.Find("TextSliderBar").GetComponent<Slider>();

        //Starts the player with max health and initializes the health slider   
    }

    // Method to reduce the player's movement speed
    public void ReduceMoveSpeed(float amount, float duration) {
        speedFactor -= amount;
        StartCoroutine(ResetMoveSpeed(duration));
    }

    private IEnumerator ResetMoveSpeed(float delay) {
        yield return new WaitForSeconds(delay);
        speedFactor = currentSpeed * speedBoost; // Reset to the normal move speed
    }

    //FixedUpdate unlike Update is called on an independant timer ignoring frame rate while Update is called each frame. Because of this, movement in FixedUpdate does not have to be multiplied by Time.deltaTime
    private void Update(){
        // Vector2 inputDirection = new Vector2(findDirectionFromInputs("Left", "Right"), findDirectionFromInputs("Down", "Up"));
        //Checks for the input and if the blink is on cooldown
        // if(input.actions["Blink"].IsPressed() && notOnCooldown(lastDashedTime, dashCooldown)){
        //     blink(GetComponent<PlayerStateManager>().lastInput, dashDistance);
        // }

        // if(input.actions["Shoot"].IsPressed() && notOnCooldown(lastShootTime, shootCooldown)){
        //     Slash();
        // }

  /*      if (input.actions["RangeAttack"].IsPressed() && notOnCooldown(lastShootTime, shootCooldown)){
            rangeAttack();
        }*/
        if(notOnCooldown(lastRegen, regenCooldown)) {
            regenHealth();
        }
        if(notOnCooldown(lastRegen, 1f)) {
            regenMana();
        }
        //setHealthandMana();
    }

    //Finds the direction given the positive/negative inputs. We add/subtract to a variable rather than directly outputting 1 or -1 because this allows to cancel movements when holding both inputs
    private int findDirectionFromInputs(string negativeDirectionInput, string positiveDirectionInput){ 
        int temp = 0;
        if(input.actions[negativeDirectionInput].IsPressed()){ //IsPressed returns true as long as the key is held down
            temp--;
        }
        if(input.actions[positiveDirectionInput].IsPressed()){
            temp++;
        }
        return temp;
    }

    //Blinks the player based on the direction and distance inputted
    private void blink(Vector2 direction, float distance){
        //First we raycast from the player in the direction and distance specified of the blink. The layermask is there so it only collides with colliders in the Default layer. We raycast to get collisions so the player cant teleport into/through walls or other objects.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, LayerMask.GetMask("Default"));

        Debug.DrawRay(transform.position, direction * distance, Color.red, 10f);
        Debug.Log(hit.distance);
        Vector2 dashLoc;
        if(hit){ //If the raycast collides with an object
            // transform.position = (direction.normalized * hit.distance) + (Vector2)transform.position; //Teleports the player to the object that the raycast collided with, we subtract 1 from hit.distance to prevent the player from teleporting into the block
            dashLoc = (direction.normalized * hit.distance) + (Vector2)transform.position; //Teleports the player to the object that the raycast collided with, we subtract 1 from hit.distance to prevent the player from teleporting into the block
        }else{//If the raycast doesnt collide with anything then there is nothing in the way of the player blinking
            // transform.position = (direction.normalized * distance) + (Vector2)transform.position; //Teleports the player the full distance in the direction the player was moving
            dashLoc = (direction.normalized * distance) + (Vector2)transform.position; //Teleports the player to the object that the raycast collided with, we subtract 1 from hit.distance to prevent the player from teleporting into the block
        }
        
        Vector2 startLoc = transform.position;

        float dashTimer = dashDuration;
        while (dashTimer > 0)
        {
            transform.position = Vector2.Lerp(dashLoc, startLoc, dashTimer/dashDuration);
            dashTimer -= Time.deltaTime;
        }

        lastDashedTime = Time.time; //Updates when the player blinked last, putting the blink on cooldown
    }

    //Range Attack
    private void rangeAttack(){
        if((mana - rangeMana) >= 0) {
            mana -= rangeMana;
            GameObject obj = Instantiate(projectilePrefab, transform.position, Quaternion.identity); //This Instantiates a new projectile from the prefab assigned in the editor then assigns it to obj so we can use it later
            Projectile projectile = obj.GetComponent<Projectile>(); //We grab the Projectile component from the newly created projectile because thats how we can edit the direction (With a public function in the Projectile script)
            Vector2 direction = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized; //We find the direction the mouse is relative to the player's transform here. .normalized effectively converts the values in the Vector2 to -1, 0 or 1

            projectile.setDirection(direction);

            lastShootTime = Time.time; //Updates when the player shot last, putting the shoot function on cooldown
            Collider2D projectileCollider = obj.GetComponent<Collider2D>();
            Collider2D playerCollider = GetComponent<Collider2D>();
            if (projectileCollider != null && playerCollider != null) {
                Physics2D.IgnoreCollision(projectileCollider, playerCollider);
            }
        }
        
    }

    //Slash
    // private void Slash(){
        
    //     if(Time.time >= nextSlashTime){
    //     animator.SetTrigger("Slash"); // Triggers the slash animation
    //     StartCoroutine(stopMovement(1f));
    //     nextSlashTime = Time.time + slashCooldown; // Cooldown management
    //     // Damage application is now handled by the animation event
    // }
    // }

    //This function is called by the animation event at the end of the slash animation
    private void ApplyDamage() {
        Vector2 knockbackDirection = (Vector2)(transform.position - slashPoint.position).normalized;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(slashPoint.position, slashRange, enemyLayer);
        //PROBLEM, THIS HITBOX DETECTION APPLIES TO ENEMY HITBOX AS WELL, NOT JUST THEIR BODY HURTBOX
        //Current fix, only look for box colliders, as current hitboxes for enemies are capsules, while their hurtbox are boxcolliders
        //NEW: simple solution, change to check if the collider checked is NOT a trigger
        foreach (Collider2D enemy in hitEnemies) {
            if (enemy is not BoxCollider2D)
                continue;
            NewEnemy enemyScript = enemy.GetComponent<NewEnemy>();
            
            if (enemyScript != null) {
                float totalDamage =  playerAttackDamage * damageBoost;
                float crit = UnityEngine.Random.Range(1, 100);
                if(crit <= (critRate * 100))
                {
                    totalDamage *= 1.5f;
                }
                enemyScript.TakeDamage(totalDamage);
                if(burning) {
                    //applies burning to enemies
                    enemyScript.EnableBurning();
                    enemyScript.setBurnDamage(burnDamage);
                }
                if (enemy.CompareTag("Enemy")){
                    Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                    if (enemyRb != null) {
                        // Apply knockback
                        enemyRb.AddForce(-knockbackDirection * knockbackStrength, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }


    //Draws a gizmo to show the range of the slash
    private void OnDrawGizmosSelected(){
        if (slashPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(slashPoint.position, slashRange);
    }

    //Stops the player's movement for a short time
    private IEnumerator stopMovement(float time){
        speedFactor = 0; //stops the player's movement
        yield return new WaitForSeconds(time); //waits for the cooldown
        yield return new WaitUntil(() => !Input.GetMouseButton(0)); // wait until the left mouse button is released
        speedFactor = 50; //resumes the player's movement
    }

    //Returns true if the values for an action are on cooldown or not
    private bool notOnCooldown(float lastActionTime, float cooldown){
        if(Time.time >= lastActionTime + cooldown){ //Time.time is the time since game started. Adding the last action time with the cooldown results in what Time.time has to be before then next action.
            return true;
        }
        return false;
    }

    //Deals damage to the player's health value
    public void dealDamage(float damage){
        health -= damage;
        //checkDeath();
        //setHealthandMana();
    }

    //Geals the player's health value
    public void healPlayer(float heal){
        health += heal;
        checkOverheal();
        //setHealthandMana();
    }

    //Sets the player's health to their max health if it is over (Ex. if current health is 13 and max health is 10 it will set the current health = max health which in this case would be 10)
    private void checkOverheal(){
        health = Mathf.Clamp(health, 0f, maxHealth);
    }

    //Checks if the player is dead (health being at or below 0)
    private void checkDeath(){
        if (health <= 0){
            if(resurrect) {
                resetHealth();
                DisableResurrect();
            }
            else {
                gameOverMenu.EnableGameOverMenu(); //Opens the Game Over Menu
                Destroy(gameObject); //Immediately destroys the player's gameobject
            }
        }
    }

    //Updates the visible health bar/slider
/*
    private void setHealthandMana(){
        // healthSlider.value = health / maxHealth; //Gets the % of health left

        float healthPercentage = health/maxHealth;

        healthSlider.value = healthPercentage;

        float manaPercentage = mana/maxMana;

        manaSlider.value = manaPercentage;

        //Sets the color of the health bar based on the % of health left
        // if (healthSlider.value > 0.3f){
        //     healthSlider.fillRect.GetComponent<Image>().color = Color.green;
        // }
        // else{
        //     healthSlider.fillRect.GetComponent<Image>().color = Color.red;
        // }

        //Updates the player's health text
        healthText.text = Mathf.Ceil(health).ToString() + "/" + Mathf.Ceil(maxHealth).ToString();

        //Updates the player's mana text
        manaText.text = Mathf.Ceil(mana).ToString() + "/" + Mathf.Ceil(maxMana).ToString();
    }
*/
    //Adds the coin's value to the player's coin count
    public void addCoin(int amount){
        coins += amount;
        goldText.text = "Gold: " + coins.ToString(); //Updates the visible health text
    }

    public void DashDamage()
    {
        Vector2 knockbackDirection = (Vector2)(this.transform.position - slashPoint.position).normalized;
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(slashPoint.position, slashRange, mask);

        foreach (Collider2D enemy in colliders)
        {
            if (enemy is not BoxCollider2D)
                continue;
            NewEnemy enemyScript = enemy.GetComponent<NewEnemy>();
            
            if (enemyScript != null) {
                float totalDamage =  playerAttackDamage * damageBoost; // increase total damage by damageBoost
                float crit = UnityEngine.Random.Range(1, 100);
                if(crit <= (critRate * 100))
                {
                    totalDamage *= 1.5f;    // use random number to determine if player hits a crit or not
                }
                enemyScript.TakeDamage(totalDamage);
                if(isVampire) {
                    healPlayer(1f);
                }
                if(burning) {
                    //applies burning to enemies
                    enemyScript.EnableBurning();
                    enemyScript.setBurnDamage(burnDamage);
                }
                if (enemy.CompareTag("Enemy")){
                    Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                    if (enemyRb != null) {
                        // Apply knockback
                        enemyRb.AddForce(-knockbackDirection * 5, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }


    // *** AUGMENTS

    /*
        increaseSpeed: increases speed boost if selected in augment system
        params: addSpeed - float, increases speedboost by this value
        @SwiftNemesis use this to connect to frontend
    */
    public void IncreaseSpeed(float addSpeed)
    {
        speedBoost += addSpeed;
    }

    /*
        increaseSpeed: increases speed boost if selected in augment system
        params: addSpeed - float, increases speedboost by this value
    */
    public float getSpeed()
    {
        return speedBoost;
    }

    //increases mana if selected in augment system
    // @SwiftNemesis use this to connect to frontend
    public void IncreaseMana(float addMana)
    {
        maxMana += addMana;
        mana = maxMana;
    }

    //increases Health regen if selected in augment system
    // @SwiftNemesis use this to connect to frontend
    public void IncreaseHealthRegen(float addHealthRegen)
    {
        healthRegen += addHealthRegen;
    }

    //increases health automatically
    private void regenHealth()
    {
        lastRegen = Time.time;
        healPlayer(healthRegen);
    }

    //Increase mana automatically
    private void regenMana()
    {
        lastRegen = Time.time;
        if(mana < maxMana)
        {
            mana += 1f;
        }
    }

    //increases damage if selected in augment system
    // @SwiftNemesis use this to connect to frontend
    public void IncreaseDamage(float addDamage)
    {
        damageBoost += addDamage;
    }

    //increases crit rate if selected in augment system
    // @SwiftNemisis use this to connect to frontend
    public void IncreaseCrit(float addCrit)
    {
        critRate += addCrit;
    }

    //enables burning if selected in augment system
    // @SwiftNemisis use this to connect to frontend
    public void EnableBurning()
    {
        burning = true;
        burnDamage = 1f;
    }

    //increases burn damage if selected in augment system, must enable burning to take effect
    // @SwiftNemisis use this to connect to frontend
    public void IncreaseBurn(float addBurn)
    {
        burnDamage += addBurn;
    }

    //enables vampiric touch if selected in augment system
    // player gains 3 hp for every enemy hit
    // @SwiftNemisis use this to connect to frontend
    public void EnableVampire()
    {
        isVampire = true;
    }

    //enables resurrection if selected in augment system
    // player can recover from dying hit and return to full health
    // @SwiftNemisis use this to connect to frontend
    public void EnableResurrect()
    {
        resurrect = true;
    }

    //disables resurrection once dying
    public void DisableResurrect()
    {
        resurrect = false;
    }

    // set health value to max health value
    public void resetHealth()
    {
        health = maxHealth;
    }

    //returns whether player has burning enabled
    public bool doesBurn()
    {
        return burning;
    }

    //returns the amount of burn damage
    public float getBurnDamage()
    {
        return burnDamage;
    }

    //returns the damage boost
    public float getDamageBoost()
    {
        return damageBoost;
    }

    //returns the crit rate
    public float getCritRate()
    {
        return critRate;
    }

     //returns whether player has vampiric touch enabled
    public bool doesVampire()
    {
        return isVampire;
    }

     //returns whether player has resurrection enabled
    public bool doesResurrect()
    {
        return resurrect;
    }
}