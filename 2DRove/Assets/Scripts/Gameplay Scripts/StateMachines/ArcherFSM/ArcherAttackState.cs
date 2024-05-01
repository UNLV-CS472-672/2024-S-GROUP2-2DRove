using UnityEngine;

public class ArcherAttackState : ArcherBaseState
{
    private float attackTime = 1.0f;
    private Animator animator;
    public override void EnterState(ArcherStateManager archer)
    {
        //Debug.Log("Entering Attack State");
        attackTime = 1.0f;
        animator = archer.GetComponent<Animator>();
        animator.SetBool("attacking", true);
    }

    public override void UpdateState(ArcherStateManager archer)
    {
        if(attackTime <= 0)
        {
            archer.SwitchState(archer.IdleState);
            animator.SetBool("attacking", false);
        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(ArcherStateManager archer, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(ArcherStateManager archer, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(ArcherStateManager archer)
    {
        Vector2 knockbackDirection = (Vector2)(archer.transform.position - archer.attackPointX.position).normalized;
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(archer.attackPointX.position, new Vector2(archer.attackRangeX * 2, archer.attackRangeY), CapsuleDirection2D.Horizontal, mask);

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

    public override void TakeDamage(ArcherStateManager archer)
    {
        archer.SwitchState(archer.HitState);
    }
}
