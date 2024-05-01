using UnityEngine;

public class DaggerMushroomSpawnState : DaggerMushroomBaseState
{
    private float spawnTime = 1f;
    public override void EnterState(DaggerMushroomStateManager mushroom)
    {
        //Debug.Log("Entering Spawn State...");
    }

    public override void UpdateState(DaggerMushroomStateManager mushroom)
    {
        if (spawnTime <= 0)
        {
            mushroom.SwitchState(mushroom.IdleState);
        }

        spawnTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(DaggerMushroomStateManager mushroom, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(DaggerMushroomStateManager mushroom, Collider2D other) {
        
    }

    public override void EventTrigger(DaggerMushroomStateManager mushroom)
    {

    }

    public override void TakeDamage(DaggerMushroomStateManager mushroom)
    {
        mushroom.SwitchState(mushroom.HitState);
    }
}
