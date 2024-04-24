using UnityEngine;

public class SpiderDeathState : SpiderBaseState
{
    
    public override void EnterState(SpiderStateManager Spider)
    {
        Debug.Log("Entering Death State...");
        Spider.animator.SetBool("isDead", true);
        // Spider.GetComponent<Collider2D>().enabled = false;
        // Spider.GetComponent<CapsuleCollider2D>().enabled = false;
        Spider.enabled = false;
        // wait for 1 second
        Spider.Destroy(.81f);
    }

    public override void UpdateState(SpiderStateManager Spider)
    {

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

    }
}
