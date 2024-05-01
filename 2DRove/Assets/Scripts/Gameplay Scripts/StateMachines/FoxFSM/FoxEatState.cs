using UnityEngine;

public class FoxEatState : FoxBaseState
{
    private Animator animator;
    private float timer;

    private float detectionRadius = 8f; // distance to become alert

    private float eatDuration = 5f; //Time fox eat before switch state
    public override void EnterState(FoxStateManager fox)
    {
        //Debug.Log("Entering Eat State...");
        animator = fox.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isEating", true);
        }
        else
        {
            animator.SetBool("isEating", false);
            fox.SwitchState(fox.IdleState);
        }
        timer = eatDuration; // Reset the timer when entering the state
    }

    public override void UpdateState(FoxStateManager fox)
    {
        if (timer <= 0)
        {
            fox.SwitchState(fox.IdleState);
        }
        else
        {
            timer -= Time.deltaTime;
        }

        // Check if the player is too close, if so, switch to AlertState
        float distanceToPlayer = Vector3.Distance(fox.transform.position, fox.Player.position);
        if (distanceToPlayer < detectionRadius)
        {
            // Player is too close, fox should stop eating and become alert
            fox.SwitchState(fox.AlertState);
        }
    }

    public override void OnCollisionEnter2D(FoxStateManager fox, Collision2D collision)
    {

    }

    public override void OnTriggerStay2D(FoxStateManager fox, Collider2D collider)
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
