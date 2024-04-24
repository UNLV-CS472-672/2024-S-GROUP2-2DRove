using System.Collections;
using UnityEngine;

public class WidowAttackState : WidowBaseState
{
    private float attackTime;
    private Animator animator;
    private int returnAttackType;
    private AudioSource spitSound;
    private AudioSource attackSound;
    private AudioSource jumpSound;
    public override void EnterState(WidowStateManager Widow)
    {
        Debug.Log("Entering Attack State");
        animator = Widow.GetComponent<Animator>();
        AudioSource[] sources = Widow.GetComponents<AudioSource>();
        jumpSound = sources[0];
        spitSound = sources[1];
        attackSound = sources[2];
        PerformRandomAttack(Widow);
    }

    private void PerformRandomAttack(WidowStateManager Widow)
    {
        int attackType = Random.Range(0, 3);
        Debug.Log(attackType);
        switch (attackType)
        {
            case 0:
                animator.SetBool("isJumping", true);
                attackTime = Widow.jumpTime / Widow.jumpSpeed;
                Debug.Log("Jumping");
                break;
            case 1:
                animator.SetBool("isSpitting", true);
                attackTime = Widow.spitTime / Widow.spitSpeed;
                Debug.Log("Spitting");
                break;
            case 2:
                animator.SetBool("isAttacking", true);
                attackTime = Widow.attackTime / Widow.attackSpeed;
                Debug.Log("Attacking");
                break;
        }
        returnAttackType = attackType;
    }

    public override void UpdateState(WidowStateManager Widow)
    {
        if(attackTime <= 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isSpitting", false);
            animator.SetBool("isAttacking", false);
            Widow.SwitchState(Widow.IdleState);
        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(WidowStateManager Widow, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(WidowStateManager Widow, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(WidowStateManager Widow)
    {
        Vector2 knockbackDirection = (Vector2)(Widow.transform.position - Widow.attackPointX.position).normalized;
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(Widow.attackPointX.position, new Vector2(Widow.attackRange * 2, Widow.attackHeight), CapsuleDirection2D.Vertical, mask);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerController playerScript = collider.GetComponent<PlayerController>();
                if (returnAttackType == 1) //spitting attack
                {
                    spitSound.Play();
                    playerScript.dealDamage(15);
                    collider.GetComponent<Rigidbody2D>().AddForce(-knockbackDirection * 25, ForceMode2D.Impulse);
                    collider.GetComponent<Animator>().SetTrigger("Hit");
                }
                else if (returnAttackType == 0) //jumping
                {
                    jumpSound.Play();
                    playerScript.dealDamage(10);
                    collider.GetComponent<Rigidbody2D>().AddForce(-knockbackDirection * 40, ForceMode2D.Impulse);
                    collider.GetComponent<Animator>().SetTrigger("Hit");
                }
                else if (returnAttackType == 2) //melee
                {
                    attackSound.Play();
                    playerScript.dealDamage(10);
                    collider.GetComponent<Rigidbody2D>().AddForce(-knockbackDirection * 5, ForceMode2D.Impulse);
                    collider.GetComponent<Animator>().SetTrigger("Hit");
                    playerScript.ReduceMoveSpeed(35, 1.5f);
                }
                
            }
        }
        
    }
    public override void TakeDamage(WidowStateManager Widow)
    {
        jumpSound.Stop();
        spitSound.Stop();
        attackSound.Stop();
        Widow.SwitchState(Widow.HitState);
    }
}
