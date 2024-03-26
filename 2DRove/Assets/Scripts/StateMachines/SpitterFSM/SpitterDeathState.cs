using UnityEngine; 
public class SpitterDeathState : SpitterBaseState
{
    
    public override void EnterState(SpitterStateManager spitter)
    {
        Debug.Log("Entering Death State...");
        spitter.animator.SetBool("isDead", true);
        spitter.GetComponent<Collider2D>().enabled = false;
        // spitter.GetComponent<CapsuleCollider2D>().enabled = false;
        spitter.enabled = false;
        // wait for 1 second
        spitter.Destroy(.81f);
    }

    public override void UpdateState(SpitterStateManager spitter)
    {

    }

    public override void OnCollisionEnter2D(SpitterStateManager spitter, Collision2D other)
    {
        
    }

    public override void OnTriggerStay2D(SpitterStateManager spitter, Collider2D other) {
    }

    public override void EventTrigger(SpitterStateManager spitter)
    {

    }

    public override void TakeDamage(SpitterStateManager spitter)
    {

    }
}
