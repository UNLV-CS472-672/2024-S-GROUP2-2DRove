using UnityEngine;

public class GuardianHorizontalDashState : GuardianBaseState
{
    private Transform player;
    private float dashDuration = .417f;
    private float dashTime;
    private float xPos;
    private float yPos;
    private float playerXPos;
    private Animator animator;
    public override void EnterState(GuardianStateManager Guardian)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerXPos = player.transform.position.x;
        xPos = Guardian.GetComponent<Transform>().position.x;
        yPos = Guardian.GetComponent<Transform>().position.y;
        Debug.Log("Entering Dash State");
        dashTime = dashDuration;
        animator = Guardian.GetComponent<Animator>();
        animator.SetTrigger("horizontalDash");
    }

    public override void UpdateState(GuardianStateManager Guardian)
    {
        if(dashTime <= 0)
        {
            Guardian.SwitchState(Guardian.SpecialState);
        }

        Guardian.GetComponent<Transform>().position = new Vector3(Mathf.SmoothStep(playerXPos, xPos, dashTime/dashDuration), yPos, 0);
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
