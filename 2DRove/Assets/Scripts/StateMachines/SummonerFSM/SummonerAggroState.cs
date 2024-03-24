using UnityEngine;

public class SummonerAggroState : SummonerBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false; 

    public float summoningDistance = 8f; // The distance at which the Summoner will stop and start summoning
    public float moveSpeed = 1f; // Speed at which the Summoner moves towards the player

    public override void EnterState(SummonerStateManager summoner)
    {
        //Debug.Log("Summoner: Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = summoner.GetComponent<Rigidbody2D>();
        animator = summoner.GetComponent<Animator>();
    }

    public override void UpdateState(SummonerStateManager summoner)
    {
        float distanceToPlayer = Vector2.Distance(player.position, summoner.transform.position);
        // Debug.Log($"Distance to player: {distanceToPlayer}");

        if (distanceToPlayer <= summoningDistance)
        {
            // Stop moving and switch to SummoningState
            animator.SetFloat("velocity", 0);
            rb.velocity = Vector2.zero; // Ensure the Rigidbody stops moving

            Debug.Log("Switching to SummoningState");
            animator.SetBool("isSummoning", true);
            summoner.SwitchState(summoner.SummoningState);
        }
        else
        {
            // Move towards the player
            Vector2 direction = (player.position - summoner.transform.position).normalized;
            rb.velocity = direction * moveSpeed; // Apply movement
            animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

            // Adjust the sprite orientation based on the player's position
            bool shouldFlip = (player.position.x < summoner.transform.position.x);
            if (shouldFlip != flipped)
            {
                flipped = shouldFlip;
                summoner.transform.Rotate(0f, 180f, 0f); // Flip the Summoner by rotating around the Y axis
            }
        }
    }


    public override void OnCollisionEnter2D(SummonerStateManager summoner, Collision2D other)
    {
        // Implement collision behavior specific to the Summoner's Aggro state
    }

    public override void OnTriggerStay2D(SummonerStateManager summoner, Collider2D other)
    {
        if (other.tag == "Player")
        {
            summoner.SwitchState(summoner.SummoningState);
        }
    }

    public override void EventTrigger(SummonerStateManager summoner)
    {
        
    }

    public override void TakeDamage(SummonerStateManager summoner)
    {
        summoner.SwitchState(summoner.HitState);
    }
}
