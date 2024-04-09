using UnityEngine;

public class ElkHitState : ElkBaseState
{
    private float hitStunDuration = 0.41f; // The time the elk stays in hit state
    private float hitStunTimer;
    private Animator animator;

    public override void EnterState(ElkStateManager elk)
    {
        Debug.Log("Entering Hit State...");
        animator = elk.GetComponent<Animator>();
        animator.SetBool("isHit", true); // Trigger hit animation

        // Set the hit stun timer when entering the state
        hitStunTimer = hitStunDuration;
    }

    public override void UpdateState(ElkStateManager elk)
    {
        // Count down the hit stun timer
        if (hitStunTimer > 0)
        {
            hitStunTimer -= Time.deltaTime;
        }

        // Once the timer reaches zero, transition back to the previous state idle
        if (hitStunTimer <= 0)
        {
            animator.SetBool("isHit", false);
            elk.SwitchState(elk.IdleState);
        }
    }

    public override void OnCollisionEnter2D(ElkStateManager elk, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(ElkStateManager elk, Collider2D other)
    {

    }
    public override void EventTrigger(ElkStateManager elk)
    {

    }
 
    public override void TakeDamage(ElkStateManager elk)
    {
       // elk.SwitchState(elk.HitState); // change to check health
        float health = elk.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            elk.SwitchState(elk.DeathState);
        }
    }
}
