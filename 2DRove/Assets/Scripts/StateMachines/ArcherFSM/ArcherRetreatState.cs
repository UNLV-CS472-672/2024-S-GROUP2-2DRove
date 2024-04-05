using UnityEngine;

public class ArcherRetreatState : ArcherBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;
    private float retreatTime = 1.5f;

    public override void EnterState(ArcherStateManager archer)
    {
        Debug.Log("Entering Retreat State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = archer.GetComponent<Rigidbody2D>();
        animator = archer.GetComponent<Animator>();
        retreatTime = 1.5f;
    }

    public override void UpdateState(ArcherStateManager archer)
    {
        if(retreatTime <= 0) 
        {
            archer.SwitchState(archer.IdleState);
        }
        Vector2 Direction = (archer.transform.position - player.position).normalized;
        rb.AddForce(Direction * 1f);
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (Direction.x != 0){ //If the player is moving horizontally
            flipped = Direction.x > 0; //If the player is moving left, flipped is false, if the player is moving right, flipped is true
        }
        
        archer.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
        retreatTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(ArcherStateManager archer, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(ArcherStateManager archer, Collider2D other) {
    }

    public override void EventTrigger(ArcherStateManager archer)
    {

    }

    public override void TakeDamage(ArcherStateManager archer)
    {
        archer.SwitchState(archer.HitState);
    }
}
