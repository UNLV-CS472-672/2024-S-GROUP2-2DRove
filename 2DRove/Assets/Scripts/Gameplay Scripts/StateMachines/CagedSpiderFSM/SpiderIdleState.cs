using UnityEngine;
using UnityEngine.UIElements;

public class SpiderIdleState : SpiderBaseState
{
    private Transform player;
    public override void EnterState(SpiderStateManager Spider)
    {
        //Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(SpiderStateManager Spider)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the Spider
        //if player is in radius, enter walk state
        Spider.SwitchState(Spider.AggroState);
    }

    public override void OnCollisionEnter2D(SpiderStateManager Spider, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(SpiderStateManager Spider, Collider2D other) {
    }

    public override void EventTrigger(SpiderStateManager Spider)
    {

    }

    public override void TakeDamage(SpiderStateManager Spider)
    {
        Spider.SwitchState(Spider.HitState);
    }
}
