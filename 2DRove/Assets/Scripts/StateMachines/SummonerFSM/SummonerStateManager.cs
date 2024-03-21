using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerStateManager : MonoBehaviour
{
    public Animator animator;
    SummonerBaseState currentState;
    public SummonerAggroState AggroState = new SummonerAggroState();
    // public SummonerAttackState AttackState = new SummonerAttackState(); // Summoner does not Attack
    public SummonerDeathState DeathState = new SummonerDeathState();
    public SummonerHitState HitState = new SummonerHitState();
    public SummonerSummoningState SummoningState = new SummonerSummoningState();
    public SummonerIdleState IdleState = new SummonerIdleState();
    public SummonerSpawnState SpawnState = new SummonerSpawnState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnTriggerStay2D(Collider2D other) {
        currentState.OnTriggerStay2D(this, other);
    }
    
    public void SwitchState(SummonerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void EventTrigger()
    {
        currentState.EventTrigger(this);
    }

    //damage dealt is calculated by PlayerController.cs
    //this is purely to be placed in hit stun
    public void TakeDamageAnimation()
    {
        currentState.TakeDamage(this);
    }

    public void Destroy(float waitDuration)
    {
        Destroy(gameObject, waitDuration);
    }
}
