using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulStateManager : MonoBehaviour
{
    GhoulBaseState currentState;
    public GhoulAggroState AggroState = new GhoulAggroState();
    public GhoulAttackState AttackState = new GhoulAttackState();
    public GhoulDeathState DeathState = new GhoulDeathState();
    public GhoulHitState HitState = new GhoulHitState();
    public GhoulIdleState IdleState = new GhoulIdleState();
    public GhoulSpawnState SpawnState = new GhoulSpawnState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = SpawnState;
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

    public void SwitchState(GhoulBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
