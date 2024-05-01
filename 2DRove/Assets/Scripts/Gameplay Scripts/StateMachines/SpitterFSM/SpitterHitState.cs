using UnityEngine;

public class SpitterHitState : SpitterBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = .41f;
    public override void EnterState(SpitterStateManager spitter)
    {
        //Debug.Log("Entering Hit State...");
        animator = spitter.GetComponent<Animator>();
        //set animation bool hitstunn to true or smth
        // NO NEED TO SET TRIGGER bc its done in NewEnemy for now
        // animator.SetTrigger("hit");

        health = spitter.GetComponent<NewEnemy>().CurrentHeath();
        if (health <= 0)
        {
            spitter.SwitchState(spitter.DeathState);
        }
    }

    public override void UpdateState(SpitterStateManager spitter)
    {
        if (hitStun <= 0)
        {
            spitter.SwitchState(spitter.IdleState);
        }

        hitStun -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(SpitterStateManager spitter, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(SpitterStateManager spitter, Collider2D other) {
    }

    public override void EventTrigger(SpitterStateManager spitter)
    {

    }

    public override void TakeDamage(SpitterStateManager spitter)
    {
        spitter.SwitchState(spitter.HitState);
    }
}
