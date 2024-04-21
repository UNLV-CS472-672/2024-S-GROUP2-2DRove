using UnityEngine;

public class FoxFleeState : FoxBaseState
{
    private Transform player;
    private float fleeSpeed = 5.5f; // Speed at which the fox moves when fleeing
    private float fleeDuration = 3.0f; // Duration of the flee action
    private float fleeTimer;

    public override void EnterState(FoxStateManager fox)
    {
        Debug.Log("Entering Flee State...");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fleeTimer = fleeDuration; // Set the timer

        fox.animator.SetBool("isEating", false); // Ensure eating stops
        fox.animator.SetBool("isFleeing", true);
    }

    public override void UpdateState(FoxStateManager fox)
    {
        if (fleeTimer > 0)
        {
            // Continue to move away from the player
            Vector3 fleeDirection = (fox.transform.position - player.position).normalized;
            fox.transform.position += fleeDirection * fleeSpeed * Time.deltaTime;
            fleeTimer -= Time.deltaTime;

            // Flip the fox to face the direction it's fleeing
            if (fleeDirection.x > 0 && fox.transform.localScale.x < 0 ||
                fleeDirection.x < 0 && fox.transform.localScale.x > 0)
            {
                fox.transform.localScale = new Vector3(-fox.transform.localScale.x,
                fox.transform.localScale.y, fox.transform.localScale.z);
            }
        }
        else
        {
            // Fleeing is over, switch to idle state
            Debug.Log("Fleeing ended, switching to Idle State");
            fox.animator.SetBool("isFleeing", false);
            fox.animator.SetBool("isAlert", false);
            fox.SwitchState(fox.IdleState);
        }
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
