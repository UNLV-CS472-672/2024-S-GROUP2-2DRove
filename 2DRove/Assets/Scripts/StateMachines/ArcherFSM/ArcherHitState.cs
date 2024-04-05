using UnityEngine;

public class ArcherHitState : ArcherBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = .41f;
    public override void EnterState(ArcherStateManager archer)
    {
        Debug.Log("Entering Hit State...");
        animator = archer.GetComponent<Animator>();
        //set animation bool hitstunn to true or smth
        // NO NEED TO SET TRIGGER bc its done in NewEnemy for now
        // animator.SetTrigger("hit");

        health = archer.GetComponent<NewEnemy>().CurrentHeath();
        if (health <= 0)
        {
            archer.SwitchState(archer.DeathState);
        }
    }

    public override void UpdateState(ArcherStateManager archer)
    {
        if (hitStun <= 0)
        {
            archer.SwitchState(archer.IdleState);
        }

        hitStun -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(ArcherStateManager archer, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(ArcherStateManager archer, Collider2D other) {
    }

    public override void EventTrigger(ArcherStateManager archer)
    {

    }

    public override void TakeDamage(ArcherStateManager archer)
    {
        archer.SwitchState(archer.HitState);
    }
}
