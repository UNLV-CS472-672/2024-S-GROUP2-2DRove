using System.Collections;
using UnityEngine;

public class RockBossAttackState : RockBossBaseState
{
    private float attackTime = 2.5f;
    private Animator animator;
    public override void EnterState(RockBossStateManager RockBoss)
    {
        Debug.Log("Entering Attack State");
        attackTime = 2.5f;
        animator = RockBoss.GetComponent<Animator>();
        PerformRandomAttack(RockBoss);
    }

    private void PerformRandomAttack(RockBossStateManager RockBoss)
    {
        int attackType = Random.Range(0, 4);
        Debug.Log(attackType);
        switch (attackType)
        {
            case 0:
                animator.SetBool("Beaming", true);
                Debug.Log("Beaming");
                break;
            case 1:
                animator.SetBool("Shooting", true);
                Debug.Log("Shooting");
                break;
            case 2:
                animator.SetBool("Charging", true);
                Debug.Log("Charging");
                break;
            case 3:
                animator.SetBool("Slamming", true);
                Debug.Log("Slamming");
                break; 
        }
    }

    public override void UpdateState(RockBossStateManager RockBoss)
    {
        if(attackTime <= 0)
        {
            animator.SetBool("Beaming", false);
            animator.SetBool("Shooting", false);
            animator.SetBool("Charging", false);
            animator.SetBool("Spinning", false);
            animator.SetBool("Slamming", false);
            RockBoss.SwitchState(RockBoss.IdleState);
        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(RockBossStateManager RockBoss, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(RockBossStateManager RockBoss, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(RockBossStateManager RockBoss)
    {
        Vector2 knockbackDirection = (Vector2)(RockBoss.transform.position - RockBoss.attackPointX.position).normalized;
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(RockBoss.attackPointX.position, new Vector2(RockBoss.attackRange * 2, RockBoss.attackHeight), CapsuleDirection2D.Vertical, mask);

        

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
    public override void TakeDamage(RockBossStateManager RockBoss)
    {
        RockBoss.SwitchState(RockBoss.HitState);
    }
}
