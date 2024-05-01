using UnityEngine;

public class FoxAlertState : FoxBaseState
{
    private Transform player;
    private Animator animator;
    private float alertDuration = 0.5f;
    private float timer;

    public override void EnterState(FoxStateManager fox)
    {
        //Debug.Log("Entering Alert State...");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = fox.GetComponent<Animator>();

        // Set alert animation or parameters
        animator.SetBool("isAlert", true);

        timer = alertDuration;
    }
    public override void UpdateState(FoxStateManager fox)
    {
        if (timer <= 0)
        {
            animator.SetBool("isAlert", false);
            fox.SwitchState(fox.IdleState);
        }
        else
        {
            timer -= Time.deltaTime;
            fox.SwitchState(fox.FleeState);
        }

        // Determine the direction to face the player
        var direction = player.position - fox.transform.position;
        var localScale = fox.transform.localScale;
        localScale.x = Mathf.Sign(direction.x) * Mathf.Abs(localScale.x);
        fox.transform.localScale = localScale;
    }


    public override void OnCollisionEnter2D(FoxStateManager fox, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(FoxStateManager fox, Collider2D other)
    {
        if (other.tag == "Player")
        {
            fox.SwitchState(fox.AlertState);
        }
    }

    public override void EventTrigger(FoxStateManager fox)
    {
        fox.SwitchState(fox.DeathState);

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
