using UnityEngine;

public class WidowDeathState : WidowBaseState
{
    
    public override void EnterState(WidowStateManager Widow)
    {
        Debug.Log("Entering Death State...");
        Widow.playerController.addCoin(Widow.goldDropped);
        Widow.animator.SetBool("isDead", true);
        Widow.GetComponent<Collider2D>().enabled = false;
        // Widow.GetComponent<CapsuleCollider2D>().enabled = false;
        Widow.enabled = false;
        // wait for 1 second
        Widow.Destroy(2f);

        GameObject thing = GameObject.Find("BossLevel3");
        thing.GetComponent<NextLevelBoss>().deathCheck();
    }

    public override void UpdateState(WidowStateManager Widow)
    {

    }

    public override void OnCollisionEnter2D(WidowStateManager Widow, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(WidowStateManager Widow, Collider2D other) {
    }

    public override void EventTrigger(WidowStateManager Widow)
    {

    }

    public override void TakeDamage(WidowStateManager Widow)
    {

    }
}
