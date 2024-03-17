using UnityEngine;

public class SpitterAggroState : SpitterBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(SpitterStateManager spitter)
    {
        Debug.Log("Spitter Entering Aggo State...");
    
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 

        rb = spitter.GetComponent<Rigidbody2D>();
        animator = spitter.GetComponent<Animator>();
    }

    public override void UpdateState(SpitterStateManager spitter)
    {
        Vector2 Direction = (player.position - spitter.transform.position).normalized;
        rb.AddForce(Direction * 1f);
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        // player is moving horizional
        if (Direction.x != 0)
        {
            flipped = Direction.x < 0; // player moved left(flipped = true), else player is moving right(flipped = false)
        }
        spitter.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180 : 0f, 0f));

    }

    public override void OnCollisionEnter2D(SpitterStateManager spitter, Collision2D other)
    {

    }
    public override void OnTriggerStay2D(SpitterStateManager spitter, Collider2D other)
    {
        // if (other.tag("Player")) // Used CompareTag for optimized performance

        if (other.CompareTag("Player"))
        {
            spitter.SwitchState(spitter.AttackState);
        }
    }

}
