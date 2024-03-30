using UnityEngine;

public class RockBossSpawnState : RockBossBaseState
{
    private float spawnTime = 1f;
    public override void EnterState(RockBossStateManager RockBoss)
    {
        Debug.Log("Entering Spawn State...");
    }

    public override void UpdateState(RockBossStateManager RockBoss)
    {
        if (spawnTime <= 0)
        {
            RockBoss.SwitchState(RockBoss.IdleState);
        }

        spawnTime -= Time.deltaTime;
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
