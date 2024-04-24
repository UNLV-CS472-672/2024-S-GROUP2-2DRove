using UnityEngine;

public class CagedShockerDeathState : CagedShockerBaseState
{
    
    public override void EnterState(CagedShockerStateManager CagedShocker)
    {
        Debug.Log("Entering Death State...");
        CagedShocker.animator.SetBool("isDead", true);
        // CagedShocker.GetComponent<Collider2D>().enabled = false;
        // CagedShocker.GetComponent<CapsuleCollider2D>().enabled = false;
        CagedShocker.enabled = false;
        // wait for 1 second
        CagedShocker.Destroy(1.5f);
    }

    public override void UpdateState(CagedShockerStateManager CagedShocker)
    {

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

    }
}
