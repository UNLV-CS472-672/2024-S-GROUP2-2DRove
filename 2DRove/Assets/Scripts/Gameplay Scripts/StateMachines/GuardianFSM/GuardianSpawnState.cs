using UnityEngine;

public class GuardianSpawnState : GuardianBaseState
{
    private float spawnTime = 1f;
    public override void EnterState(GuardianStateManager Guardian)
    {
        //Debug.Log("Entering Spawn State...");
    }

    public override void UpdateState(GuardianStateManager Guardian)
    {
        if (spawnTime <= 0)
        {
            Guardian.SwitchState(Guardian.IdleState);
        }

        spawnTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(GuardianStateManager Guardian, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(GuardianStateManager Guardian, Collider2D other) {
        
    }

    public override void EventTrigger(GuardianStateManager Guardian, int attackID)
    {

    }

    public override void TakeDamage(GuardianStateManager Guardian)
    {
        // Guardian.SwitchState(Guardian.HitState);
        float health = Guardian.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            Guardian.SwitchState(Guardian.DeathState);
        }
    }
}
