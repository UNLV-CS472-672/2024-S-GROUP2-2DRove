using UnityEngine;
using System.Collections;


public class BomberDeathState : BomberBaseState
{
    public Animator animator;
    private Transform shadowTransform;
    private Vector3 shadowOriginalPosition;
    private bool isDead = false;

    public override void EnterState(BomberStateManager bomber)
    {
        Debug.Log("Entering Death State...");
        bomber.animator.SetBool("isDead", true);
        bomber.GetComponent<Collider2D>().enabled = false;
        bomber.enabled = false;

        // wait for 1 second
        // bomber.Destroy(1f);

        // Find the shadow and store its original position
        shadowTransform = bomber.transform.Find("BombShadow");
        if (shadowTransform != null)
        {
            shadowOriginalPosition = shadowTransform.localPosition;
        }
        else
        {
            Debug.LogError("BombShadow not found as a child of the bomber!");
        }
        // isdead wait 1 second
        isDead = true;
        bomber.Destroy(1f);

    }

    public override void UpdateState(BomberStateManager bomber)
    {
       

    }


    public override void OnCollisionEnter2D(BomberStateManager bomber, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(BomberStateManager bomber, Collider2D other)
    {
    }

    public override void EventTrigger(BomberStateManager bomber)
    {

    }

    public override void TakeDamage(BomberStateManager bomber)
    {

    }
}
