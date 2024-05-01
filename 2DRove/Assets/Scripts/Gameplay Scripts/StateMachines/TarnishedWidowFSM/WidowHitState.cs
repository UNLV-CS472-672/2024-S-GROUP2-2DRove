using UnityEngine;

public class WidowHitState : WidowBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = 0f;
    public override void EnterState(WidowStateManager Widow)
    {
        //Debug.Log("Entering Hit State...");
        animator = Widow.GetComponent<Animator>();

        health = Widow.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            Widow.SwitchState(Widow.DeathState);
        }
    }

    public override void UpdateState(WidowStateManager Widow)
    {
        if (hitStun <= 0)
        {
            Widow.SwitchState(Widow.IdleState);
        }

        hitStun -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(WidowStateManager Widow, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(WidowStateManager Widow, Collider2D other) {
    }

    public override void EventTrigger(WidowStateManager Widow)
    {

    }

    public override void TakeDamage(WidowStateManager Widow)
    {
        Widow.SwitchState(Widow.HitState);
    }
}
