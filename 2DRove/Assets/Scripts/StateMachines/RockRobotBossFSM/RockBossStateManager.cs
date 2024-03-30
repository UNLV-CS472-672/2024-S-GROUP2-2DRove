using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBossStateManager : MonoBehaviour
{
    public Animator animator;
    public Transform attackPointX;
    public Transform attackPointY;
    [SerializeField] public float attackRange;
    [SerializeField] public float attackHeight;
    private Mesh attackHitbox;
    RockBossBaseState currentState;
    public RockBossAggroState AggroState = new RockBossAggroState();
    public RockBossAttackState AttackState = new RockBossAttackState();
    public RockBossDeathState DeathState = new RockBossDeathState();
    public RockBossHitState HitState = new RockBossHitState();
    public RockBossIdleState IdleState = new RockBossIdleState();
    public RockBossSpawnState SpawnState = new RockBossSpawnState();

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

    public void SwitchState(RockBossBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void EventTrigger()
    {
        currentState.EventTrigger(this);
    }
    public void TeleportToRandomPosition(RockBossStateManager RockBoss)
    {
        // Define the range of teleportation around the current position
        float teleportRangeX = 10f; 
        float teleportRangeY = 5f; 

        // Generate a random new position within the defined range
        Vector3 randomPosition = new Vector3(Random.Range(-teleportRangeX, teleportRangeX), Random.Range(-teleportRangeY, teleportRangeY), 0) + RockBoss.transform.position;

        // Update the RockBoss's position to the new random position
        RockBoss.transform.position = randomPosition;
    }

//damage dealt is calculated by PlayerController.cs
//this is purely to be placed in hit stun
    public void TakeDamageAnimation()
    {
        currentState.TakeDamage(this);
        TeleportToRandomPosition(this);
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
