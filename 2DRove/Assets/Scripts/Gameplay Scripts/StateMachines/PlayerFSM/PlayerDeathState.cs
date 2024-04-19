using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    
    public override void EnterState(PlayerStateManager Player)
    {
        Debug.Log("Entering Death State...");
        Player.animator.SetBool("isDead", true);
        Player.GetComponent<Collider2D>().enabled = false;
        // Player.GetComponent<CapsuleCollider2D>().enabled = false;
        Player.enabled = false;
        // wait for 1 second
        Player.Destroy(.81f);
    }

    public override void UpdateState(PlayerStateManager Player)
    {

    }

    public override void OnCollisionEnter2D(PlayerStateManager Player, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(PlayerStateManager Player, Collider2D other) {
    }

    public override void EventTrigger(PlayerStateManager Player)
    {

    }

    public override void TakeDamage(PlayerStateManager Player)
    {

    }
}
