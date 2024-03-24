using UnityEngine;

public class WardenSpawnState : WardenBaseState
{
    private float spawnTime = 1f;
    public override void EnterState(WardenStateManager Warden)
    {
        Debug.Log("Entering Spawn State...");
    }

    public override void UpdateState(WardenStateManager Warden)
    {
        if (spawnTime <= 0)
        {
            Warden.SwitchState(Warden.IdleState);
        }

        spawnTime -= Time.deltaTime;
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
