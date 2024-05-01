using UnityEngine;

public class PlayerSpawnState : PlayerBaseState
{
    private float spawnTime = 1.017f;
    public override void EnterState(PlayerStateManager Player)
    {
        //Debug.Log("Entering Spawn State...");
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        if (spawnTime <= 0)
        {
            Player.SwitchState(Player.NeutralState);
        }

        spawnTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(PlayerStateManager Player, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(PlayerStateManager Player, Collider2D other) {
        
    }

    public override void EventTrigger(PlayerStateManager Player)
    {

    }

    public override void TakeDamage(PlayerStateManager Player)
    {
        Player.SwitchState(Player.HitState);
    }
}
