using UnityEngine;

public class GhoulAttackState : GhoulBaseState
{
    private float attackTime = .9f;
    private Animator animator;
    public override void EnterState(GhoulStateManager ghoul)
    {
        //Debug.Log("Entering Attack State");
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
    
    public override void OnTriggerStay2D(GhoulStateManager ghoul, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(GhoulStateManager ghoul)
    {
        Vector2 knockbackDirection = (Vector2)(ghoul.transform.position - ghoul.attackPoint.position).normalized;
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(ghoul.attackPoint.position, ghoul.attackRange, mask);

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

    public override void TakeDamage(GhoulStateManager ghoul)
    {
        ghoul.SwitchState(ghoul.HitState);
    }
}
