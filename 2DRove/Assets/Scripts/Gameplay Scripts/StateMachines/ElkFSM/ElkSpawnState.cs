using UnityEngine;

public class ElkSpawnState : ElkBaseState
{
    private float spawnTime = 1f;
    public override void EnterState(ElkStateManager elk)
    {
        //Debug.Log("Entering Spawn State...");
    }

    public override void UpdateState(ElkStateManager elk)
    {
        if (spawnTime <= 0)
        {
            elk.SwitchState(elk.IdleState);
        }

        spawnTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(ElkStateManager elk, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(ElkStateManager elk, Collider2D other)
    {

    }

    public override void EventTrigger(ElkStateManager elk)
    {

    }

    public override void TakeDamage(ElkStateManager elk)
    {
       // elk.SwitchState(elk.HitState); // change to check health
        float health = elk.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            elk.SwitchState(elk.DeathState);
        }
    }
}
