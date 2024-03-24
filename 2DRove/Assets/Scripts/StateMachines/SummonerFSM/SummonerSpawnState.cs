using UnityEngine;

public class SummonerSpawnState : SummonerBaseState
{
    private float spawnTime = 1f;

    public override void EnterState(SummonerStateManager summoner)
    {
        Debug.Log("Entering Spawn State...");
        // Perform spawn state initialization for the Summoner
    }

    public override void UpdateState(SummonerStateManager summoner)
    {
        // Transition to IdleState after spawnTime has elapsed
        if (spawnTime <= 0)
        {
            summoner.SwitchState(summoner.IdleState);
        }
        else
        {
            // Decrement spawnTime by the time passed since the last frame
            spawnTime -= Time.deltaTime;
        }
    }

    public override void OnTriggerStay2D(SummonerStateManager summoner, Collider2D other)
    {
        // Handle trigger stay events if needed
    }

    // Implement the OnCollisionEnter2D if your SummonerStateManager has this method.
    public override void OnCollisionEnter2D(SummonerStateManager summoner, Collision2D other)
    {
        // Handle collision events if needed
    }

    public override void EventTrigger(SummonerStateManager summoner)
    {
        // Handle specific events if necessary
    }

    public override void TakeDamage(SummonerStateManager summoner)
    {
        // Implement logic for when the Summoner takes damage during the spawn state.
        summoner.SwitchState(summoner.HitState);
    }
}
