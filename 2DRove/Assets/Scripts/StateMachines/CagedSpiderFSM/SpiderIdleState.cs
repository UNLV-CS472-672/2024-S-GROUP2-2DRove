using UnityEngine;
using UnityEngine.UIElements;

public class SpiderIdleState : SpiderBaseState
{
    private bool idling = true;
    private Transform player;
    public override void EnterState(SpiderStateManager spider)
    {
        Debug.Log("Entering Wake State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(SpiderStateManager spider)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the spider
        //if player is in radius, enter walk state
        spider.SwitchState(spider.AggroState);
    }

    public override void OnCollisionEnter2D(SpiderStateManager spider, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(SpiderStateManager spider, Collider2D other) {
    }

    public override void EventTrigger(SpiderStateManager spider)
    {

    }

    public override void TakeDamage(SpiderStateManager spider)
    {
        spider.SwitchState(spider.HitState);
    }
}
