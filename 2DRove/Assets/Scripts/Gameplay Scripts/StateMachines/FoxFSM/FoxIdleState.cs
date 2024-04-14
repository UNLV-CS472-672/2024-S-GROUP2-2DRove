using UnityEngine;
using UnityEngine.UIElements;

public class FoxIdleState : FoxBaseState
{
    private Transform player;
    public override void EnterState(FoxStateManager fox)
    {
        Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();        
        fox.animator.SetFloat("velocity", 0); 
    }

    public override void UpdateState(FoxStateManager fox)
    {

        // If the player is within a certain range, switch to the FleeState
        float distanceToPlayer = Vector3.Distance(fox.transform.position, player.position);
        if (distanceToPlayer < fox.PlayerRange)
        {
            fox.SwitchState(fox.FleeState);
        }
        else
        {
            fox.SwitchState(fox.EatState);
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
