using UnityEngine;

public class BomberSpawnState : BomberBaseState
{
    private float spawnTime = 1f;

    public override void EnterState(BomberStateManager bomber)
    {
        Debug.Log("Bomber Entering Spawn State...");
    }

    public override void UpdateState(BomberStateManager bomber)
    {
        if (spawnTime <= 0)
        {
            bomber.SwitchState(bomber.IdleState); 
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }

    public override void OnCollisionEnter2D(BomberStateManager bomber, Collision2D collision)
    {
        
    } 

    public override void OnTriggerStay2D(BomberStateManager bomber, Collider2D collider) 
    {
    
    }

    public override void EventTrigger(BomberStateManager bomber)
    {

    }

    public override void TakeDamage(BomberStateManager bomber)
    {
        bomber.SwitchState(bomber.HitState); 
    }
}
