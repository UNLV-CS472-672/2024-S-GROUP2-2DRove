using UnityEngine;

public class SpitterAggroState : SpitterBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    public override void EnterState(SpitterStateManager spitter)
    {
        Debug.Log("Spitter Entering Aggo State...");
        // Safe check to ensure player is not null
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>(); 
        if (player == null)
        {
            // Debug.LogWarning("Player not found!");
            spitter.SwitchState(spitter.IdleState);
            return;
        }

        rb = spitter.GetComponent<Rigidbody2D>();
        animator = spitter.GetComponent<Animator>();
    }

    public override void UpdateState(SpitterStateManager spitter)
    {
        if (player == null)
        {
            spitter.SwitchState(spitter.IdleState);
            return;
        }

        Vector2 direction = (player.position - spitter.transform.position).normalized;
        rb.AddForce(direction * 1f);
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        // If not attacking, flip the spitter to face the player
        if (!animator.GetBool("isAttacking")) 
        {
            // Check the direction to the player and flip if necessary
            bool shouldFlip = (direction.x > 0 && spitter.transform.localScale.x < 0) || (direction.x < 0 && spitter.transform.localScale.x > 0);
            if (shouldFlip)
            {
                // Flip by scaling in x direction
                spitter.transform.localScale = new Vector3(-spitter.transform.localScale.x, spitter.transform.localScale.y, spitter.transform.localScale.z);
            }
        }
    }

    public override void OnCollisionEnter2D(SpitterStateManager spitter, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(SpitterStateManager spitter, Collider2D other) 
    {
        // Check for null reference before attempting to switch state
        if (player != null && other.gameObject == player.gameObject)
        {
            spitter.SwitchState(spitter.AttackState);
        }
    }

    public override void EventTrigger(SpitterStateManager spitter)
    {
        // Implement any specific event triggers if necessary
    }

    public override void TakeDamage(SpitterStateManager spitter)
    {
        spitter.SwitchState(spitter.HitState);
    }
}
