using UnityEngine;

public class GuardianAttackState : GuardianBaseState
{
    private float attackTime = .917f + .917f;
    private Animator animator;
    public override void EnterState(GuardianStateManager Guardian)
    {
        Debug.Log("Entering Attack State");
        attackTime = .917f + .917f;
        animator = Guardian.GetComponent<Animator>();
        animator.SetBool("attacking", true);
    }

    public override void UpdateState(GuardianStateManager Guardian)
    {
        if(attackTime <= 0)
        {
            Guardian.SwitchState(Guardian.AggroState);
            animator.SetBool("attacking", false);
        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(GuardianStateManager Guardian, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(GuardianStateManager Guardian, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(GuardianStateManager Guardian, int attackID)
    {
        if (attackID == 1)
        {
            //prepping hitbox for attack 2
            Guardian.GetComponent<CircleCollider2D>().enabled = true;
            LayerMask mask = LayerMask.GetMask("Player");
            Collider2D[] colliders = Physics2D.OverlapCapsuleAll(Guardian.attack1X.position, new Vector2(Guardian.attack1Length, Guardian.attack1Height), CapsuleDirection2D.Vertical, mask);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    Vector2 knockbackDirection = (Vector2)(Guardian.transform.position - collider.transform.position);
                    PlayerController playerScript = collider.GetComponent<PlayerController>();
                    playerScript.dealDamage(1);
                    collider.GetComponent<Rigidbody2D>().AddForce(knockbackDirection, ForceMode2D.Impulse);
                    collider.GetComponent<Animator>().SetTrigger("Hit");
                }
            }
        }
        else if (attackID == 2)
        {
            Vector2 knockbackDirection = (Vector2)(Guardian.transform.position - Guardian.attack2.position).normalized;
            LayerMask mask = LayerMask.GetMask("Player");
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Guardian.attack2.position, Guardian.attack2Range, mask);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    PlayerController playerScript = collider.GetComponent<PlayerController>();
                    playerScript.dealDamage(1);
                    collider.GetComponent<Rigidbody2D>().AddForce(-knockbackDirection * 5, ForceMode2D.Impulse);
                    collider.GetComponent<Animator>().SetTrigger("Hit");
                }
            }

            Guardian.GetComponent<CircleCollider2D>().enabled = false;
        }
        else if (attackID == 3)
        {
            LayerMask mask = LayerMask.GetMask("Player");
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Guardian.attack1X.position, 8, mask);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    Vector2 knockbackDirection = (Vector2)(Guardian.transform.position - collider.transform.position);
                    PlayerController playerScript = collider.GetComponent<PlayerController>();
                    playerScript.dealDamage(1);
                    collider.GetComponent<Rigidbody2D>().AddForce(knockbackDirection, ForceMode2D.Impulse);
                    collider.GetComponent<Animator>().SetTrigger("Hit");
                }
            }
        }
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

    //debug appears for ONE frame, you need to pause and go frame by frame, to do so click on the button next to the pause button, which is next to the play button on top
    void DebugDrawCircle(Vector2 center, float radius, Color color)
    {
        int segments = 36; // Number of segments to approximate circle
        float angleIncrement = 360f / segments;

        Vector2 prevPoint = center + new Vector2(radius, 0); // Starting point

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleIncrement;

            // Calculate the next point on the circle
            Vector2 nextPoint = center + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;

            // Draw line segment from previous point to next point
            Debug.DrawLine(prevPoint, nextPoint, color);

            // Update previous point
            prevPoint = nextPoint;
        }

        // Draw a line to close the circle
        Debug.DrawLine(prevPoint, center + new Vector2(radius, 0), color);
    }
}
