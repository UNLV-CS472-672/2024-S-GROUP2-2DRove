using UnityEngine;

public class GuardianHitState : GuardianBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = .167f;
    public override void EnterState(GuardianStateManager Guardian)
    {
        //Debug.Log("Entering Hit State...");
        animator = Guardian.GetComponent<Animator>();
        //set animation bool hitstunn to true or smth
        // NO NEED TO SET TRIGGER bc its done in NewEnemy for now
        // animator.SetTrigger("hit");

        health = Guardian.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            Guardian.SwitchState(Guardian.DeathState);
        }
    }

    public override void UpdateState(GuardianStateManager Guardian)
    {
        if (hitStun <= 0)
        {
            Guardian.SwitchState(Guardian.IdleState);
        }

        hitStun -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(GuardianStateManager Guardian, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(GuardianStateManager Guardian, Collider2D other) {
    }

    public override void EventTrigger(GuardianStateManager Guardian, int attackID)
    {

    }

    public override void TakeDamage(GuardianStateManager Guardian)
    {
        // Guardian.SwitchState(Guardian.HitState);
        float health = Guardian.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            Guardian.SwitchState(Guardian.DeathState);
        }
    }
}
