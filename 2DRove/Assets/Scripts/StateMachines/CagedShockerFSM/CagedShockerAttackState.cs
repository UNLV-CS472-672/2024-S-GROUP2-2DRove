using UnityEngine;

public class CagedShockerAttackState : CagedShockerBaseState
{
    private float attackTime = .9f;
    private Animator animator;
    public override void EnterState(CagedShockerStateManager CagedShocker)
    {
        Debug.Log("Entering Attack State");
        attackTime = .9f;
        animator = CagedShocker.GetComponent<Animator>();
        animator.SetBool("attacking", true);
    }

    public override void UpdateState(CagedShockerStateManager CagedShocker)
    {
        if(attackTime <= 0)
        {
            CagedShocker.SwitchState(CagedShocker.IdleState);
            animator.SetBool("attacking", false);
        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(CagedShockerStateManager CagedShocker, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(CagedShockerStateManager CagedShocker, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(CagedShockerStateManager CagedShocker)
    {
        Vector2 knockbackDirection = (Vector2)(CagedShocker.transform.position - CagedShocker.attackPointX.position).normalized;
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(CagedShocker.attackPointX.position, new Vector2(CagedShocker.attackRange * 2, CagedShocker.attackHeight), CapsuleDirection2D.Vertical, mask);

        

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
    }

    public override void TakeDamage(CagedShockerStateManager CagedShocker)
    {
        CagedShocker.SwitchState(CagedShocker.HitState);
    }
}
