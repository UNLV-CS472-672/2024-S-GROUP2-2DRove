using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianStateManager : MonoBehaviour
{
    public AfterImage afterImage;
    public Animator animator;
    public Transform attack1X;
    public Transform attack1Y;
    [SerializeField] public float attack1Length;
    [SerializeField] public float attack1Height;
    public Transform attack2;
    [SerializeField] public float attack2Range;
    private Mesh attackHitbox;
    GuardianBaseState currentState;
    public GuardianAggroState AggroState = new GuardianAggroState();
    public GuardianAttackState AttackState = new GuardianAttackState();
    public GuardianDeathState DeathState = new GuardianDeathState();
    // public GuardianHitState HitState = new GuardianHitState();
    public GuardianIdleState IdleState = new GuardianIdleState();
    public GuardianSpawnState SpawnState = new GuardianSpawnState();
    public GuardianVertDashState VertDashState = new GuardianVertDashState();
    public GuardianHorizontalDashState HorizontalDashState = new GuardianHorizontalDashState();
    public GuadianSpecialState SpecialState = new GuadianSpecialState();

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

    public void SwitchState(GuardianBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void EventTrigger(int attackID)
    {
        currentState.EventTrigger(this, attackID);
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
        if (attack1X == null){
            return;
        }
        Gizmos.DrawWireSphere(attack1X.position, attack1Length);
        Gizmos.DrawWireSphere(attack1Y.position, attack1Height);
        Gizmos.DrawWireSphere(attack2.position, attack2Range);
    }
}
