using UnityEngine;

public class WardenAggroState : WardenBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(WardenStateManager Warden)
    {
        Debug.Log("Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = Warden.GetComponent<Rigidbody2D>();
        animator = Warden.GetComponent<Animator>();
    }

    public override void UpdateState(WardenStateManager Warden)
    {
        Vector2 Direction = (player.position - Warden.transform.position).normalized;
        rb.AddForce(Direction * 0f); //warden does not move in aggro state
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (Direction.x != 0){ //If the player is moving horizontally
            flipped = Direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }
        
        Warden.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
    }

    public override void OnCollisionEnter2D(WardenStateManager Warden, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(WardenStateManager Warden, Collider2D other) {
        if (other.tag == "Player")
        {
            Warden.SwitchState(Warden.AttackState);
        }
    }

    public override void EventTrigger(WardenStateManager Warden)
    {

    }

    public override void TakeDamage(WardenStateManager Warden)
    {
        Warden.SwitchState(Warden.HitState);
    }
}
