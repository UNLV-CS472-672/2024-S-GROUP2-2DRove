using UnityEngine;

public class RockBossHitState : RockBossBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = 0f;
    public override void EnterState(RockBossStateManager RockBoss)
    {
        //Debug.Log("Entering Hit State...");
        animator = RockBoss.GetComponent<Animator>();

        health = RockBoss.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            RockBoss.SwitchState(RockBoss.DeathState);
        }
    }

    public override void UpdateState(RockBossStateManager RockBoss)
    {
        if (hitStun <= 0)
        {
            RockBoss.SwitchState(RockBoss.IdleState);
        }

        hitStun -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(RockBossStateManager RockBoss, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(RockBossStateManager RockBoss, Collider2D other) {
    }

    public override void EventTrigger(RockBossStateManager RockBoss)
    {

    }

    public override void TakeDamage(RockBossStateManager RockBoss)
    {
        RockBoss.SwitchState(RockBoss.HitState);
    }
}
