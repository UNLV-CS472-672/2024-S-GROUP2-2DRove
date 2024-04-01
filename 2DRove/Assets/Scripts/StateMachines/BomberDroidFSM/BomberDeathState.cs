using UnityEngine;

public class BomberDeathState : BomberBaseState
{

    public override void EnterState(BomberStateManager bomber)
    {
        Debug.Log("Entering Death State...");
        bomber.animator.SetBool("isDead", true);
        bomber.GetComponent<Collider2D>().enabled = false;
        // bomber.GetComponent<CapsuleCollider2D>().enabled = false;
        bomber.enabled = false;
        // wait for 1 second
        bomber.Destroy(1f);
    }

    public override void UpdateState(BomberStateManager bomber)
    {

    }

    public override void OnCollisionEnter2D(BomberStateManager bomber, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(BomberStateManager bomber, Collider2D other)
    {
    }

    public override void EventTrigger(BomberStateManager bomber)
    {

    }

    public override void TakeDamage(BomberStateManager bomber)
    {

    }
}
