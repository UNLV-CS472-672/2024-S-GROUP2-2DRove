using UnityEngine;

public class RockBossAggroState : RockBossBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(RockBossStateManager RockBoss)
    {
        //Debug.Log("Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = RockBoss.GetComponent<Rigidbody2D>();
        animator = RockBoss.GetComponent<Animator>();
    }

    public override void UpdateState(RockBossStateManager RockBoss)
    {
        Vector2 Direction = (player.position - RockBoss.transform.position).normalized;
        rb.AddForce(Direction * RockBoss.MovementSpeed); 
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (Direction.x != 0){ //If the player is moving horizontally
            flipped = Direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }
        
        RockBoss.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
    }

    public override void OnCollisionEnter2D(RockBossStateManager RockBoss, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(RockBossStateManager RockBoss, Collider2D other) {
        if (other.tag == "Player")
        {
            RockBoss.SwitchState(RockBoss.AttackState);
        }
    }

    public override void EventTrigger(RockBossStateManager RockBoss)
    {

    }

    public override void TakeDamage(RockBossStateManager RockBoss)
    {
        RockBoss.SwitchState(RockBoss.HitState);
    }
}
