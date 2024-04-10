using UnityEngine;

public abstract class ElkBaseState
{
    public abstract void EnterState(ElkStateManager elk);

    public abstract void UpdateState(ElkStateManager elk);

    public abstract void OnCollisionEnter2D(ElkStateManager elk, Collision2D collision);

    public abstract void OnTriggerStay2D(ElkStateManager elk, Collider2D collider);

    public abstract void EventTrigger(ElkStateManager elk);

    public abstract void TakeDamage(ElkStateManager elk);
}
