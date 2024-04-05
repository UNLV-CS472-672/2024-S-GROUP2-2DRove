using UnityEngine;

public class ArcherAttackState : ArcherBaseState
{
    private float attackTime = 1.0f;
    private Animator animator;
    private int countdown = 2;
    private bool isSpecial = false;
    public override void EnterState(ArcherStateManager archer)
    {
        Debug.Log("Entering Attack State");
        attackTime = 1.0f;
        animator = archer.GetComponent<Animator>();
        if(countdown <= 0)
        {
            Debug.Log("Entering Special Attack State");
            animator.SetBool("specialAttack", true);
            isSpecial = true;
            countdown = 2;
        }
        else
        {
            animator.SetBool("attacking", true);
            isSpecial = false;
            countdown--;
        }
    }

    public override void UpdateState(ArcherStateManager archer)
    {
        if(attackTime <= 0)
        {
            archer.SwitchState(archer.IdleState);
            animator.SetBool("attacking", false);
            animator.SetBool("specialAttack", false);
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
        Vector2 knockbackDirection = (Vector2)(archer.transform.position - archer.attackPoint.position).normalized;
        LayerMask mask = LayerMask.GetMask("Player");
        if(isSpecial)
        {
            archer.attackRange = 10;
        }
        else
        {
            archer.attackRange = 5;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(archer.attackPoint.position, archer.attackRange, mask);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerController playerScript = collider.GetComponent<PlayerController>();
                if(isSpecial)
                {
                    playerScript.dealDamage(5);
                }
                else
                {
                    playerScript.dealDamage(1);
                }
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
