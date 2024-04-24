using UnityEngine;

public class ElkEatState : ElkBaseState
{
    private Animator animator;
    private float timer;

    private float detectionRadius = 5f; // distance to become alert

    private float eatDuration = 5f; //Time elk eat before switch state

    private AudioSource eatSound;
    public override void EnterState(ElkStateManager elk)
    {
        Debug.Log("Entering Eat State...");
        animator = elk.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isEating", true);
        }
        else
        {
            animator.SetBool("isEating", false);
            elk.SwitchState(elk.IdleState);
        }
        AudioSource[] sources = elk.GetComponents<AudioSource>();
        eatSound = sources[0];
        eatSound.Play();
        timer = eatDuration; // Reset the timer when entering the state
    }

    public override void UpdateState(ElkStateManager elk)
    {
        if (timer <= 0)
        {
            eatSound.Stop();
            elk.SwitchState(elk.IdleState);
        }
        else
        {
            timer -= Time.deltaTime;
        }

        // Check if the player is too close, if so, switch to AlertState
        float distanceToPlayer = Vector3.Distance(elk.transform.position, elk.Player.position);
        if (distanceToPlayer < detectionRadius)
        {
            eatSound.Stop();
            // Player is too close, elk should stop eating and become alert
            elk.SwitchState(elk.AlertState);
        }
    }

    public override void OnCollisionEnter2D(ElkStateManager elk, Collision2D collision)
    {

    }

    public override void OnTriggerStay2D(ElkStateManager elk, Collider2D collider)
    {

    }


    public override void EventTrigger(ElkStateManager elk)
    {

    }

    public override void TakeDamage(ElkStateManager elk)
    {
       // elk.SwitchState(elk.HitState); // change to check health
        eatSound.Stop();
        float health = elk.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            elk.SwitchState(elk.DeathState);
        }
    }

}
