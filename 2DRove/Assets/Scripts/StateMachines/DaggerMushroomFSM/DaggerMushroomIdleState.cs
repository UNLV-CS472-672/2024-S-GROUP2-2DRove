using UnityEngine;
using UnityEngine.UIElements;

public class DaggerMushroomIdleState : DaggerMushroomBaseState
{
    private bool idling = true;
    private Transform player;
    public override void EnterState(DaggerMushroomStateManager mushroom)
    {
        Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(DaggerMushroomStateManager mushroom)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the mushroom
        //if player is in radius, enter walk state
        mushroom.SwitchState(mushroom.AggroState);
    }

    public override void OnCollisionEnter2D(DaggerMushroomStateManager mushroom, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(DaggerMushroomStateManager mushroom, Collider2D other) {
    }

    public override void EventTrigger(DaggerMushroomStateManager mushroom)
    {

    }

    public override void TakeDamage(DaggerMushroomStateManager mushroom)
    {
        mushroom.SwitchState(mushroom.HitState);
    }
}
