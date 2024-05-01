using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStateManager : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;
    [SerializeField] public int goldDropped = 1;
    public Transform attackPointX;
    public Transform attackPointY;
    [SerializeField] public float attackRangeX;
    [SerializeField] public float attackRangeY;
    ArcherBaseState currentState;
    public ArcherAggroState AggroState = new ArcherAggroState();
    public ArcherAttackState AttackState = new ArcherAttackState();
    public ArcherDeathState DeathState = new ArcherDeathState();
    public ArcherHitState HitState = new ArcherHitState();
    public ArcherIdleState IdleState = new ArcherIdleState();
    public ArcherSpawnState SpawnState = new ArcherSpawnState();

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
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

    public void SwitchState(ArcherBaseState state)
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
        Gizmos.DrawWireSphere(attackPointX.position, attackRangeX);
        Gizmos.DrawWireSphere(attackPointY.position, attackRangeY);
    }
}
