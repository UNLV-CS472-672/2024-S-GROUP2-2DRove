using UnityEngine;

public class SummonerSummoningState : SummonerBaseState
{
    private float summoningTime = .9f; // Duration before the actual summoning happens
    private Animator animator;
    public override void EnterState(SummonerStateManager summoner)
    {
        Debug.Log("Entering Attack State");
        summoningTime = .9f;
        animator = summoner.GetComponent<Animator>();
        animator.SetBool("isSummoning", true);
    }

    public override void UpdateState(SummonerStateManager summoner)
    {
        if(summoningTime <= 0)
        {
            summoner.SwitchState(summoner.IdleState);
            animator.SetBool("isSummoning", false);
        }

        summoningTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(SummonerStateManager summoner, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(SummonerStateManager summoner, Collider2D other) {
    }

        public override void EventTrigger(SummonerStateManager summoner)
    {

    }
    public override void TakeDamage(SummonerStateManager summoner)
    {
        summoner.SwitchState(summoner.HitState);
    }
}

    /* ERROR - ArgumentException: The Object you want to instantiate is null

    public GameObject ghoulPrefab; // Assign this in the inspector with your Summoner prefab
    public int numberOfGhoulsToSpawn = 3; // Number of Ghouls to spawn each time

    public override void EnterState(SummonerStateManager summoner)
    {
        Debug.Log("Summoner: Entering Summoning State...");
        summoningTime = 2f; // Reset summoning time if needed
        animator = summoner.GetComponent<Animator>();
        animator.SetBool("isSummoning", true); // Trigger summoning animation
    }

    public override void UpdateState(SummonerStateManager summoner)
    {
        if (summoningTime <= 0)
        {
            PerformSummoning(summoner);
            summoningTime = 2f; // Reset time if want to summoning multiple times
        }
        else
        {
            summoningTime -= Time.deltaTime;
        }
    }

    private void PerformSummoning(SummonerStateManager summoner)
    {
        animator.SetBool("isSummoning", false); // Stop summoning animation

        for (int i = 0; i < numberOfGhoulsToSpawn; i++)
        {
            // Calculate spawn position (around the summoner)
           //  Vector3 spawnPosition = summoner.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

            // Instantiate the ghoul at the spawn position
            GameObject spawnedGhoul = GameObject.Instantiate(ghoulPrefab, spawnPosition, Quaternion.identity);
            // Optional: Initialize the ghoul with any specific state or settings
        }

        // Switch back to idle or any other state as needed
        summoner.SwitchState(summoner.IdleState);
    }

    // Implement other methods as needed, potentially empty if they have no behavior in this state
    public override void OnCollisionEnter2D(SummonerStateManager summoner, Collision2D other)
    {

    }
    public override void OnTriggerStay2D(SummonerStateManager summoner, Collider2D other)
    {

    }
    public override void EventTrigger(SummonerStateManager summoner)
    {

    }
    public override void TakeDamage(SummonerStateManager summoner)
    {
        summoner.SwitchState(summoner.HitState);
    }
}
*/