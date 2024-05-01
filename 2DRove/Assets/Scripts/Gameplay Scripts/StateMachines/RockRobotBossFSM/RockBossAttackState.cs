using System.Collections;
using UnityEngine;

public class RockBossAttackState : RockBossBaseState
{
    private float attackTime;
    private Animator animator;
    private int returnAttackType;
    public override void EnterState(RockBossStateManager RockBoss)
    {
        //Debug.Log("Entering Attack State");
        // attackTime = 20f;
        animator = RockBoss.GetComponent<Animator>();
        PerformRandomAttack(RockBoss);
    }

    // private IEnumerator AttackCoolDown()
    // {
    //     yield return new WaitForSeconds(2.5f);
    // }

    private void PerformRandomAttack(RockBossStateManager RockBoss)
    {
        int attackType = Random.Range(0, 4);
        Debug.Log(attackType);
        switch (attackType)
        {
            case 0:
                animator.SetBool("Beaming", true);
                attackTime = RockBoss.burstTime / RockBoss.burstSpeed;
                Debug.Log("Beaming");
                break;
            case 1:
                animator.SetBool("Shooting", true);
                attackTime = RockBoss.rangeAttackTime / RockBoss.rangeAttackSpeed;
                Debug.Log("Shooting");
                break;
            case 2:
                animator.SetBool("Charging", true);
                attackTime = RockBoss.buffTime / RockBoss.buffSpeed;
                Debug.Log("Charging");
                break;
            case 3:
                animator.SetBool("Slamming", true);
                attackTime = RockBoss.attackTime / RockBoss.attackSpeed;
                Debug.Log("Slamming");
                break; 
        }
        returnAttackType = attackType;
        // RockBoss.StartCoroutine(AttackCoolDown());
    }

    public override void UpdateState(RockBossStateManager RockBoss)
    {
        if(attackTime <= 0)
        {
            animator.SetBool("Beaming", false);
            animator.SetBool("Shooting", false);
            animator.SetBool("Charging", false);
            animator.SetBool("Slamming", false);
            RockBoss.SwitchState(RockBoss.AggroState);
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
                if (returnAttackType == 0) //beam attack
                {
                    playerScript.dealDamage(15);
                    collider.GetComponent<Rigidbody2D>().AddForce(-knockbackDirection * 25, ForceMode2D.Impulse);
                    collider.GetComponent<Animator>().SetTrigger("Hit");
                }
                else if (returnAttackType == 1) //shooting attack
                {
                    playerScript.dealDamage(12);
                    collider.GetComponent<Rigidbody2D>().AddForce(-knockbackDirection * 20, ForceMode2D.Impulse);
                    collider.GetComponent<Animator>().SetTrigger("Hit");
                }
                else if (returnAttackType == 2) //charging
                {
                    playerScript.dealDamage(10);
                    collider.GetComponent<Rigidbody2D>().AddForce(-knockbackDirection * 40, ForceMode2D.Impulse);
                    collider.GetComponent<Animator>().SetTrigger("Hit");
                }
                else if (returnAttackType == 3) //slamming
                {
                    playerScript.dealDamage(10);
                    collider.GetComponent<Rigidbody2D>().AddForce(-knockbackDirection * 5, ForceMode2D.Impulse);
                    collider.GetComponent<Animator>().SetTrigger("Hit");
                    playerScript.ReduceMoveSpeed(35, 1.5f);
                }
                
            }
        }
        
    }
    public override void TakeDamage(RockBossStateManager RockBoss)
    {
        RockBoss.SwitchState(RockBoss.HitState);
    }
}
