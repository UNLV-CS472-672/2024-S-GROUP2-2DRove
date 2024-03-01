using UnityEngine;

public class GhoulAttackState : GhoulBaseState
{
    private float attackTime = .9f;
    private Animator animator;
    public override void EnterState(GhoulStateManager ghoul)
    {
        Debug.Log("Entering Attack State");
        attackTime = .9f;
        animator = ghoul.GetComponent<Animator>();
        animator.SetBool("attacking", true);
    }

    public override void UpdateState(GhoulStateManager ghoul)
    {
        if(attackTime <= 0)
        {
            ghoul.SwitchState(ghoul.IdleState);
            animator.SetBool("attacking", false);
        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(GhoulStateManager ghoul, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(GhoulStateManager ghoul, Collider2D other) {
    }
}
