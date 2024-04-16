using UnityEngine;
using UnityEngine.UIElements;

public class RockBossIdleState : RockBossBaseState
{
    private Transform player;
    public override void EnterState(RockBossStateManager RockBoss)
    {
        Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(RockBossStateManager RockBoss)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the RockBoss
        //if player is in radius, enter walk state
        RockBoss.SwitchState(RockBoss.AggroState);
    }

    public override void OnCollisionEnter2D(RockBossStateManager RockBoss, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(RockBossStateManager RockBoss, Collider2D other) {
    }

    public override void EventTrigger(RockBossStateManager RockBoss)
    {

    }

    public override void TakeDamage(RockBossStateManager RockBoss)
    {
        RockBoss.SwitchState(RockBoss.HitState);
    }
}
