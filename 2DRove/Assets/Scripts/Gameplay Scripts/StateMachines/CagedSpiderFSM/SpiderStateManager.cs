using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderStateManager : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;
    [SerializeField] public int goldDropped = 1;
    public Transform attackPoint;
    [SerializeField] public float attackRange;
    SpiderBaseState currentState;
    public SpiderAggroState AggroState = new SpiderAggroState();
    public SpiderAttackState AttackState = new SpiderAttackState();
    public SpiderDeathState DeathState = new SpiderDeathState();
    public SpiderHitState HitState = new SpiderHitState();
    public SpiderIdleState IdleState = new SpiderIdleState();
    public SpiderSleepState SpawnState = new SpiderSleepState();
    private Transform player;
    public float attackDamage = 1f;
    public float movementSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        if((this.transform.position - player.position).magnitude > 50)
        {
            Destroy(gameObject);
        }
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
}
