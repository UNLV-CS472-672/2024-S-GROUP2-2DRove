using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStateManager : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    [SerializeField] public float attackRange;
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
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        currentState.OnCollisionEnter2D(this, other);
    }

    private void OnTriggerStay2D(Collider2D other) {
        currentState.OnTriggerStay2D(this, other);
    }

    public void SwitchState(SpiderBaseState state)
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

    private void OnDrawGizmosSelected(){
        if (attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void CollisionTesting(Collision2D collision2D)
    {
        OnCollisionEnter2D(collision2D);
    }

    public void TriggerTesting(Collider2D collider2D)
    {
        OnTriggerStay2D(collider2D);
    }
}
