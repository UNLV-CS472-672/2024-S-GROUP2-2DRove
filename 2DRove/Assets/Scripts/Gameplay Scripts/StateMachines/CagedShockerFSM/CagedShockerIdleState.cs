using UnityEngine;
using UnityEngine.UIElements;

public class CagedShockerIdleState : CagedShockerBaseState
{
    private Transform player;
    private float idleTime = 2f;

    public override void EnterState(CagedShockerStateManager CagedShocker)
    {
        //Debug.Log("Entering Idle State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        idleTime = 2f;
    }

    public override void UpdateState(CagedShockerStateManager CagedShocker)
    {
        if (idleTime < 0)
        {
            CagedShocker.SwitchState(CagedShocker.Lurch1State);
        }

        idleTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(CagedShockerStateManager CagedShocker, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(CagedShockerStateManager CagedShocker, Collider2D other) {
    }

    public override void EventTrigger(CagedShockerStateManager CagedShocker)
    {

    }

    public override void TakeDamage(CagedShockerStateManager CagedShocker)
    {
        CagedShocker.SwitchState(CagedShocker.HitState);
    }
}
