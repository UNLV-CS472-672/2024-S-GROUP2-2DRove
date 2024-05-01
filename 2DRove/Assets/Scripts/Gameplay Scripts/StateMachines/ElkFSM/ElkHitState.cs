using UnityEngine;

public class ElkHitState : ElkBaseState
{
    private Animator animator;

    public override void EnterState(ElkStateManager elk)
    {
        //Debug.Log("Entering Hit State...");
        animator = elk.GetComponent<Animator>();
        animator.SetBool("hit", true); // Trigger hit animation

    }

    public override void UpdateState(ElkStateManager elk)
    {
        // Debug.Log("Updating Hit State...");
        animator.SetBool("hit", false);
        // Move away from the player
        elk.SwitchState(elk.IdleState);
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
