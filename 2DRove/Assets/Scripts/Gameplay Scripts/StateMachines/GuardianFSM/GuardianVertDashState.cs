using UnityEngine;

public class GuardianVertDashState : GuardianBaseState
{
    private Transform player;
    private float dashTime;
    private float xPos;
    private float yPos;
    private float playerYPos;
    private Animator animator;
    public override void EnterState(GuardianStateManager Guardian)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        xPos = Guardian.GetComponent<Transform>().position.x;
        yPos = Guardian.GetComponent<Transform>().position.y;
        //Debug.Log("Entering Dash State");
        dashTime = ((Guardian.vertDashTime * 2) / Guardian.vertDashSpeed);
        animator = Guardian.GetComponent<Animator>();
        animator.SetTrigger("vertDash");
        Guardian.GetComponent<PolygonCollider2D>().enabled = true;
        Guardian.afterImage.makeGhost = true;
    }

    public override void UpdateState(GuardianStateManager Guardian)
    {
        if(dashTime <= 0)
        {
            Guardian.SwitchState(Guardian.HorizontalDashState);
        }

        playerYPos = player.transform.position.y;
        Guardian.GetComponent<Transform>().position = new Vector3(xPos, Mathf.SmoothStep(playerYPos, yPos, dashTime/(Guardian.vertDashTime *2)), 0);
        dashTime -= Time.deltaTime;
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
