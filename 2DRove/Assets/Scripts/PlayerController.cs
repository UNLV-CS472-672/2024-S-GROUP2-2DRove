using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

//Generally a PlayerController class is used to contain most if not all player based stuff in one place. (Movement/Actions)

public class PlayerController : MonoBehaviour
{
    //[SerializeField] allows the variable to be edited/set in the editor without having to make it public and exposing it
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private int coins;
    [SerializeField] private float speedFactor;
    [SerializeField] private float blinkDistance;
    [SerializeField] private float blinkCooldown;
    private float lastBlinkedTime;
    [SerializeField] private float shootCooldown;
    private float lastShootTime;
    [SerializeField] private GameObject projectilePrefab;
    private TMP_Text healthText;
    private TMP_Text goldText;
    private Slider healthSlider; 

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


    //The Start function is called if the script is enabled before any update functions
    private void Start(){
        //Assigning the component to the variables to prevent having to get the component at every instance where you need to edit the values
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        if (animator == null){
            Debug.LogError("Animator not found on player");
        }
        rb = GetComponent<Rigidbody2D>();

        //Find the game over menu
        gameOverMenu = GameObject.Find("UI Overlay").GetComponent<GameOverMenu>();

        //Find text fields
        healthText = GameObject.Find("Health Text").GetComponent<TMP_Text>();
        goldText = GameObject.Find("Gold Text").GetComponent<TMP_Text>();

        //Find health slider
        healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();

        //Starts the player with max health and initializes the health slider
        health = maxHealth;
        setHealthUI();
    }

    //FixedUpdate unlike Update is called on an independant timer ignoring frame rate while Update is called each frame. Because of this, movement in FixedUpdate does not have to be multiplied by Time.deltaTime
    private void FixedUpdate(){
        //A Vector2 is a data type formatted like a coordinate (x, y) and is used by many things from position in the transform component to magitude of forces with physics
        Vector2 inputDirection = new Vector2(findDirectionFromInputs("Left", "Right"), findDirectionFromInputs("Down", "Up"));

        //Multiplies the direction by the speed and applies it as a force. Default force type is ForceMode2D.Force
        rb.AddForce(inputDirection * speedFactor);

        //Decides which animation plays based on the input direction
        animateMovement(inputDirection);
        

        //Checks for the input and if the blink is on cooldown
        if(input.actions["Blink"].IsPressed() && notOnCooldown(lastBlinkedTime, blinkCooldown)){
            blink(inputDirection, blinkDistance);
        }

        if(input.actions["Shoot"].IsPressed() && notOnCooldown(lastShootTime, shootCooldown)){
            Slash();
        }

        if (input.actions["RangeAttack"].IsPressed() && notOnCooldown(lastShootTime, shootCooldown)){
            rangeAttack();
        }
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

    //Decides the animation state of the player (Idle is layer 0, Walking is layer 1)
    private void animateMovement(Vector2 direction){
        animator.SetFloat("yDir", Mathf.Abs(direction.y)); //Sets the vertical direction parameter in the animator to the player's y velocity
        animator.SetFloat("xDir", Mathf.Abs(direction.x)); //Sets the velocity parameter in the animator to the absolute value of the player's x velocity. This is used to determine if the player is moving or not
        
        if (direction.x != 0){ //If the player is moving horizontally
            flipped = direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }
        
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
        // if(direction == Vector2.zero){ //If player is not moving
        //     animator.SetLayerWeight(1, 0); //Hides the movement animation by setting it's weight to 0
        // }
        // else{
        //     animator.SetLayerWeight(1, 1); //Shows the movement animation by setting it's weight to 1, allowing it to show over the idle animation
        //     animator.SetFloat("xDir", direction.x); //These set which movement animation plays based on which direction the player is facing
        //     animator.SetFloat("yDir", direction.y);
        // }
    }

    //Blinks the player based on the direction and distance inputted
    private void blink(Vector2 direction, float distance){
        //First we raycast from the player in the direction and distance specified of the blink. The layermask is there so it only collides with colliders in the Default layer. We raycast to get collisions so the player cant teleport into/through walls or other objects.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, LayerMask.GetMask("Default"));
        Debug.DrawRay(transform.position, direction * distance, Color.red, 10f);
        Debug.Log(hit.distance);
        if(hit){ //If the raycast collides with an object
            transform.position = (direction * (hit.distance - 1)) + (Vector2)transform.position; //Teleports the player to the object that the raycast collided with, we subtract 1 from hit.distance to prevent the player from teleporting into the block
        }else{//If the raycast doesnt collide with anything then there is nothing in the way of the player blinking
            transform.position = (direction * distance) + (Vector2)transform.position; //Teleports the player the full distance in the direction the player was moving
        }

        lastBlinkedTime = Time.time; //Updates when the player blinked last, putting the blink on cooldown
    }

    //Range Attack
    private void rangeAttack(){
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

    //Slash
    private void Slash(){
        
        if(Time.time >= nextSlashTime){
        animator.SetTrigger("Slash"); // Triggers the slash animation
        StartCoroutine(stopMovement(1f));
        nextSlashTime = Time.time + slashCooldown; // Cooldown management
        // Damage application is now handled by the animation event
    }
    }

    //This function is called by the animation event at the end of the slash animation
    private void ApplyDamage() {
        Vector2 knockbackDirection = (Vector2)(transform.position - slashPoint.position).normalized;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(slashPoint.position, slashRange, enemyLayer);
        //PROBLEM, THIS HITBOX DETECTION APPLIES TO ENEMY HITBOX AS WELL, NOT JUST THEIR BODY HURTBOX
        //Current fix, only look for box colliders, as current hitboxes for enemies are capsules, while their hurtbox are boxcolliders
        foreach (Collider2D enemy in hitEnemies) {
            if (enemy is not BoxCollider2D)
                continue;
            NewEnemy enemyScript = enemy.GetComponent<NewEnemy>();
            
            if (enemyScript != null) {
                enemyScript.TakeDamage(playerAttackDamage);
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                if (enemyRb != null) {
                    // Apply knockback
                    enemyRb.AddForce(-knockbackDirection * knockbackStrength, ForceMode2D.Impulse);
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
        checkDeath();
        setHealthUI();
    }

    //Geals the player's health value
    public void healPlayer(float heal){
        health += heal;
        checkOverheal();
        setHealthUI();
    }

    //Sets the player's health to their max health if it is over (Ex. if current health is 13 and max health is 10 it will set the current health = max health which in this case would be 10)
    private void checkOverheal(){
        health = Mathf.Clamp(health, 0f, maxHealth);
    }

    //Checks if the player is dead (health being at or below 0)
    private void checkDeath(){
        if (health <= 0){
            gameOverMenu.EnableGameOverMenu(); //Opens the Game Over Menu
            Destroy(gameObject); //Immediately destroys the player's gameobject
        }
    }

    //Updates the visible health bar/slider
    private void setHealthUI(){
        healthSlider.value = health / maxHealth; //Gets the % of health left

        //Clamps the health between 0f and 1f (0% - 100%)
        healthSlider.value = Mathf.Clamp(healthSlider.value, 0f, 1f);

        //Sets the color of the health bar based on the % of health left
        if (healthSlider.value > 0.3f){
            healthSlider.fillRect.GetComponent<Image>().color = Color.green;
        }
        else{
            healthSlider.fillRect.GetComponent<Image>().color = Color.red;
        }

        //Updates the player's health text
        healthText.text = Mathf.Ceil(health).ToString() + "/" + Mathf.Ceil(maxHealth).ToString();
    }

    //Adds the coin's value to the player's coin count
    public void addCoin(int amount){
        coins += amount;
        goldText.text = "Gold: " + coins.ToString(); //Updates the visible health text
    }
}
