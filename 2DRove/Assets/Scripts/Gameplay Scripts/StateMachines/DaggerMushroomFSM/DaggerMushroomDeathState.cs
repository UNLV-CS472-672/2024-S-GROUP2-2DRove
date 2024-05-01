using UnityEngine;

public class DaggerMushroomDeathState : DaggerMushroomBaseState
{
    
    public override void EnterState(DaggerMushroomStateManager mushroom)
    {
        //Debug.Log("Entering Death State...");
        mushroom.playerController.addCoin(mushroom.goldDropped);
        mushroom.animator.SetBool("isDead", true);
        mushroom.GetComponent<Collider2D>().enabled = false;
        // mushroom.GetComponent<CapsuleCollider2D>().enabled = false;
        mushroom.enabled = false;
        // wait for 1 second
        mushroom.Destroy(.81f);
    }

    public override void UpdateState(DaggerMushroomStateManager mushroom)
    {

    }

    public override void OnCollisionEnter2D(DaggerMushroomStateManager mushroom, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(DaggerMushroomStateManager mushroom, Collider2D other) {
    }

    public override void EventTrigger(DaggerMushroomStateManager mushroom)
    {

    }

    public override void TakeDamage(DaggerMushroomStateManager mushroom)
    {

    }
}
