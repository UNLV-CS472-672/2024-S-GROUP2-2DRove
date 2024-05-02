using UnityEngine;
using System.Collections; // This line is necessary for IEnumerator


public class SummonerSummoningState : SummonerBaseState
{
    public GameObject ghoulPrefab; // Assigned via Unity Inspector
    // private float summoningTime = 0.5f; // Time before summoning happens
    private Animator animator;
    private Transform playerTransform;
    public float summoningRange = 8f; // Range within which summoning continues
    private int ghoulSummonCount = 0; // Counter for the number of Ghouls summoned
    public int maxGhouls = 3; // Maximum number of Ghouls to summon
    public float summonDelay = 1f; // Delay between each Ghoul summoning

    public override void EnterState(SummonerStateManager summoner)
    {
        Debug.Log("Summoner: Entering Summoning State...");
        animator = summoner.GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player

        // Start the summoning process
        animator.SetBool("isSummoning", true);

        summoner.StartCoroutine(SummonGhoul(summoner));
    }
    private IEnumerator SummonGhoul(SummonerStateManager summoner)
    {
        ghoulPrefab = Resources.Load<GameObject>("Prefabs/Ghoul");
        while (ghoulSummonCount < maxGhouls)
        {
            animator.SetBool("isSummoning", true);

            // Wait for the specified delay to simulate summoning time
            yield return new WaitForSeconds(summonDelay);

            // Randomize the spawn position around the summoner within a given radius
            Vector3 spawnOffset = Random.insideUnitCircle * summoningRange; // summoningRange is used as radius here
            Vector3 spawnPosition = summoner.transform.position + spawnOffset;

            // Instantiate the ghoul prefab
            if (ghoulPrefab != null)
            {
                GameObject.Instantiate(ghoulPrefab, spawnPosition, Quaternion.identity);
                ghoulSummonCount++;
            }
            else
            {
                Debug.LogError("Ghoul prefab not assigned in SummonerSummoningState.");
                break; // Exit the loop if the prefab is not assigned
            }

            // Reset the animator if we've reached the max number of ghouls
            if (ghoulSummonCount >= maxGhouls)
            {
                animator.SetBool("isSummoning", false);
                break;
            }
        }

        // Once all ghouls are summoned, stop the summoning animation and switch to idle
        animator.SetBool("isSummoning", false);
        summoner.SwitchState(summoner.IdleState);
    }

    public override void UpdateState(SummonerStateManager summoner)
    {
        // Check if the player has moved out of summoning range
        if (Vector3.Distance(summoner.transform.position, playerTransform.position) > summoningRange)
        {
            Debug.Log("Player moved out of summoning range. Switching state.");
            animator.SetBool("isSummoning", false);
            summoner.SwitchState(summoner.AggroState); // Switch back to AggroState
        }
    }


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
