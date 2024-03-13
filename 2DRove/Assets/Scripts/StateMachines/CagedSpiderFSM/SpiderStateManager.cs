using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStateManager : MonoBehaviour
{
    SpiderBaseState currentState;
    public SpiderAggroState AggroState = new SpiderAggroState();
    public SpiderAttackState AttackState = new SpiderAttackState();
    public SpiderDeathState DeathState = new SpiderDeathState();
    public SpiderHitState HitState = new SpiderHitState();
    public SpiderIdleState IdleState = new SpiderIdleState();
    public SpiderSleepState SleepState = new SpiderSleepState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = SleepState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnTriggerStay2D(Collider2D other) {
        currentState.OnTriggerStay2D(this, other);
    }

    public void SwitchState(SpiderBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
