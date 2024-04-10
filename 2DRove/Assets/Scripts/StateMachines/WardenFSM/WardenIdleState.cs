using UnityEngine;
using UnityEngine.UIElements;

public class WardenIdleState : WardenBaseState
{
    private bool idling = true;
    private Transform player;
    public override void EnterState(WardenStateManager Warden)
    {
        Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(WardenStateManager Warden)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the Warden
        //if player is in radius, enter walk state
        Warden.SwitchState(Warden.AggroState);
    }

    public override void OnCollisionEnter2D(WardenStateManager Warden, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(WardenStateManager Warden, Collider2D other) {
    }

    public override void EventTrigger(WardenStateManager Warden)
    {

    }

    public override void TakeDamage(WardenStateManager Warden)
    {
        Warden.SwitchState(Warden.HitState);
    }
}
