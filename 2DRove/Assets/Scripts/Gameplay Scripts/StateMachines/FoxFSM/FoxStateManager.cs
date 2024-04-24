using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxStateManager : MonoBehaviour

{
    public FoxIdleState IdleState = new FoxIdleState();
    public FoxEatState EatState = new FoxEatState();
    public FoxSpawnState SpawnState = new FoxSpawnState();
    public FoxDeathState DeathState = new FoxDeathState();
    public FoxAlertState AlertState = new FoxAlertState();
    public FoxFleeState FleeState = new FoxFleeState();

    private Mesh FoxHitbox;
    private FoxBaseState currentState;

    public Animator animator;
    [SerializeField] public Transform player;
    [SerializeField] public float playerRange;


    public Transform Player;
    public float PlayerRange => playerRange;


    void Start()
    {
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        currentState.OnCollisionEnter2D(this, other);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        currentState.OnTriggerStay2D(this, other);
    }

    public void SwitchState(FoxBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void EventTrigger()
    {
        currentState.EventTrigger(this);
    }

    // This would handle the fox take damage
    public void TakeDamageAnimation()
    {
        currentState.TakeDamage(this);
    }


    public void TakeDamage(int damage)
    {
        currentState.TakeDamage(this);
    }

    public void Destroy(float waitDuration)
    {
        Destroy(gameObject, waitDuration);
    }

    private void OnDrawGizmosSelected()
    {
        // draw range of Fox eating range
        // Gizmos.DrawWireSphere(playerRange)
        Gizmos.DrawWireSphere(transform.position, playerRange);

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
