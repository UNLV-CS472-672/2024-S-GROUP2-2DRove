using UnityEngine;

public class DaggerMushroomAggroState : DaggerMushroomBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;

    public override void EnterState(DaggerMushroomStateManager mushroom)
    {
        //Debug.Log("Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = mushroom.GetComponent<Rigidbody2D>();
        animator = mushroom.GetComponent<Animator>();
    }

    public override void UpdateState(DaggerMushroomStateManager mushroom)
    {
        Vector2 Direction = (player.position - mushroom.transform.position).normalized;
        rb.AddForce(Direction * mushroom.movementSpeed);
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));

        if (Direction.x != 0){ //If the player is moving horizontally
            flipped = Direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }
        
        mushroom.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));
    }

    public override void OnCollisionEnter2D(DaggerMushroomStateManager mushroom, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(DaggerMushroomStateManager mushroom, Collider2D other) {
        if (other.tag == "Player")
        {
            mushroom.SwitchState(mushroom.AttackState);
        }
    }

    public override void EventTrigger(DaggerMushroomStateManager mushroom)
    {

    }

    public override void TakeDamage(DaggerMushroomStateManager mushroom)
    {
        mushroom.SwitchState(mushroom.HitState);
    }
}
