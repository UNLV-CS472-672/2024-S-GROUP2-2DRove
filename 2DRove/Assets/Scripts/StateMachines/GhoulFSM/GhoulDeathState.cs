using UnityEngine;

public class GhoulDeathState : GhoulBaseState
{
    
    public override void EnterState(GhoulStateManager ghoul)
    {
        Debug.Log("Entering Death State...");
    }

    public override void UpdateState(GhoulStateManager ghoul)
    {

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
