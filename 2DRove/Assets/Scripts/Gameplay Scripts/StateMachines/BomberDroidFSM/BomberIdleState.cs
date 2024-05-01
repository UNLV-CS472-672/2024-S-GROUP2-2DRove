using UnityEngine;
using UnityEngine.UIElements;

public class BomberIdleState : BomberBaseState
{
    private bool idling = true;
    private Transform player;
    public override void EnterState(BomberStateManager bomber)
    {
        //Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(BomberStateManager bomber)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the bomber
        //if player is in radius, enter walk state
        bomber.SwitchState(bomber.AggroState);
    }

    public override void OnCollisionEnter2D(BomberStateManager bomber, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(BomberStateManager bomber, Collider2D other) {
    }

    public override void EventTrigger(BomberStateManager bomber)
    {

    }

    public override void TakeDamage(BomberStateManager bomber)
    {
        bomber.SwitchState(bomber.HitState);
    }
}
