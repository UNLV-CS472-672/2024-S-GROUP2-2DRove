using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElkStateManager : MonoBehaviour

{
    [SerializeField] public int goldDropped = 1;
    public ElkIdleState IdleState = new ElkIdleState();
    public ElkEatState EatState = new ElkEatState();
    public ElkSpawnState SpawnState = new ElkSpawnState();
    public ElkDeathState DeathState = new ElkDeathState();
    public ElkAlertState AlertState = new ElkAlertState();
    public ElkFleeState FleeState = new ElkFleeState();

    private Mesh ElkHitbox;
    private ElkBaseState currentState;

    public Animator animator;
    [SerializeField] public float playerRange;

    public Transform Player;
    public PlayerController playerController;
    public float PlayerRange => playerRange; 
    public float movementSpeed = 1f;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();
        Player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        currentState.UpdateState(this);
        if((this.transform.position - Player.position).magnitude > 75)
        {
            Destroy(gameObject);
        }
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
