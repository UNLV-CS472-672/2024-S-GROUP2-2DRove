using UnityEngine;

public class RockBossDeathState : RockBossBaseState
{
    
    public override void EnterState(RockBossStateManager RockBoss)
    {
        Debug.Log("Entering Death State...");
        RockBoss.playerController.addCoin(RockBoss.goldDropped);
        RockBoss.animator.SetBool("isDead", true);
        RockBoss.GetComponent<Collider2D>().enabled = false;
        // RockBoss.GetComponent<CapsuleCollider2D>().enabled = false;
        RockBoss.enabled = false;
        // wait for 1 second
        RockBoss.Destroy(2f);

        GameObject thing = GameObject.Find("BossLevel2");
        thing.GetComponent<NextLevelBoss>().deathCheck();
    }

    public override void UpdateState(RockBossStateManager RockBoss)
    {

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

    }
}
