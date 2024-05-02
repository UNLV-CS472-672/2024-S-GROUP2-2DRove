using UnityEngine;

public class ArcherAggroState : ArcherBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(ArcherStateManager archer)
    {
        //Debug.Log("Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = archer.GetComponent<Rigidbody2D>();
        animator = archer.GetComponent<Animator>();
    }

    public override void UpdateState(ArcherStateManager archer)
    {
        Vector2 Direction = (player.position - archer.transform.position).normalized;
        rb.AddForce(Direction * archer.movementSpeed);
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (Direction.x != 0){ //If the player is moving horizontally
            flipped = Direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }
        
        archer.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
    }

    public override void OnCollisionEnter2D(ArcherStateManager archer, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(ArcherStateManager archer, Collider2D other) {
        if (other.tag == "Player")
        {
            archer.SwitchState(archer.AttackState);
        }
    }

    public override void EventTrigger(ArcherStateManager archer)
    {

    }

    public override void TakeDamage(ArcherStateManager archer)
    {
        archer.SwitchState(archer.HitState);
    }
}
