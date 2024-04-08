using UnityEngine;

public abstract class WidowBaseState
{
    public abstract void EnterState(WidowStateManager Widow);
    public abstract void UpdateState(WidowStateManager Widow);
    public abstract void OnCollisionEnter2D(WidowStateManager Widow, Collision2D other);
    public abstract void OnTriggerStay2D(WidowStateManager Widow, Collider2D other);
    public abstract void EventTrigger(WidowStateManager Widow);
    public abstract void TakeDamage(WidowStateManager Widow);
}
