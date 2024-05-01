using UnityEngine;

public class CagedShockerSpawnState : CagedShockerBaseState
{
    private float spawnTime = 1f;
    public override void EnterState(CagedShockerStateManager CagedShocker)
    {
        //Debug.Log("Entering Spawn State...");
    }

    public override void UpdateState(CagedShockerStateManager CagedShocker)
    {
        if (spawnTime <= 0)
        {
            CagedShocker.SwitchState(CagedShocker.IdleState);
        }

        spawnTime -= Time.deltaTime;
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
