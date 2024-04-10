using UnityEngine;

public class CagedShockerLurch2State : CagedShockerBaseState
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool flipped = false;
    private float lurchCD = .5f + .2f;
    private bool lurched = false;
    private bool wantsToAttack = false;

    public override void EnterState(CagedShockerStateManager CagedShocker)
    {
        Debug.Log("Entering Lurch2 State...");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = CagedShocker.GetComponent<Rigidbody2D>();
        animator = CagedShocker.GetComponent<Animator>();
        lurchCD = .5f + .2f;
        lurched = false;
        wantsToAttack = false;
        animator.SetTrigger("lurch2");
    }

    public override void UpdateState(CagedShockerStateManager CagedShocker)
    {
        if (!lurched)
        {
            Vector3 Direction = (player.position - CagedShocker.transform.position).normalized;
            rb.AddForce(Direction * 300f);

            if (Direction.x != 0){ //If the player is moving horizontally
                flipped = Direction.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
            }
        
            CagedShocker.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f: 0f, 0f));

            lurched = true;
        }
        else if (lurchCD < 0 && !wantsToAttack)
        {
            CagedShocker.SwitchState(CagedShocker.Lurch1State);
        }
        else if(lurchCD < 0 && wantsToAttack)
        {
            CagedShocker.SwitchState(CagedShocker.AttackState);
        }

        lurchCD -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(CagedShockerStateManager CagedShocker, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(CagedShockerStateManager CagedShocker, Collider2D other) {
        wantsToAttack = true;
        if (other.tag == "Player" && lurchCD < 0)
        {
            CagedShocker.SwitchState(CagedShocker.AttackState);
        }
    }

    public override void EventTrigger(CagedShockerStateManager CagedShocker)
    {

    }

    public override void TakeDamage(CagedShockerStateManager CagedShocker)
    {
        CagedShocker.SwitchState(CagedShocker.HitState);
    }
}
