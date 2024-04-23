using System.Collections;
using Codice.Client.Common.GameUI;
using UnityEngine;

public class PlayerSlash1State : PlayerBaseState
{
    private float attackTime;
    private bool combo;
    private bool burning;
    private float burnDamage;
    private float damageBoost;
    private float critRate;
    private bool isVampire;

    private float playerAttackDamage = 3f;
    public override void EnterState(PlayerStateManager Player)
    {
        Debug.Log("Entering Slash1 State");
        attackTime = Player.slash1Time;
        combo = false;
        Player.Coroutine(DashDelay(Player));
        Player.animator.SetTrigger("slash1");
        burning = Player.GetComponent<PlayerController>().doesBurn();
        burnDamage = Player.GetComponent<PlayerController>().getBurnDamage();
        damageBoost = Player.GetComponent<PlayerController>().getDamageBoost();
        critRate = Player.GetComponent<PlayerController>().getCritRate();
        isVampire = Player.GetComponent<PlayerController>().doesVampire();
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        if(attackTime <= 0 && combo == false)
        {
            Player.animator.SetTrigger("neutral");
            Player.SwitchState(Player.NeutralState);
        }
        else if(attackTime <= (Player.slash1Time * .2f) && combo == true)
        {
            Player.SwitchState(Player.Slash2State);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            combo = true;
        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(PlayerStateManager Player, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(PlayerStateManager Player, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(PlayerStateManager Player)
    {
        Vector2 knockbackDirection = (Vector2)(Player.transform.position - Player.attackPoint.position).normalized;
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Player.attackPoint.position, Player.attackRange, mask);

        foreach (Collider2D enemy in colliders)
        {
            if (enemy is not BoxCollider2D)
                continue;
            NewEnemy enemyScript = enemy.GetComponent<NewEnemy>();
            
            if (enemyScript != null) {
                float totalDamage =  playerAttackDamage * damageBoost; // increase total damage by damageBoost
                float crit = Random.Range(1, 100);
                if(crit <= (critRate * 100))
                {
                    totalDamage *= 1.5f;  // use random number to determine if player hits a crit or not
                }
                enemyScript.TakeDamage(totalDamage);
                if(isVampire) {
                     Player.GetComponent<PlayerController>().healPlayer(1f);
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

    public override void TakeDamage(PlayerStateManager Player)
    {
        Player.SwitchState(Player.HitState);
    }

    IEnumerator DashDelay(PlayerStateManager Player)
    {
        yield return new WaitForSeconds(Player.slash1Time * (3/4));
        Player.inputDirection = new Vector2(Player.findDirectionFromInputs("Left", "Right"), Player.findDirectionFromInputs("Down", "Up"));
        // Player.lastInput = (Player.inputDirection != Vector2.zero) ? ((Player.lastInput * .80f) + Player.inputDirection * .20f).normalized : Player.lastInput;
        // Player.lastInput = inputDirection;
        if (Player.inputDirection != Vector2.zero)
        {
            Player.rb.AddForce(100 * Player.slash1Lurch * Player.inputDirection.normalized);
            if (Player.inputDirection.x != 0){ //If the player is moving horizontally
                Player.flipped = Player.inputDirection.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
            }

            Player.transform.rotation = Quaternion.Euler(new Vector3(0f, Player.flipped ? 180f: 0f, 0f));
        }
    }
}
