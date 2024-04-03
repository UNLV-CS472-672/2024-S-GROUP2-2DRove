using UnityEngine;
using UnityEngine.UIElements;

public class ArcherIdleState : ArcherBaseState
{
    private bool idling = true;
    private Transform player;
    public override void EnterState(ArcherStateManager archer)
    {
        Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(ArcherStateManager archer)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the archer
        //if player is in radius, enter walk state
        archer.SwitchState(archer.AggroState);
    }

    public override void OnCollisionEnter2D(ArcherStateManager archer, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(ArcherStateManager archer, Collider2D other) {
    }

    public override void EventTrigger(ArcherStateManager archer)
    {

    }

    public override void TakeDamage(ArcherStateManager archer)
    {
        archer.SwitchState(archer.HitState);
    }
}
