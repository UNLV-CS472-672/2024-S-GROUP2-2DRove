using UnityEngine;

public class SpiderAggroState : SpiderBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(SpiderStateManager spider)
    {
        Debug.Log("Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = spider.GetComponent<Rigidbody2D>();
        animator = spider.GetComponent<Animator>();
    }

    public override void UpdateState(SpiderStateManager spider)
    {
        Vector2 Direction = (player.position - spider.transform.position).normalized;
        rb.AddForce(Direction * 1f);
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (Direction.x != 0){ //If the player is moving horizontally
            flipped = Direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }
        
        spider.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
    }

    public override void OnCollisionEnter2D(SpiderStateManager spider, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(SpiderStateManager spider, Collider2D other) {
        if (other.tag == "Player")
        {
            spider.SwitchState(spider.AttackState);
        }
    }

    public override void EventTrigger(SpiderStateManager spider)
    {

    }

    public override void TakeDamage(SpiderStateManager spider)
    {
        spider.SwitchState(spider.HitState);
    }
}
