using UnityEngine;

public class GuardianDeathState : GuardianBaseState
{

    public override void EnterState(GuardianStateManager Guardian)
    {
        Debug.Log("Entering Death State...");
        Guardian.playerController.addCoin(Guardian.goldDropped);
        Guardian.animator.SetBool("isDead", true);
        Guardian.GetComponent<Collider2D>().enabled = false;
        // Guardian.GetComponent<CapsuleCollider2D>().enabled = false;
        Guardian.enabled = false;
        // wait for 1 second
        Guardian.Destroy(1.5f);

        GameObject thing = GameObject.Find("BossLevel1");
        thing.GetComponent<NextLevelBoss>().deathCheck();
    }  

    public override void UpdateState(GuardianStateManager Guardian)
    {

    }

    public override void OnCollisionEnter2D(GuardianStateManager Guardian, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(GuardianStateManager Guardian, Collider2D other) {
    }

    public override void EventTrigger(GuardianStateManager Guardian, int attackID)
    {

    }

    public override void TakeDamage(GuardianStateManager Guardian)
    {

    }
}
