using UnityEngine;

public class GhoulSpawnState : GhoulBaseState
{
    private float spawnTime = 1f;
    public override void EnterState(GhoulStateManager ghoul)
    {
        Debug.Log("Entering Spawn State...");
    }

    public override void UpdateState(GhoulStateManager ghoul)
    {
        if (spawnTime <= 0)
        {
            ghoul.SwitchState(ghoul.IdleState);
        }

        spawnTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(GhoulStateManager ghoul, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(GhoulStateManager ghoul, Collider2D other) {
        
    }

    public override void EventTrigger(GhoulStateManager ghoul)
    {

    }

    public override void TakeDamage(GhoulStateManager ghoul)
    {
        ghoul.SwitchState(ghoul.HitState);
    }
}
