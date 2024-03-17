using UnityEngine;

public abstract class GhoulBaseState
{
    public abstract void EnterState(GhoulStateManager ghoul);
    public abstract void UpdateState(GhoulStateManager ghoul);
    public abstract void OnCollisionEnter2D(GhoulStateManager ghoul, Collision2D other);
    public abstract void OnTriggerStay2D(GhoulStateManager ghoul, Collider2D other);
    public abstract void EventTrigger(GhoulStateManager ghoul);
    public abstract void TakeDamage(GhoulStateManager ghoul);
}
