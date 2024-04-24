using UnityEngine;

public class GuadianSpecialState : GuardianBaseState
{
    private Transform player;
    private float specialTime;
    private float xPos;
    private float yPos;
    private float playerXPos;
    private Animator animator;
    private AudioSource attackSound;
    public override void EnterState(GuardianStateManager Guardian)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerXPos = player.transform.position.x;
        xPos = Guardian.GetComponent<Transform>().position.x;
        yPos = Guardian.GetComponent<Transform>().position.y;
        Debug.Log("Entering Dash State");
        specialTime = (Guardian.AoETime / Guardian.AoESpeed) + (Guardian.AoEResetTime / Guardian.AoEResetSpeed);
        animator = Guardian.GetComponent<Animator>();
        AudioSource[] sources = Guardian.GetComponents<AudioSource>();
        attackSound = sources[1];
        attackSound.Play();
        animator.SetTrigger("special");
        Guardian.afterImage.makeGhost = false;
    }

    public override void UpdateState(GuardianStateManager Guardian)
    {
        if(specialTime <= 0)
        {
            Guardian.GetComponent<PolygonCollider2D>().enabled = false;
            Guardian.SwitchState(Guardian.AggroState);
        }

        specialTime -= Time.deltaTime;
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
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Guardian.attack1.position, 16, mask);

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
