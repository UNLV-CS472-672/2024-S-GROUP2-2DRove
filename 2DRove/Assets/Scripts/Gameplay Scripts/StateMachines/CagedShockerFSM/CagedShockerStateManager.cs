using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagedShockerStateManager : MonoBehaviour
{
    public Animator animator;
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
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        //new aduio part
        audioManager = FindObjectOfType<AudioManager>();
        if(this.GetComponent<AudioSource>() == null)
        {
            audioManager.AddAudioSourcesToEnemy(gameObject, "CagedShocker");
        }
        
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
}
