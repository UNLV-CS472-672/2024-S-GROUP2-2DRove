using UnityEngine;


public class SummonerHitState : SummonerBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = .41f;

// Error missing Updated version NewEnemy.cs
    public override void EnterState(SummonerStateManager summoner)
    {

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
        summoner.SwitchState(summoner.HitState);
    }
}



/* ERROR 
public class SummonerHitState : SummonerBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = .41f;
    private NewEnemy summonerHealthComponent;


    public override void EnterState(SummonerStateManager summoner)
    {
        Debug.Log("Entering Hit State...");
        animator = summoner.GetComponent<Animator>();
        //set animation bool hitstunn to true or smth
        // NO NEED TO SET TRIGGER bc its done in NewEnemy for now
        // animator.SetTrigger("hit");

        health = summoner.GetComponent<NewEnemy>().CurrentHealth();
        if (health <= 0)
        {
            summoner.SwitchState(summoner.DeathState);
        }
    }

    public override void UpdateState(SummonerStateManager summoner)
    {
        if (hitStun <= 0)
        {
            // After hit stun duration passes, return to previous state
            summoner.SwitchState(summoner.IdleState);
        }
        else
        {
            // Decrement hit stun duration over time
            hitStun -= Time.deltaTime;
        }
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
         summoner.SwitchState(summoner.HitState);
    }

}

*/