using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerStateManager Player);
    public abstract void UpdateState(PlayerStateManager Player);
    public abstract void OnCollisionEnter2D(PlayerStateManager Player, Collision2D other);
    public abstract void OnTriggerStay2D(PlayerStateManager Player, Collider2D other);
    public abstract void EventTrigger(PlayerStateManager Player);
    public abstract void TakeDamage(PlayerStateManager Player);
}
