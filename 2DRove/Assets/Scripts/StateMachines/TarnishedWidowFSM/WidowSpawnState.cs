using UnityEngine;

public class WidowSpawnState : WidowBaseState
{
    private float spawnTime = 1f;
    public override void EnterState(WidowStateManager Widow)
    {
        Debug.Log("Entering Spawn State...");
    }

    public override void UpdateState(WidowStateManager Widow)
    {
        if (spawnTime <= 0)
        {
            Widow.SwitchState(Widow.IdleState);
        }

        spawnTime -= Time.deltaTime;
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
