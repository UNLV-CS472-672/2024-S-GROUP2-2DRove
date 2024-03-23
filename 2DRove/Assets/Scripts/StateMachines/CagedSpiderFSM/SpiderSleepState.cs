using UnityEngine;

public class SpiderSleepState : SpiderBaseState
{
    private float spawnTime = 1f;
    public override void EnterState(SpiderStateManager Spider)
    {
        Debug.Log("Spider entering Spawn State...");
    }

    public override void UpdateState(SpiderStateManager Spider)
    {
        if (spawnTime <= 0)
        {
            Spider.SwitchState(Spider.IdleState);
        }

        spawnTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(SpiderStateManager Spider, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(SpiderStateManager Spider, Collider2D other) {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            Spider.SwitchState(Spider.AttackState);
        }
    }
}
