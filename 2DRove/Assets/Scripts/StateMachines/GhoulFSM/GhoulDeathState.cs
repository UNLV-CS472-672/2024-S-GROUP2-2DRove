using UnityEngine;

public class GhoulDeathState : GhoulBaseState
{
    
    public override void EnterState(GhoulStateManager ghoul)
    {
        Debug.Log("Entering Death State...");
        ghoul.animator.SetBool("isDead", true);
        ghoul.GetComponent<Collider2D>().enabled = false;
        // ghoul.GetComponent<CapsuleCollider2D>().enabled = false;
        ghoul.enabled = false;
        // wait for 1 second
        ghoul.Destroy(.81f);
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

    }
}
