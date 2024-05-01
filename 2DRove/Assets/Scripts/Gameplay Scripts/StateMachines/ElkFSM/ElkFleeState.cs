using UnityEngine;

public class ElkFleeState : ElkBaseState
{
    private Transform player;
    private float fleeSpeed = 3.5f; // Speed at which the elk moves when fleeing
    private float fleeDuration = 3.0f; // Duration of the flee action
    private float fleeTimer;

    public override void EnterState(ElkStateManager elk)
    {
        //Debug.Log("Entering Flee State...");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fleeTimer = fleeDuration; // Set the timer

        elk.animator.SetBool("isEating", false); // Ensure eating stops
        elk.animator.SetBool("isFleeing", true);
    }

    public override void UpdateState(ElkStateManager elk)
    {
        if (fleeTimer > 0)
        {
            // Continue to move away from the player
            Vector3 fleeDirection = (elk.transform.position - player.position).normalized;
            elk.transform.position += fleeDirection * fleeSpeed * Time.deltaTime;
            fleeTimer -= Time.deltaTime;

            // Flip the elk to face the direction it's fleeing
            if (fleeDirection.x > 0 && elk.transform.localScale.x < 0 ||
                fleeDirection.x < 0 && elk.transform.localScale.x > 0)
            {
                elk.transform.localScale = new Vector3(-elk.transform.localScale.x,
                elk.transform.localScale.y, elk.transform.localScale.z);
            }
        }
        else
        {
            // Fleeing is over, switch to idle state
            elk.animator.SetBool("isFleeing", false);
            elk.SwitchState(elk.IdleState);
        }
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
