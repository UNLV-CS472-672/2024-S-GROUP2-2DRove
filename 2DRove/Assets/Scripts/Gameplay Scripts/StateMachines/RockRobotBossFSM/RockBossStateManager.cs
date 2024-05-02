using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBossStateManager : MonoBehaviour
{
    public PlayerController playerController;
    [SerializeField] public int goldDropped = 1;
    public float MovementSpeed;
    public float walkAnimSpeed;
    public float rangeAttackSpeed;
    public float attackSpeed;
    public float burstSpeed;
    public float buffSpeed;
    public float rangeAttackDamage = 1f;
    public float attackDamage = 1f;
    public float burstDamage = 1f;
    public float buffDamage = 1f;
    public Animator animator;
    public Transform attackPointX;
    public Transform attackPointY;
    [System.NonSerialized] public float rangeAttackTime;
    [System.NonSerialized] public float attackTime;
    [System.NonSerialized] public float burstTime;
    [System.NonSerialized] public float buffTime;
    [SerializeField] public float attackRange;
    [SerializeField] public float attackHeight;
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
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();
        findAnimationTimes();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("rangeAttackSpeed", rangeAttackSpeed);
        animator.SetFloat("attackSpeed", attackSpeed);
        animator.SetFloat("buffSpeed", buffSpeed);
        animator.SetFloat("burstSpeed", burstSpeed);
        animator.SetFloat("walkAnimSpeed", walkAnimSpeed);
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
                case "Range Attack":
                    rangeAttackTime = clip.length;
                    break;
                case "Attack":
                    attackTime = clip.length;
                    break;
                case "Burst":
                    burstTime = clip.length;
                    break;
                case "Buff":
                    buffTime = clip.length;
                    break;
                default:
                    Debug.Log(clip.name + " is not accounted for.");
                    break;
            }
        }
    }

    
}
