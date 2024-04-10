using UnityEngine;

public abstract class ArcherBaseState
{
    public abstract void EnterState(ArcherStateManager archer);
    public abstract void UpdateState(ArcherStateManager archer);
    public abstract void OnCollisionEnter2D(ArcherStateManager archer, Collision2D other);
    public abstract void OnTriggerStay2D(ArcherStateManager archer, Collider2D other);
    public abstract void EventTrigger(ArcherStateManager archer);
    public abstract void TakeDamage(ArcherStateManager archer);
}
