using UnityEngine;
using UnityEngine.UIElements;

public class GuardianIdleState : GuardianBaseState
{
    private Transform player;
    public override void EnterState(GuardianStateManager Guardian)
    {
        Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(GuardianStateManager Guardian)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the Guardian
        //if player is in radius, enter walk state
        Guardian.SwitchState(Guardian.AggroState);
    }

    public override void OnCollisionEnter2D(GuardianStateManager Guardian, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(GuardianStateManager Guardian, Collider2D other) {
    }

    public override void EventTrigger(GuardianStateManager Guardian, int attackID)
    {

    }

    public override void TakeDamage(GuardianStateManager Guardian)
    {
        // Guardian.SwitchState(Guardian.HitState);
        float health = Guardian.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            Guardian.SwitchState(Guardian.DeathState);
        }
    }
}
