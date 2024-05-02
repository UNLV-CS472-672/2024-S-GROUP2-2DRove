using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberStateManager : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;
    [SerializeField] public int goldDropped = 1;
    public Transform attackPointX;
    public Transform attackPointY;
    [SerializeField] public float attackRange; // Distance of the attack 
     [SerializeField] private float attackHeight = 3f; // The point from where bombs will be dropped
    [SerializeField] private float moveSpeed = 3f; // Default speed
    [SerializeField] private int attackDamage = 5; // Default damage value    
    public float MoveSpeed => moveSpeed; // Public getter for moveSpeed so it can be accessed but not directly modified by states
    public int AttackDamage => attackDamage; // Public getter to access the damage value
    public Transform Player; // Public getter for the player's transform
    public float AttackHeight => attackHeight; // Public getter for the attack height


    private Mesh bombHitbox;
    private BomberBaseState currentState;

    // States for the bomber
    public BomberAggroState AggroState = new BomberAggroState();
    public BomberAttackState AttackState = new BomberAttackState();
    public BomberDeathState DeathState = new BomberDeathState();
    public BomberIdleState IdleState = new BomberIdleState();
    public BomberHitState HitState = new BomberHitState();
    public BomberSpawnState SpawnState = new BomberSpawnState();
    public float attackSpeed = 1f;
    [System.NonSerialized] public float attackTime;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();
        Player = GameObject.Find("Player").GetComponent<Transform>();
        findAnimationTimes();
        animator.SetFloat("attackSpeed", attackSpeed);
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

    public void SwitchState(BomberBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void EventTrigger()
    {
        currentState.EventTrigger(this);
    }

    // This would handle the bomber taking damage
    public void TakeDamageAnimation()
    {
        currentState.TakeDamage(this);
    }


  public void TakeDamage(int damage)
    {
        
    }

    public void Destroy(float waitDuration)
    {
        Destroy(gameObject, waitDuration);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointY == null)
        {
            return;
        }
        // Shows the attackRange, attackHeight
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
                case "BomberAttack":
                    attackTime = clip.length;
                    Debug.Log(attackTime + " attackTime");
                    break;
                default:
                    Debug.Log(clip.name + " is not accounted for.");
                    break;
            }
        }
    }
}
