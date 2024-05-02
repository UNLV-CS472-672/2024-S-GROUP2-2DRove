using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerMushroomStateManager : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;
    [SerializeField] public int goldDropped = 1;
    public Transform attackPoint;
    [SerializeField] public float attackRange;
    DaggerMushroomBaseState currentState;
    public DaggerMushroomAggroState AggroState = new DaggerMushroomAggroState();
    public DaggerMushroomAttackState AttackState = new DaggerMushroomAttackState();
    public DaggerMushroomDeathState DeathState = new DaggerMushroomDeathState();
    public DaggerMushroomHitState HitState = new DaggerMushroomHitState();
    public DaggerMushroomIdleState IdleState = new DaggerMushroomIdleState();
    public DaggerMushroomSpawnState SpawnState = new DaggerMushroomSpawnState();
    private Transform player;
    public float attackDamage = 1f;
    public float movementSpeed = 1f;
    public float attackSpeed = 1f;
    [System.NonSerialized] public float attackTime;
    public float lastAttack = 0f;
    public float attackCD = 2f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        findAnimationTimes();
        animator.SetFloat("attackSpeed", attackSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        if((this.transform.position - player.position).magnitude > 75)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        currentState.OnTriggerStay2D(this, other);
    }

    public void SwitchState(DaggerMushroomBaseState state)
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
    private void findAnimationTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "airAttack":
                    attackTime = clip.length;
                    break;
                default:
                    Debug.Log(clip.name + " is not accounted for.");
                    break;
            }
        }
    }
}
