using UnityEngine;
using UnityEngine.UIElements;

public class SpitterIdleState : SpitterBaseState
{
    private Transform playerTransform;

    public override void EnterState(SpitterStateManager spitter)
    {
        //Debug.Log("Entering Idle State...");

        // Use FindWithTag to get the Transform component of the Player GameObject
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player not found in EnterState!");
            playerTransform = null; // Set to null to avoid future errors
        }
    }

    public override void UpdateState(SpitterStateManager spitter)
    {
        // Check for null to ensure the player is still in the scene
        if (playerTransform != null)
        {
            spitter.SwitchState(spitter.AggroState);
        }
        else
        {
            // If the player is not found, stay in Idle or consider switching to a different state
            // Debug.LogWarning("Player not found in UpdateState!");
        }
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
        spitter.SwitchState(spitter.HitState);
    }


}
