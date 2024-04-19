using UnityEngine;
using System.Collections;


public class FoxDeathState : FoxBaseState
{
    public Animator animator;
    private bool isDead = false;

    public override void EnterState(FoxStateManager fox)
    {
        Debug.Log("Entering Death State...");
        fox.animator.SetBool("isDead", true);
        fox.GetComponent<Collider2D>().enabled = false;
        fox.enabled = false;

        // wait for 1 second before destroy
        fox.Destroy(1f);

    }
    public override void UpdateState(FoxStateManager fox)
    {


    }


    public override void OnCollisionEnter2D(FoxStateManager fox, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(FoxStateManager fox, Collider2D other)
    {
    }

    public override void EventTrigger(FoxStateManager fox)
    {

    }

    public override void TakeDamage(FoxStateManager fox)
    {

    }
}
