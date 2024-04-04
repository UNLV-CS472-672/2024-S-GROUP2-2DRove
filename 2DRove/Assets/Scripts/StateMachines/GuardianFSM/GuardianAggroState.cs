using UnityEngine;

public class GuardianAggroState : GuardianBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(GuardianStateManager Guardian)
    {
        Debug.Log("Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = Guardian.GetComponent<Rigidbody2D>();
        animator = Guardian.GetComponent<Animator>();
    }

    public override void UpdateState(GuardianStateManager Guardian)
    {
        Vector2 Direction = (player.position - Guardian.transform.position).normalized;
        rb.AddForce(Direction * 1f);
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (Direction.x != 0){ //If the player is moving horizontally
            flipped = Direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }
        
        Guardian.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
    }

    public override void OnCollisionEnter2D(GuardianStateManager Guardian, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(GuardianStateManager Guardian, Collider2D other) {
        if (other.tag == "Player")
        {
            Guardian.SwitchState(Guardian.AttackState);
        }
    }

    public override void EventTrigger(GuardianStateManager Guardian, int attackID)
    {

    }

    public override void TakeDamage(GuardianStateManager Guardian)
    {
        // Guardian.SwitchState(Guardian.HitState);
        float health = Guardian.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            Guardian.SwitchState(Guardian.DeathState);
        }
    }
}
