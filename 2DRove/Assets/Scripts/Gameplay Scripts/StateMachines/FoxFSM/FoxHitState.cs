using UnityEngine;

public class FoxHitState : FoxBaseState
{
    private Animator animator;


    public override void EnterState(FoxStateManager fox)
    {
        Debug.Log("Entering Hit State...");
        animator = fox.GetComponent<Animator>();
        animator.SetBool("hit", true); // Trigger hit animation

    }

    public override void UpdateState(FoxStateManager fox)
    {
        // Debug.Log("Updating Hit State...");
        animator.SetBool("hit", false);
        // Move away from the player
        fox.SwitchState(fox.IdleState);
    }

    public override void OnCollisionEnter2D(FoxStateManager fox, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(FoxStateManager fox, Collider2D other)
    {

    }
    public override void EventTrigger(FoxStateManager fox)
    {

    }

    public override void TakeDamage(FoxStateManager fox)
    {
        // fox.SwitchState(fox.HitState); // change to check health
        float health = fox.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            fox.SwitchState(fox.DeathState);
        }
    }
}
