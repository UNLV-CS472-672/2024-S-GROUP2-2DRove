using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianStateManager : MonoBehaviour
{
    public float MovementSpeed;
    public float walkAnimSpeed;
    public float attack1Speed;
    public float attack2Speed;
    public float vertDashSpeed;
    public float horizontalDashSpeed;
    public float AoESpeed;
    public float AoEResetSpeed;
    [System.NonSerialized] public float attack1Time;
    [System.NonSerialized] public float attack2Time;
    [System.NonSerialized] public float vertDashTime;
    [System.NonSerialized] public float horizontalDashTime;
    [System.NonSerialized] public float AoETime;
    [System.NonSerialized] public float AoEResetTime;
    [System.NonSerialized] public AfterImage afterImage;
    public Animator animator;
    public Transform attack1;
    [SerializeField] public float attack1Length;
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

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        currentState = SpawnState;
        currentState.EnterState(this);
        afterImage = this.GetComponent<AfterImage>();
        //new aduio part
        audioManager = FindObjectOfType<AudioManager>();
        if(this.GetComponent<AudioSource>() == null)
        {
            audioManager.AddAudioSourcesToEnemy(gameObject, "Guardian");
        }
        /*

            DO NOT DELETE

        // animator.SetFloat("walkAnimSpeed", walkAnimSpeed);
        // animator.SetFloat("attack1Speed", attack1Speed);
        // animator.SetFloat("attack2Speed", attack2Speed);
        // animator.SetFloat("vertDashSpeed", vertDashSpeed);
        // animator.SetFloat("horizontalDashSpeed", horizontalDashSpeed);
        // animator.SetFloat("AoESpeed", AoESpeed);
        // animator.SetFloat("AoEResetSpeed", AoEResetSpeed);


            DO NOT DELETE

        */
        findAnimationTimes();
    }

    // Update is called once per frame
    void Update()
    {
        //TEMORARILY HERE FOR FASTER DEBUGGING REASIONS
        //THIS SLOWS THE GAME DODWN A LOT MORE, SHOULD BE PLACED IN START FUCNCTION
            //all the animator.SetFloat() calls
        animator.SetFloat("walkAnimSpeed", walkAnimSpeed);
        animator.SetFloat("attack1Speed", attack1Speed);
        animator.SetFloat("attack2Speed", attack2Speed);
        animator.SetFloat("vertDashSpeed", vertDashSpeed);
        animator.SetFloat("horizontalDashSpeed", horizontalDashSpeed);
        animator.SetFloat("AoESpeed", AoESpeed);
        animator.SetFloat("AoEResetSpeed", AoEResetSpeed);
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
        if (attack1 == null){
            return;
        }
        Gizmos.DrawWireSphere(attack1.position, attack1Length);
        Gizmos.DrawWireSphere(attack2.position, attack2Range);
    }

    private void findAnimationTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Attack1":
                    attack1Time = clip.length;
                    break;
                case "Attack2":
                    attack2Time = clip.length;
                    break;
                case "VertDash":
                    vertDashTime = clip.length;
                    break;
                case "ChargeDash":
                    horizontalDashTime = clip.length;
                    break;
                case "DashSpecial":
                    AoETime = clip.length;
                    break;
                case "SpecialReset":
                    AoEResetTime = clip.length;
                    break;
                default:
                    Debug.Log(clip.name + " is not accounted for.");
                    break;
            }
        }
    }
}
