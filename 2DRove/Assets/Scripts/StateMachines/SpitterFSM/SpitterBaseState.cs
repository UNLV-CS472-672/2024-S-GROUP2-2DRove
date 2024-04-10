using UnityEngine;

public abstract class SpitterBaseState
{
    public abstract void EnterState(SpitterStateManager spitter);

    public abstract void UpdateState(SpitterStateManager spitter);

    public abstract void OnCollisionEnter2D(SpitterStateManager spitter, Collision2D other);

    public abstract void OnTriggerStay2D(SpitterStateManager spitter, Collider2D other);

    public abstract void EventTrigger(SpitterStateManager spitter);
    public abstract void TakeDamage(SpitterStateManager spitter);
}
