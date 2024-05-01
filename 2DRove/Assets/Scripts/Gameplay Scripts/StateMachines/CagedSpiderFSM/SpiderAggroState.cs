using UnityEngine;

public class SpiderAggroState : SpiderBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(SpiderStateManager Spider)
    {
        //Debug.Log("Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = Spider.GetComponent<Rigidbody2D>();
        animator = Spider.GetComponent<Animator>();
    }

    public override void UpdateState(SpiderStateManager Spider)
    {
        Vector2 Direction = (player.position - Spider.transform.position).normalized;
        rb.AddForce(Direction * 2f);
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (Direction.x != 0){ //If the player is moving horizontally
            flipped = Direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }
        
        Spider.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
    }

    public override void OnCollisionEnter2D(SpiderStateManager Spider, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(SpiderStateManager Spider, Collider2D other) {
        // if (other.tag("Player")) // Used CompareTag for optimized performance

        if (other.CompareTag("Player"))
        {
            Spider.SwitchState(Spider.AttackState);
        }
    }

    public override void EventTrigger(SpiderStateManager Spider)
    {

    }

    public override void TakeDamage(SpiderStateManager Spider)
    {
        Spider.SwitchState(Spider.HitState);
    }
}
