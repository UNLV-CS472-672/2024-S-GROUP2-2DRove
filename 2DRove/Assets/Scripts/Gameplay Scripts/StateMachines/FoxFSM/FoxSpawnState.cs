using UnityEngine;

public class FoxSpawnState : FoxBaseState
{
    private float spawnTime = 1f;
    public override void EnterState(FoxStateManager fox)
    {
        //Debug.Log("Entering Spawn State...");
    }

    public override void UpdateState(FoxStateManager fox)
    {
        if (spawnTime <= 0)
        {
            fox.SwitchState(fox.IdleState);
        }

        spawnTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(FoxStateManager fox, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(FoxStateManager fox, Collider2D other)
    {

    }

    public override void EventTrigger(FoxStateManager fox)
    {

    }

    public override void TakeDamage(FoxStateManager fox)
    {
       // fox.SwitchState(fox.HitState); // change to check health
        float health = fox.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            fox.SwitchState(fox.DeathState);
        }
    }
}
