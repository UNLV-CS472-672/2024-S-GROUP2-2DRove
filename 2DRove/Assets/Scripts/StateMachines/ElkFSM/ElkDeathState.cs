using UnityEngine;
using System.Collections;


public class ElkDeathState : ElkBaseState
{
    public Animator animator;
    private bool isDead = false;

    public override void EnterState(ElkStateManager elk)
    {
        Debug.Log("Entering Death State...");
        elk.animator.SetBool("isDead", true);
        elk.GetComponent<Collider2D>().enabled = false;
        elk.enabled = false;

        // wait for 1 second before destroy
        elk.Destroy(1f);

    }
    public override void UpdateState(ElkStateManager elk)
    {


    }


    public override void OnCollisionEnter2D(ElkStateManager elk, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(ElkStateManager elk, Collider2D other)
    {
    }

    public override void EventTrigger(ElkStateManager elk)
    {

    }

    public override void TakeDamage(ElkStateManager elk)
    {

    }
}
