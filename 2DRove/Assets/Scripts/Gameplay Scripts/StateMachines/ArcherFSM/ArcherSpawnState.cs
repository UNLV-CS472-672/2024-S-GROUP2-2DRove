using UnityEngine;

public class ArcherSpawnState : ArcherBaseState
{
    private float spawnTime = 1f;
    public override void EnterState(ArcherStateManager archer)
    {
        //Debug.Log("Entering Spawn State...");
    }

    public override void UpdateState(ArcherStateManager archer)
    {
        if (spawnTime <= 0)
        {
            archer.SwitchState(archer.IdleState);
        }

        spawnTime -= Time.deltaTime;
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
