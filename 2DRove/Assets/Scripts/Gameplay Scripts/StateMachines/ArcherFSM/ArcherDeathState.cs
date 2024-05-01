using UnityEngine;

public class ArcherDeathState : ArcherBaseState
{
    
    public override void EnterState(ArcherStateManager archer)
    {
        Debug.Log("Entering Death State...");
        archer.playerController.addCoin(archer.goldDropped);
        archer.animator.SetBool("isDead", true);
        archer.GetComponent<Collider2D>().enabled = false;
        // archer.GetComponent<CapsuleCollider2D>().enabled = false;
        archer.enabled = false;
        // wait for 1 second
        archer.Destroy(.81f);
    }

    public override void UpdateState(ArcherStateManager archer)
    {

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

    }
}
