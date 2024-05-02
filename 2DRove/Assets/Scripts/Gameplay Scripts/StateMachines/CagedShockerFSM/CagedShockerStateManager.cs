using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagedShockerStateManager : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;
    [SerializeField] public int goldDropped = 1;
    public Transform attackPointX;
    public Transform attackPointY;
    [SerializeField] public float attackRange;
    [SerializeField] public float attackHeight;
    private Mesh attackHitbox;
    CagedShockerBaseState currentState;
    // public CagedShockerAggroState AggroState = new CagedShockerAggroState();
    public CagedShockerAttackState AttackState = new CagedShockerAttackState();
    public CagedShockerDeathState DeathState = new CagedShockerDeathState();
    public CagedShockerHitState HitState = new CagedShockerHitState();
    public CagedShockerIdleState IdleState = new CagedShockerIdleState();
    public CagedShockerSpawnState SpawnState = new CagedShockerSpawnState();
    public CagedShockerLurch1State Lurch1State = new CagedShockerLurch1State();
    public CagedShockerLurch2State Lurch2State = new CagedShockerLurch2State();
    private Transform player;
    public float attackDamage = 1f;
    public float lurch1Dist = 1f;
    public float lurch2Dist = 1f;
    public float attackSpeed = 1f;
    [System.NonSerialized] public float attackTime;

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

    public void SwitchState(CagedShockerBaseState state)
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

    private void findAnimationTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Attack":
                    attackTime = clip.length;
                    break;
                default:
                    Debug.Log(clip.name + " is not accounted for.");
                    break;
            }
        }
    }
}
