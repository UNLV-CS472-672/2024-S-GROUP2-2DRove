using UnityEngine;

public class WardenHitState : WardenBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = 0f;
    public override void EnterState(WardenStateManager Warden)
    {
        Debug.Log("Entering Hit State...");
        animator = Warden.GetComponent<Animator>();

        health = Warden.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            Warden.SwitchState(Warden.DeathState);
        }
    }

    public override void UpdateState(WardenStateManager Warden)
    {
        if (hitStun <= 0)
        {
            Warden.SwitchState(Warden.IdleState);
        }

        hitStun -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(WardenStateManager Warden, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(WardenStateManager Warden, Collider2D other) {
    }

    public override void EventTrigger(WardenStateManager Warden)
    {

    }

    public override void TakeDamage(WardenStateManager Warden)
    {
        Warden.SwitchState(Warden.HitState);
    }
}
