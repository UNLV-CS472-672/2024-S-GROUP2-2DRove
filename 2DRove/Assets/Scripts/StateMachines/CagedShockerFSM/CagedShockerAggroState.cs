using UnityEngine;

public class CagedShockerAggroState : CagedShockerBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(CagedShockerStateManager CagedShocker)
    {
        Debug.Log("Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = CagedShocker.GetComponent<Rigidbody2D>();
        animator = CagedShocker.GetComponent<Animator>();
    }

    public override void UpdateState(CagedShockerStateManager CagedShocker)
    {
        Vector2 Direction = (player.position - CagedShocker.transform.position).normalized;
        rb.AddForce(Direction * 1f);
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (Direction.x != 0){ //If the player is moving horizontally
            flipped = Direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }
        
        CagedShocker.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
    }

    public override void OnCollisionEnter2D(CagedShockerStateManager CagedShocker, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(CagedShockerStateManager CagedShocker, Collider2D other) {
        if (other.tag == "Player")
        {
            CagedShocker.SwitchState(CagedShocker.AttackState);
        }
    }

    public override void EventTrigger(CagedShockerStateManager CagedShocker)
    {

    }

    public override void TakeDamage(CagedShockerStateManager CagedShocker)
    {
        CagedShocker.SwitchState(CagedShocker.HitState);
    }
}
