using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenStateManager : MonoBehaviour
{
    public Animator animator;
    public Transform attackPointX;
    public Transform attackPointY;
    [SerializeField] public float attackRange;
    [SerializeField] public float attackHeight;
    private Mesh attackHitbox;
    WardenBaseState currentState;
    public WardenAggroState AggroState = new WardenAggroState();
    public WardenAttackState AttackState = new WardenAttackState();
    public WardenDeathState DeathState = new WardenDeathState();
    public WardenHitState HitState = new WardenHitState();
    public WardenIdleState IdleState = new WardenIdleState();
    public WardenSpawnState SpawnState = new WardenSpawnState();

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

    public void SwitchState(WardenBaseState state)
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
        if (attackPointX == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPointX.position, attackRange);
        Gizmos.DrawWireSphere(attackPointY.position, attackHeight);
    }
}
