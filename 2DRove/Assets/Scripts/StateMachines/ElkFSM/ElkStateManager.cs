using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElkStateManager : MonoBehaviour

{
    public ElkIdleState IdleState = new ElkIdleState();
    public ElkHitState HitState = new ElkHitState();
    public ElkEatState EatState = new ElkEatState();
    public ElkSpawnState SpawnState = new ElkSpawnState();
    public ElkDeathState DeathState = new ElkDeathState();
    public ElkAlertState AlertState = new ElkAlertState();

    private Mesh ElkHitbox;
    private ElkBaseState currentState;

    public Animator animator;
    [SerializeField] private Transform player;
    [SerializeField] public float playerRange; // Distance to the player 

    public Transform Player => player; // Public getter for the player's transform
    public float PlayerRange => playerRange; // Public getter for the player's transform


    void Start()
    {
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        currentState.OnTriggerStay2D(this, other);
    }

    public void SwitchState(ElkBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void EventTrigger()
    {
        currentState.EventTrigger(this);
    }

    // This would handle the elk take damage
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
        // draw range of Elk eating range
        // Gizmos.DrawWireSphere(playerRange)
        Gizmos.DrawWireSphere(transform.position, playerRange);

    }
}
