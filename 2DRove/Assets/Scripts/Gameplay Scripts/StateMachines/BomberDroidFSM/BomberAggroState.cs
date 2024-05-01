using UnityEngine;

public class BomberAggroState : BomberBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private float attackAboveDistance = 3.0f; // Distance above the player's head to stop and attack


    public override void EnterState(BomberStateManager bomber)
    {
        //Debug.Log("Entering Aggro State...");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = bomber.GetComponent<Rigidbody2D>();
        animator = bomber.GetComponent<Animator>();
    }

    public override void UpdateState(BomberStateManager bomber)
    {
        Vector2 toPlayer = player.position - bomber.transform.position;
        float horizontalDistance = toPlayer.x;
        float verticalDistanceToTarget = player.position.y + attackAboveDistance - bomber.transform.position.y;
        // Tolerance for horizontal movement to prevent flipping near vertical alignment
        float horizontalTolerance = 0.2f;

        // Adjusted movement direction with tolerance
        float horizontalMoveDirection = Mathf.Abs(horizontalDistance) > horizontalTolerance ? Mathf.Sign(horizontalDistance) : 0;
        float verticalSpeedAdjustment = Mathf.Clamp(Mathf.Abs(verticalDistanceToTarget), 0f, 1f);

        Vector2 moveDirection = new Vector2(horizontalMoveDirection, Mathf.Sign(verticalDistanceToTarget) * verticalSpeedAdjustment).normalized;
        rb.velocity = moveDirection * bomber.MoveSpeed;
        animator.SetFloat("velocity", rb.velocity.magnitude);

        // Sets the  Bomber is above the player before it attacks
        bool isBomberAbovePlayer = verticalDistanceToTarget > attackAboveDistance;

        // Only update facing direction if moving horizontally
        if (horizontalMoveDirection != 0)
        {
            bomber.transform.localEulerAngles = new Vector3(0f, horizontalMoveDirection < 0 ? 180f : 0f, 0f);
        }

        // Check for transition to AttackState
        if (Mathf.Abs(horizontalDistance) <= 0.5f && isBomberAbovePlayer)
        {
            bomber.SwitchState(bomber.AttackState);
        }
    }

    public override void OnCollisionEnter2D(BomberStateManager bomber, Collision2D other)
    {
    }

    public override void OnTriggerStay2D(BomberStateManager bomber, Collider2D other)
    {
        if (other.tag == "Player")
        {
            bomber.SwitchState(bomber.AttackState);
        }
    }

    public override void EventTrigger(BomberStateManager bomber)
    {
    }

    public override void TakeDamage(BomberStateManager bomber)
    {
        bomber.SwitchState(bomber.HitState);
    }
}
