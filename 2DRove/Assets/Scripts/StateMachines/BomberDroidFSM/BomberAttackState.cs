using UnityEngine;

public class BomberAttackState : BomberBaseState
{
    private float attackTime = 0.9f;
    private Animator animator;
    private bool isProperlyPositionedToAttack = false; 

    public override void EnterState(BomberStateManager bomber)
    {
        Debug.Log("Bomber Entering Attack State...");
        attackTime = 1f; // Reset the attack timer

        animator = bomber.GetComponent<Animator>();

        // Determine if the bomber is properly positioned above the player to start attack
        UpdateAttackPermission(bomber);

        if (isProperlyPositionedToAttack)
        {
            animator.SetBool("isAttacking", true);
            // bomber.DropBomb(); // Proceed with attack only if properly positioned
        }
        else
        {
            animator.SetBool("isAttacking", false);
            bomber.SwitchState(bomber.IdleState); // Immediately switch back if not properly positioned
        }
    }

    public override void UpdateState(BomberStateManager bomber)
    {
        if (!isProperlyPositionedToAttack || attackTime <= 0)
        {
            bomber.SwitchState(bomber.IdleState); // Go back to Idle after attacking or if not properly positioned
            animator.SetBool("isAttacking", false);
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }

    private void UpdateAttackPermission(BomberStateManager bomber)
    {
        Vector3 playerPosition = bomber.Player.transform.position;
        Vector3 bomberPosition = bomber.transform.position;

        // Check if the bomber is directly above the player
        bool isAbovePlayer = bomberPosition.x >= playerPosition.x - 0.5f && bomberPosition.x <= playerPosition.x + 0.5f;
        bool isAtCorrectHeight = bomberPosition.y > playerPosition.y + bomber.AttackHeight;

        isProperlyPositionedToAttack = isAbovePlayer && isAtCorrectHeight;
    }

    public override void OnCollisionEnter2D(BomberStateManager bomber, Collision2D collision)
    {

    }

    public override void OnTriggerStay2D(BomberStateManager bomber, Collider2D collider)
    {

    }

    public override void EventTrigger(BomberStateManager bomber)
    {
        int damage = bomber.AttackDamage; // Accessing the damage value from the state manager

        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] affectedPlayers = Physics2D.OverlapCircleAll(bomber.transform.position, bomber.attackRange, mask);

        foreach (Collider2D playerCollider in affectedPlayers)
        {
            if (playerCollider.CompareTag("Player"))
            {
                PlayerController playerScript = playerCollider.GetComponent<PlayerController>();
                if (playerScript != null)
                {
                    playerScript.dealDamage(damage); // Apply damage
                    // Optionally apply knockback
                    Rigidbody2D rb = playerCollider.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 knockbackDirection = (playerCollider.transform.position - bomber.transform.position).normalized;
                        rb.AddForce(knockbackDirection * 5, ForceMode2D.Impulse); // Adjust force as needed
                    }
                    // Trigger hit animation on the player
                    Animator playerAnimator = playerCollider.GetComponent<Animator>();
                    if (playerAnimator != null)
                    {
                        playerAnimator.SetTrigger("Hit");
                    }
                }
            }
        }
    }

    public override void TakeDamage(BomberStateManager bomber)
    {
        bomber.SwitchState(bomber.HitState);
    }
}
