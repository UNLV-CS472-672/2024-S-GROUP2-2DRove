using UnityEngine;

public class SpiderAttackState : SpiderBaseState
{
    private float attackTime = .9f;
    private Animator animator;
    public override void EnterState(SpiderStateManager Spider)
    {
        Debug.Log("Spider entering Attack State");
        attackTime = .9f;
        animator = Spider.GetComponent<Animator>();
        animator.SetBool("attacking", true);
        animator.SetFloat("velocity", 0);
    }

    public override void UpdateState(SpiderStateManager Spider)
    {
        if(attackTime <= 0)
        {
            Spider.SwitchState(Spider.IdleState);
            animator.SetBool("attacking", false);
        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(SpiderStateManager Spider, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(SpiderStateManager Spider, Collider2D other) {
    }
}
