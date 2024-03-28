using UnityEngine;

public class DaggerMushroomAttackState : DaggerMushroomBaseState
{
    private float attackTime = .9f;
    private Animator animator;
    public override void EnterState(DaggerMushroomStateManager mushroom)
    {
        Debug.Log("Entering Attack State");
        attackTime = .9f;
        animator = mushroom.GetComponent<Animator>();
        animator.SetBool("attacking", true);
    }

    public override void UpdateState(DaggerMushroomStateManager mushroom)
    {
        if(attackTime <= 0)
        {
            mushroom.SwitchState(mushroom.IdleState);
            animator.SetBool("attacking", false);
        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(DaggerMushroomStateManager mushroom, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(DaggerMushroomStateManager mushroom, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(DaggerMushroomStateManager mushroom)
    {
        Vector2 knockbackDirection = (Vector2)(mushroom.transform.position - mushroom.attackPoint.position).normalized;
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mushroom.attackPoint.position, mushroom.attackRange, mask);

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

    public override void TakeDamage(DaggerMushroomStateManager mushroom)
    {
        mushroom.SwitchState(mushroom.HitState);
    }
}
