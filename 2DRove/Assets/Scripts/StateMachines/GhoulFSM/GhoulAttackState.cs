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
    
    public override void OnTriggerStay2D(GhoulStateManager ghoul, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(GhoulStateManager ghoul)
    {
        CapsuleCollider2D hitbox = ghoul.GetComponent<CapsuleCollider2D>();
        Vector2 worldSpace = new Vector2(ghoul.GetComponent<Transform>().position.x + 1.1f, ghoul.GetComponent<Transform>().position.y + 1.15f);
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(worldSpace + hitbox.offset, new Vector2(2.1f, 1.75f), CapsuleDirection2D.Horizontal, 0f, mask);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerController playerScript = collider.GetComponent<PlayerController>();
                playerScript.dealDamage(1);
            }
        }
    }

    public override void TakeDamage(GhoulStateManager ghoul)
    {
        ghoul.SwitchState(ghoul.HitState);
    }
}
