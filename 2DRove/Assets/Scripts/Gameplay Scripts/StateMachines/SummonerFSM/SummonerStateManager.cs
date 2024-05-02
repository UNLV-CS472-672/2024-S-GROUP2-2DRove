using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerStateManager : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;
    [SerializeField] public int goldDropped = 1;
    SummonerBaseState currentState;
    public SummonerAggroState AggroState = new SummonerAggroState();
    // public SummonerAttackState AttackState = new SummonerAttackState(); // Summoner does not Attack
    public SummonerDeathState DeathState = new SummonerDeathState();
    public SummonerHitState HitState = new SummonerHitState();
    public SummonerSummoningState SummoningState = new SummonerSummoningState();
    public SummonerIdleState IdleState = new SummonerIdleState();
    public SummonerSpawnState SpawnState = new SummonerSpawnState();

    private NewEnemy newEnemy; // Reference to NewEnemy component
    public GameObject ghoulPrefab;
    private Transform player;
    public float movementSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        currentState = SpawnState;
        currentState.EnterState(this);
        animator = this.GetComponent<Animator>();
        newEnemy = GetComponent<NewEnemy>(); // Assign NewEnemy component
        player = GameObject.Find("Player").GetComponent<Transform>();

        // Create a new SummoningState 

        SummoningState = new SummonerSummoningState();
        SummoningState.ghoulPrefab = ghoulPrefab;
        // Set the ghoulPrefab field in SummoningState
        if (SummoningState != null)
        {
            SummoningState.ghoulPrefab = ghoulPrefab;
        }

    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

        // Check for death condition if not already in DeathState
        if (currentState != DeathState && newEnemy.CurrentHeath() <= 0)
        {
            SwitchState(DeathState);
        }
        if((this.transform.position - player.position).magnitude > 50)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        currentState.OnTriggerStay2D(this, other);
    }

    public void SwitchState(SummonerBaseState state)
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
}
