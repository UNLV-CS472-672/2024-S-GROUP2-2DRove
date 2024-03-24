using UnityEngine;

public abstract class WardenBaseState
{
    public abstract void EnterState(WardenStateManager Warden);
    public abstract void UpdateState(WardenStateManager Warden);
    public abstract void OnCollisionEnter2D(WardenStateManager Warden, Collision2D other);
    public abstract void OnTriggerStay2D(WardenStateManager Warden, Collider2D other);
    public abstract void EventTrigger(WardenStateManager Warden);
    public abstract void TakeDamage(WardenStateManager Warden);
}
