using UnityEngine;

public abstract class FoxBaseState
{
    public abstract void EnterState(FoxStateManager fox);

    public abstract void UpdateState(FoxStateManager fox);

    public abstract void OnCollisionEnter2D(FoxStateManager fox, Collision2D collision);

    public abstract void OnTriggerStay2D(FoxStateManager fox, Collider2D collider);

    public abstract void EventTrigger(FoxStateManager fox);

    public abstract void TakeDamage(FoxStateManager fox);
}
