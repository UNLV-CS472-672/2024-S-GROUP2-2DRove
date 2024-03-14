using UnityEngine;

public class GhoulHitState : GhoulBaseState
{
    private Animator animator;
    private float hitStun = .41f;
    public override void EnterState(GhoulStateManager ghoul)
    {
        Debug.Log("Entering Hit State...");
        animator = ghoul.GetComponent<Animator>();
        //set animation bool hitstunn to true or smth
        animator.SetTrigger("hit");
    }

    public override void UpdateState(GhoulStateManager ghoul)
    {
        if (hitStun <= 0)
        {
            ghoul.SwitchState(ghoul.IdleState);
        }

        hitStun -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(GhoulStateManager ghoul, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(GhoulStateManager ghoul, Collider2D other) {
    }

    public override void EventTrigger(GhoulStateManager ghoul)
    {

    }

    public override void TakeDamage(GhoulStateManager ghoul)
    {
        ghoul.SwitchState(ghoul.HitState);
    }
}
