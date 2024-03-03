using UnityEngine;

public class SpitterAttackState : SpitterBaseState
{
    private float attackTime = 0.9f; // Will attack time different per enemy?
    private Animator animator;
    public override void EnterState(SpitterStateManager spitter)
    {
        Debug.Log("Spitter Entering Attack State");
        attackTime =0.9f; 
        animator = spitter.GetComponent<Animator>();
        animator.SetBool("attacking", true);
    }

    public override void UpdateState(SpitterStateManager spitter)
    {
        if (attackTime <= 0)
        {
            spitter.SwitchState(spitter.IdleState);
            animator.SetBool("attacking", false);
        }
        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(SpitterStateManager spitter, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(SpitterStateManager spitter, Collider2D other)
    {

    }
}
