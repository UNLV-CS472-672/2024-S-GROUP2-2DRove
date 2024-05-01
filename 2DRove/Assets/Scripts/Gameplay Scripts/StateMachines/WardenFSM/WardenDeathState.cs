using UnityEngine;

public class WardenDeathState : WardenBaseState
{
    
    public override void EnterState(WardenStateManager Warden)
    {
        //Debug.Log("Entering Death State...");
        Warden.playerController.addCoin(Warden.goldDropped);
        Warden.animator.SetBool("isDead", true);
        Warden.GetComponent<Collider2D>().enabled = false;
        // Warden.GetComponent<CapsuleCollider2D>().enabled = false;
        Warden.enabled = false;
        // wait for 1 second
        Warden.Destroy(1.5f);
    }

    public override void UpdateState(WardenStateManager Warden)
    {

    }

    public override void OnCollisionEnter2D(WardenStateManager Warden, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(WardenStateManager Warden, Collider2D other) {
    }

    public override void EventTrigger(WardenStateManager Warden)
    {

    }

    public override void TakeDamage(WardenStateManager Warden)
    {

    }
}
