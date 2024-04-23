using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    public Vector2 inputDirection;
    public float MovementSpeed;
    public float walkAnimSpeed;
    public float slash1Speed;
    public float slash2Speed;
    public float slash3Speed;
    public float hitStunSpeed;
    public float idleAnimSpeed;
    public float slash1Lurch;
    public float slash2Lurch;
    public float slash3Lurch;
    [System.NonSerialized] public float slash1Time;
    [System.NonSerialized] public float slash2Time;
    [System.NonSerialized] public float slash3Time;
    public float dashDistance;
    public float dashDuration;
    public float dashCooldown;
    [System.NonSerialized] public AfterImage afterImage;
    [System.NonSerialized] public float lastDashedTime;
    public Animator animator;
    public Transform attackPoint;
    [SerializeField] public float attackRange;
    PlayerBaseState currentState;
    private PlayerInput input;
    public Rigidbody2D rb;
    public bool flipped; 
    public PlayerNeutralState NeutralState = new PlayerNeutralState();
    public PlayerSlash1State Slash1State = new PlayerSlash1State();
    public PlayerSlash2State Slash2State = new PlayerSlash2State();
    public PlayerSlash3State Slash3State = new PlayerSlash3State();
    public PlayerDeathState DeathState = new PlayerDeathState();
    public PlayerHitState HitState = new PlayerHitState();
    public PlayerSpawnState SpawnState = new PlayerSpawnState();
    public PlayerDashState DashState = new PlayerDashState();

    // Start is called before the first frame update
    void Start()
    {
        input = this.GetComponent<PlayerInput>();
        afterImage = this.GetComponent<AfterImage>();
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        flipped = false;
        // lastInput = new Vector2(.9f, .1f);
        findAnimationTimes();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("walkAnimSpeed", walkAnimSpeed);
        animator.SetFloat("slash1Speed", slash1Speed);
        animator.SetFloat("slash2Speed", slash2Speed);
        animator.SetFloat("slash3Speed", slash3Speed);
        animator.SetFloat("hitStunSpeed", hitStunSpeed);
        animator.SetFloat("idleAnimSpeed", idleAnimSpeed);
        currentState.UpdateState(this);
    }

    private void OnTriggerStay2D(Collider2D other) {
        currentState.OnTriggerStay2D(this, other);
    }

    public void SwitchState(PlayerBaseState state)
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

    public int findDirectionFromInputs(string negativeDirectionInput, string positiveDirectionInput){ 
        int temp = 0;
        if(input.actions[negativeDirectionInput].IsPressed()){ //IsPressed returns true as long as the key is held down
            temp--;
        }
        if(input.actions[positiveDirectionInput].IsPressed()){
            temp++;
        }
        return temp;
    }

    public void Coroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    private void findAnimationTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Slash1":
                    slash1Time = clip.length;
                    break;
                case "Slash2":
                    slash2Time = clip.length;
                    break;
                case "Slash3":
                    slash3Time = clip.length;
                    break;
                default:
                    Debug.Log(clip.name + " is not accounted for.");
                    break;
            }
        }
    }
}
