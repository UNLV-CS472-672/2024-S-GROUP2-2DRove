using UnityEngine;
using UnityEngine.UIElements;

public class ElkIdleState : ElkBaseState
{
    private Transform player;
    public override void EnterState(ElkStateManager elk)
    {
        Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();        
        elk.animator.SetFloat("velocity", 0); 
    }

    public override void UpdateState(ElkStateManager elk)
    {

        // If the player is within a certain range, switch to the FleeState
        float distanceToPlayer = Vector3.Distance(elk.transform.position, player.position);
        if (distanceToPlayer < elk.PlayerRange)
        {
            elk.SwitchState(elk.FleeState);
        }
        else
        {
            elk.SwitchState(elk.EatState);
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
