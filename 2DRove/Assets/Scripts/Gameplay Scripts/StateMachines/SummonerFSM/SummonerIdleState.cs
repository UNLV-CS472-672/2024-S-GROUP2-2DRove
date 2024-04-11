using UnityEngine;
using UnityEngine.UIElements;

public class SummonerIdleState : SummonerBaseState
{
    private bool idling = true;
    private Transform player;
    public override void EnterState(SummonerStateManager summoner)
    {
        Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateState(SummonerStateManager summoner)
    {
        //make them just walk around randomly
        //make a radius fov? that seems pretty cool, and it would have a faint ring around the summoner
        //if player is in radius, enter walk state
        summoner.SwitchState(summoner.AggroState);
    }

    public override void OnCollisionEnter2D(SummonerStateManager summoner, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(SummonerStateManager summoner, Collider2D other) {
    }

    public override void EventTrigger(SummonerStateManager summoner)
    {

    }

    public override void TakeDamage(SummonerStateManager summoner)
    {
        summoner.SwitchState(summoner.HitState);
    }
}

/*    With Radius

private Transform player;
    private float detectionRadius = 5f;
    private LayerMask playerLayer;
    private bool playerDetected = false;

    public override void EnterState(SummonerStateManager summoner)
    {
        Debug.Log("Summoner: Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerLayer = LayerMask.GetMask("Player");
    }

    public override void UpdateState(SummonerStateManager summoner)
    {
        // Check for player within detection radius
        playerDetected = Physics2D.OverlapCircle(summoner.transform.position, detectionRadius, playerLayer);

        if (playerDetected)
        {
            summoner.SwitchState(summoner.AggroState);
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