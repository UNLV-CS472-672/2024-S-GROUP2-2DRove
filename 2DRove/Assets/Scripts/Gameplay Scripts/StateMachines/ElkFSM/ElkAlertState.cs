using UnityEngine;

public class ElkAlertState : ElkBaseState
{
    private Transform player;
    private Animator animator;
    private float alertDuration = 1.0f;
    private float timer;

    public override void EnterState(ElkStateManager elk)
    {
        //Debug.Log("Entering Alert State...");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = elk.GetComponent<Animator>();

        // Set alert animation or parameters
        animator.SetBool("isAlert", true);
        timer = alertDuration;
    }
    public override void UpdateState(ElkStateManager elk)
    {
        if (timer <= 0)
        {
            animator.SetBool("isAlert", false);
            elk.SwitchState(elk.IdleState);
        }
        else
        {
            timer -= Time.deltaTime;
        }

        // Determine the direction to face the player
        var direction = player.position - elk.transform.position;
        var localScale = elk.transform.localScale;
        localScale.x = Mathf.Sign(direction.x) * Mathf.Abs(localScale.x);
        elk.transform.localScale = localScale;
    }


    public override void OnCollisionEnter2D(ElkStateManager elk, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(ElkStateManager elk, Collider2D other)
    {
        if (other.tag == "Player")
        {
            elk.SwitchState(elk.AlertState);
        }
    }

    public override void EventTrigger(ElkStateManager elk)
    {
        elk.SwitchState(elk.DeathState);

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
