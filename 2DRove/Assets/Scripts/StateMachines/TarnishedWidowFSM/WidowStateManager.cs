using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidowStateManager : MonoBehaviour
{
    public Animator animator;
    public Transform attackPointX;
    public Transform attackPointY;
    [SerializeField] public float attackRange;
    [SerializeField] public float attackHeight;
    private Mesh attackHitbox;
    WidowBaseState currentState;
    public WidowAggroState AggroState = new WidowAggroState();
    public WidowAttackState AttackState = new WidowAttackState();
    public WidowDeathState DeathState = new WidowDeathState();
    public WidowHitState HitState = new WidowHitState();
    public WidowIdleState IdleState = new WidowIdleState();
    public WidowSpawnState SpawnState = new WidowSpawnState();

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

    public void SwitchState(WidowBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void EventTrigger()
    {
        currentState.EventTrigger(this);
    }
    public void TeleportToRandomPosition(WidowStateManager Widow)
    {
        // Define the range of teleportation around the current position
        float teleportRangeX = 10f; 
        float teleportRangeY = 5f; 

        // Generate a random new position within the defined range
        Vector3 randomPosition = new Vector3(Random.Range(-teleportRangeX, teleportRangeX), Random.Range(-teleportRangeY, teleportRangeY), 0) + Widow.transform.position;

        // Update the Widow's position to the new random position
        Widow.transform.position = randomPosition;
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
