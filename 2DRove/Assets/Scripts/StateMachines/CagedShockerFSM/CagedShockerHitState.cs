using UnityEngine;

public class CagedShockerHitState : CagedShockerBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = .167f;
    public override void EnterState(CagedShockerStateManager CagedShocker)
    {
        Debug.Log("Entering Hit State...");
        animator = CagedShocker.GetComponent<Animator>();
        //set animation bool hitstunn to true or smth
        // NO NEED TO SET TRIGGER bc its done in NewEnemy for now
        // animator.SetTrigger("hit");

        health = CagedShocker.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            CagedShocker.SwitchState(CagedShocker.DeathState);
        }
    }

    public override void UpdateState(CagedShockerStateManager CagedShocker)
    {
        if (hitStun <= 0)
        {
            CagedShocker.SwitchState(CagedShocker.IdleState);
        }

        hitStun -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(CagedShockerStateManager CagedShocker, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(CagedShockerStateManager CagedShocker, Collider2D other) {
    }

    public override void EventTrigger(CagedShockerStateManager CagedShocker)
    {

    }

    public override void TakeDamage(CagedShockerStateManager CagedShocker)
    {
        CagedShocker.SwitchState(CagedShocker.HitState);
    }
}
