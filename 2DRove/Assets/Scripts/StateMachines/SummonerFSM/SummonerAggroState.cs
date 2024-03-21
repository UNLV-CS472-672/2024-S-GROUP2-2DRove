using UnityEngine;

public class SummonerAggroState : SummonerBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(SummonerStateManager summoner)
    {
        Debug.Log("Summoner: Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = summoner.GetComponent<Rigidbody2D>();
        animator = summoner.GetComponent<Animator>();
    }

    public override void UpdateState(SummonerStateManager summoner)
    {
        Vector2 direction = (player.position - summoner.transform.position).normalized;
        rb.AddForce(direction * 1f); 
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (direction.x != 0) // If the player is moving horizontally
        {
            flipped = direction.x < 0; // Determine if should face left or right
        }

        summoner.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }

    public override void OnCollisionEnter2D(SummonerStateManager summoner, Collision2D other)
    {
        // Implement collision behavior specific to the Summoner's Aggro state
    }

    public override void OnTriggerStay2D(SummonerStateManager summoner, Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            summoner.SwitchState(summoner.SummoningState);
        }
    }

    public override void EventTrigger(SummonerStateManager summoner)
    {
        // Implement any event-specific behavior
    }

    public override void TakeDamage(SummonerStateManager summoner)
    {
        summoner.SwitchState(summoner.HitState);
    }
}
