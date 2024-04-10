using UnityEngine;

public class WidowAggroState : WidowBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(WidowStateManager Widow)
    {
        Debug.Log("Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = Widow.GetComponent<Rigidbody2D>();
        animator = Widow.GetComponent<Animator>();
    }

    public override void UpdateState(WidowStateManager Widow)
    {
        Vector2 Direction = (player.position - Widow.transform.position).normalized;
        rb.AddForce(Direction * 1f); 
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (Direction.x != 0){ //If the player is moving horizontally
            flipped = Direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }
        
        Widow.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
    }

    public override void OnCollisionEnter2D(WidowStateManager Widow, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(WidowStateManager Widow, Collider2D other) {
        if (other.tag == "Player")
        {
            Widow.SwitchState(Widow.AttackState);
        }
    }

    public override void EventTrigger(WidowStateManager Widow)
    {

    }

    public override void TakeDamage(WidowStateManager Widow)
    {
        Widow.SwitchState(Widow.HitState);
    }
}
