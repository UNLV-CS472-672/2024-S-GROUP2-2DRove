using UnityEngine;

public class GhoulAggroState : GhoulBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(GhoulStateManager ghoul)
    {
        Debug.Log("Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = ghoul.GetComponent<Rigidbody2D>();
        animator = ghoul.GetComponent<Animator>();
    }

    public override void UpdateState(GhoulStateManager ghoul)
    {
        Vector2 Direction = (player.position - ghoul.transform.position).normalized;
        rb.AddForce(Direction * 1f);
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (Direction.x != 0){ //If the player is moving horizontally
            flipped = Direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }
        
        ghoul.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
    }

    public override void OnCollisionEnter2D(GhoulStateManager ghoul, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(GhoulStateManager ghoul, Collider2D other) {
        if (other.tag == "Player")
        {
            ghoul.SwitchState(ghoul.AttackState);
        }
    }

    public override void EventTrigger(GhoulStateManager ghoul)
    {

    }

    public override void TakeDamage(GhoulStateManager ghoul)
    {
        ghoul.SwitchState(ghoul.HitState);
    }
}
