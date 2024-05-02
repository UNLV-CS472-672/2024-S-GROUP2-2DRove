using System.Collections;
using UnityEngine;

public class WardenAttackState : WardenBaseState
{
    private float attackTime;
    private Animator animator;
    public override void EnterState(WardenStateManager Warden)
    {
        //Debug.Log("Entering Attack State");
        attackTime = Warden.attackTime / Warden.attackSpeed;
        animator = Warden.GetComponent<Animator>();
        animator.SetBool("attacking", true);
    }

    public override void UpdateState(WardenStateManager Warden)
    {
        if(attackTime <= 0)
        {
            Warden.SwitchState(Warden.IdleState);
            animator.SetBool("attacking", false);
        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(WardenStateManager Warden, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(WardenStateManager Warden, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(WardenStateManager Warden)
    {
        // Calculate knockback direction and set y to 0 for horizontal force
        Vector2 knockbackDirection = (Vector2)(Warden.transform.position - Warden.attackPointX.position).normalized;
        knockbackDirection.y = 0; // This ensures the force is applied horizontally
        
        // Use LayerMask.GetMask incorrectly here. Should pass layer names as string array
        // Correcting to use a predefined LayerMask for the player if needed
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(Warden.attackPointX.position, new Vector2(Warden.attackRange * 2, Warden.attackHeight), CapsuleDirection2D.Vertical, 0, mask);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerController playerScript = collider.GetComponent<PlayerController>();
                playerScript.dealDamage(Warden.attackDamage);
                
                // Apply the force using the adjusted knockbackDirection
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(-knockbackDirection * Warden.knockBackForce, ForceMode2D.Impulse);
                }

                Animator anim = collider.GetComponent<Animator>();
                if (anim != null)
                {
                    anim.SetTrigger("Hit");
                }
            }
        }
        
    }
    public override void TakeDamage(WardenStateManager Warden)
    {
        Warden.SwitchState(Warden.HitState);
    }
}
