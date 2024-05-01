using UnityEngine;

public class SummonerDeathState : SummonerBaseState
{
    public override void EnterState(SummonerStateManager summoner)
    {
        Debug.Log("Summoner: Entering Death State...");
        summoner.playerController.addCoin(summoner.goldDropped);
        summoner.animator.SetBool("isDead", true);

        // Disabling the collider so it no longer interacts with other game objects
        summoner.GetComponent<Collider2D>().enabled = false;
        summoner.GetComponent<CapsuleCollider2D>().enabled = false;
        
        summoner.enabled = false;

        // wait for 1 second
        summoner.Destroy(.81f);
    }
    public override void UpdateState(SummonerStateManager summoner)
    {
        
    }

    public override void OnCollisionEnter2D(SummonerStateManager summoner, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(SummonerStateManager summoner, Collider2D other) 
    {
        
    }

    public override void EventTrigger(SummonerStateManager summoner)
    {

    }

    public override void TakeDamage(SummonerStateManager summoner)
    {
       
    }
}