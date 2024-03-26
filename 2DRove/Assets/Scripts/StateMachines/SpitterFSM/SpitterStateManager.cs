using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterStateManager : MonoBehaviour
{
    SpitterBaseState currentState;
    public Animator animator;

    public SpitterSpawnState SpawnState = new SpitterSpawnState();
    public SpitterIdleState IdleState = new SpitterIdleState();
    public SpitterAggroState AggroState = new SpitterAggroState();
    public SpitterAttackState AttackState = new SpitterAttackState();
    public SpitterHitState HitState = new SpitterHitState();
    public SpitterDeathState DeathState = new SpitterDeathState();

    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public Transform projectileSpawnPoint;

    public GameObject ProjectilePrefab => projectilePrefab;
    public Transform ProjectileSpawnPoint => projectileSpawnPoint;

    [SerializeField] public float attackRange;
    public Transform attackPoint;


    void Start()
    {
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();

        // AttackState.Setup(this, projectilePrefab, projectileSpawnPoint);
        if (projectilePrefab == null || projectileSpawnPoint == null)
        {
            Debug.LogError("Projectile Prefab or Projectile Spawn Point not assigned in SpitterStateManager");
            return;
        }


    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        currentState.OnTriggerStay2D(this, other);
    }
    public void SwitchState(SpitterBaseState state)
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
        // currentState = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
