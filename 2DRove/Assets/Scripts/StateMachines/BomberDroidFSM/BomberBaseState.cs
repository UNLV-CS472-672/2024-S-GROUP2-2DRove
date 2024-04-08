using UnityEngine;

public abstract class BomberBaseState
{
    public abstract void EnterState(BomberStateManager bomber);

    public abstract void UpdateState(BomberStateManager bomber);

    public abstract void OnCollisionEnter2D(BomberStateManager bomber, Collision2D collision);

    public abstract void OnTriggerStay2D(BomberStateManager bomber, Collider2D collider);

    public abstract void EventTrigger(BomberStateManager bomber);

    public abstract void TakeDamage(BomberStateManager bomber);
}
