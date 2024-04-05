using UnityEngine;
using UnityEngine.UIElements;

public class ArcherIdleState : ArcherBaseState
{
    private bool idling = true;
    private Transform player;
    private float idleTime = 1.0f;
    public override void EnterState(ArcherStateManager archer)
    {
        Debug.Log("Entering Idle State...");
        idleTime = 1.0f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(ArcherStateManager archer)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the archer
        //if player is in radius, enter walk state
        if(idleTime <= 0)
        {
            archer.SwitchState(archer.AggroState);
        }
        idleTime -= Time.deltaTime;
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
