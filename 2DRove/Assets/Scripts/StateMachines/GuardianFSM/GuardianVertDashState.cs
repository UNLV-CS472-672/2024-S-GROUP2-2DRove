using UnityEngine;

public class GuardianVertDashState : GuardianBaseState
{
    private Transform player;
    private float dashDuration = .167f * 2;
    private float dashTime;
    private float attackTime = .9f;
    private float specialTime = 1.167f;
    private float xPos;
    private float yPos;
    private float playerYPos;
    private Animator animator;
    public override void EnterState(GuardianStateManager Guardian)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerYPos = player.transform.position.y;
        xPos = Guardian.GetComponent<Transform>().position.x;
        yPos = Guardian.GetComponent<Transform>().position.y;
        Debug.Log("Entering Dash State");
        dashTime = dashDuration;
        animator = Guardian.GetComponent<Animator>();
        animator.SetTrigger("vertDash");
        Guardian.GetComponent<PolygonCollider2D>().enabled = true;
    }

    public override void UpdateState(GuardianStateManager Guardian)
    {
        if(dashTime <= 0)
        {
            Guardian.SwitchState(Guardian.HorizontalDashState);
        }

        float vertDist = playerYPos - yPos;
        float prediction = (vertDist < 6 && vertDist > -6) ? 0 : ((vertDist)/(Mathf.Abs(vertDist))) * 6;
        Guardian.GetComponent<Transform>().position = new Vector3(xPos, Mathf.SmoothStep(playerYPos + prediction, yPos, dashTime/dashDuration), 0);
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
