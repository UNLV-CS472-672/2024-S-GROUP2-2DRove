using UnityEngine;

public class SpiderHitState : SpiderBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = .41f;
    public override void EnterState(SpiderStateManager Spider)
    {
        Debug.Log("Entering Hit State...");
        animator = Spider.GetComponent<Animator>();
        //set animation bool hitstunn to true or smth
        // NO NEED TO SET TRIGGER bc its done in NewEnemy for now
        // animator.SetTrigger("hit");

        health = Spider.GetComponent<NewEnemy>().CurrentHeath();
        if (health <= 0)
        {
            Spider.SwitchState(Spider.DeathState);
        }
    }

    public override void UpdateState(SpiderStateManager Spider)
    {
        if (hitStun <= 0)
        {
            Spider.SwitchState(Spider.IdleState);
        }

        hitStun -= Time.deltaTime;
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
        Spider.SwitchState(Spider.HitState);
    }
}
