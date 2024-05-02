using UnityEngine;

public class DaggerMushroomAttackState : DaggerMushroomBaseState
{
    private float attackTime;
    private Animator animator;
    public override void EnterState(DaggerMushroomStateManager mush)
    {
        //Debug.Log("Entering Attack State");
        attackTime = mush.attackTime / mush.attackSpeed;
        animator = mush.GetComponent<Animator>();
        animator.SetBool("attacking", true);
    }

    public override void UpdateState(DaggerMushroomStateManager mush)
    {
        if(attackTime <= 0)
        {
            mush.lastAttack = Time.time + mush.attackCD;
            animator.SetBool("attacking", false);
            mush.SwitchState(mush.IdleState);

        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(DaggerMushroomStateManager mush, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(DaggerMushroomStateManager mush, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(DaggerMushroomStateManager mush)
    {
        Vector2 knockbackDirection = (Vector2)(mush.transform.position - mush.attackPoint.position).normalized;
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mush.attackPoint.position, mush.attackRange, mask);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerController playerScript = collider.GetComponent<PlayerController>();
                playerScript.dealDamage(mush.attackDamage);
                collider.GetComponent<Rigidbody2D>().AddForce(-knockbackDirection * 5, ForceMode2D.Impulse);
                collider.GetComponent<Animator>().SetTrigger("Hit");
            }
        }
    }

    public override void TakeDamage(DaggerMushroomStateManager mush)
    {
        
    }
}
