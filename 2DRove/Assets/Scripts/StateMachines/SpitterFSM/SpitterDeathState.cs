using UnityEngine;
public class SpitterDeathState : SpitterBaseState
{
    public override void EnterState(SpitterStateManager spitter)
    {
        Debug.Log("Entering Death State...");

        // Trigger the death animation
        spitter.animator.SetBool("isDead", true);

        // Ensure there is no residual movement or forces on the Rigidbody
        Rigidbody2D rb = spitter.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop any existing movement
            rb.isKinematic = true; // Prevent further physics interactions
        }

        // Disable the collider to prevent further collisions
        Collider2D collider = spitter.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Disable the SpitterStateManager script to stop any further updates or state transitions
        spitter.enabled = false;

        // Wait for a second to allow the animation to play before destroying the object 
        spitter.Destroy(1.3f);
    }

    public override void UpdateState(SpitterStateManager spitter)
    {

    }

    public override void OnCollisionEnter2D(SpitterStateManager spitter, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(SpitterStateManager spitter, Collider2D other)
    {
    }

    public override void EventTrigger(SpitterStateManager spitter)
    {

    }

    public override void TakeDamage(SpitterStateManager spitter)
    {

    }
}
