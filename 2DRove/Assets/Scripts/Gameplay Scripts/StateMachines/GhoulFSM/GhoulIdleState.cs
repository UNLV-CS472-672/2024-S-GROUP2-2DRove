using UnityEngine;
using UnityEngine.UIElements;

public class GhoulIdleState : GhoulBaseState
{
    private Transform player;
    public override void EnterState(GhoulStateManager ghoul)
    {
        Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(GhoulStateManager ghoul)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the ghoul
        //if player is in radius, enter walk state
        ghoul.SwitchState(ghoul.AggroState);
    }

    public override void OnCollisionEnter2D(GhoulStateManager ghoul, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(GhoulStateManager ghoul, Collider2D other) {
    }

    public override void EventTrigger(GhoulStateManager ghoul)
    {

    }

    public override void TakeDamage(GhoulStateManager ghoul)
    {
        ghoul.SwitchState(ghoul.HitState);
    }
}
