using UnityEngine;
using UnityEngine.UIElements;

public class WidowIdleState : WidowBaseState
{
    private Transform player;
    public override void EnterState(WidowStateManager Widow)
    {
        //Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(WidowStateManager Widow)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the Widow
        //if player is in radius, enter walk state
        Widow.SwitchState(Widow.AggroState);
    }

    public override void OnCollisionEnter2D(WidowStateManager Widow, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(WidowStateManager Widow, Collider2D other) {
    }

    public override void EventTrigger(WidowStateManager Widow)
    {

    }

    public override void TakeDamage(WidowStateManager Widow)
    {
        Widow.SwitchState(Widow.HitState);
    }
}
