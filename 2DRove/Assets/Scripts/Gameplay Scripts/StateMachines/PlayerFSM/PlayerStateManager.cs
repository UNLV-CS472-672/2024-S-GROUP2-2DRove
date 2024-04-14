using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        input = this.GetComponent<PlayerInput>();
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        flipped = false;
    }

    // Update is called once per frame
    void Update()
    {
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
}
