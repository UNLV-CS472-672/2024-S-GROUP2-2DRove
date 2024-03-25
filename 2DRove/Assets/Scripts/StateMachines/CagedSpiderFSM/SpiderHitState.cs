using UnityEngine;

public class SpiderHitState : SpiderBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = .41f;
    public override void EnterState(SpiderStateManager spider)
    {
        Debug.Log("Entering Hit State...");
        animator = spider.GetComponent<Animator>();
        //set animation bool hitstunn to true or smth
        // NO NEED TO SET TRIGGER bc its done in NewEnemy for now
        // animator.SetTrigger("hit");

        health = spider.GetComponent<NewEnemy>().CurrentHeath();
        if (health <= 0)
        {
            spider.SwitchState(spider.DeathState);
        }
    }

    public override void UpdateState(SpiderStateManager spider)
    {
        if (hitStun <= 0)
        {
            spider.SwitchState(spider.IdleState);
        }

    }

    public override void OnCollisionEnter2D(SpiderStateManager spider, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(SpiderStateManager spider, Collider2D other) {
    }

    public override void EventTrigger(SpiderStateManager spider)
    {

    }

    public override void TakeDamage(SpiderStateManager spider)
    {
        spider.SwitchState(spider.HitState);
    }
}
