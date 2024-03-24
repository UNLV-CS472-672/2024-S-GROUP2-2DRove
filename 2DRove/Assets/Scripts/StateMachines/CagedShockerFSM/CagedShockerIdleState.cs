using UnityEngine;
using UnityEngine.UIElements;

public class CagedShockerIdleState : CagedShockerBaseState
{
    private bool idling = true;
    private Transform player;
    public override void EnterState(CagedShockerStateManager CagedShocker)
    {
        Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(CagedShockerStateManager CagedShocker)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the CagedShocker
        //if player is in radius, enter walk state
        CagedShocker.SwitchState(CagedShocker.AggroState);
    }

    public override void OnCollisionEnter2D(CagedShockerStateManager CagedShocker, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(CagedShockerStateManager CagedShocker, Collider2D other) {
    }

    public override void EventTrigger(CagedShockerStateManager CagedShocker)
    {

    }

    public override void TakeDamage(CagedShockerStateManager CagedShocker)
    {
        CagedShocker.SwitchState(CagedShocker.HitState);
    }
}
