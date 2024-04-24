using UnityEngine;

public class SpiderAttackState : SpiderBaseState
{
    private float attackTime = 5.0f;
    private Animator animator;
    private AudioSource attackSound;
    public override void EnterState(SpiderStateManager Spider)
    {
        Debug.Log("Entering Attack State");
        attackTime = 2.0f;
        animator = Spider.GetComponent<Animator>();
        AudioSource[] sources = Spider.GetComponents<AudioSource>();
        attackSound = sources[0];
        animator.SetBool("attacking", true);
    }

    public override void UpdateState(SpiderStateManager Spider)
    {
        if(attackTime <= 0)
        {
            Spider.SwitchState(Spider.IdleState);
            animator.SetBool("attacking", false);
        }

        attackTime -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(SpiderStateManager Spider, Collision2D other)
    {
        
    }
    
    public override void OnTriggerStay2D(SpiderStateManager Spider, Collider2D other) 
    {

    }

    //done in animation events
    public override void EventTrigger(SpiderStateManager Spider)
    {
        Vector2 knockbackDirection = (Vector2)(Spider.transform.position - Spider.attackPoint.position).normalized;
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Spider.attackPoint.position, Spider.attackRange, mask);
        attackSound.Play();
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

    public override void TakeDamage(SpiderStateManager Spider)
    {
        attackSound.Stop();
        Spider.SwitchState(Spider.HitState);
    }
}
