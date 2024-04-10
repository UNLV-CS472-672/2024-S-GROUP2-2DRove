using UnityEngine;

public abstract class CagedShockerBaseState
{
    public abstract void EnterState(CagedShockerStateManager CagedShocker);
    public abstract void UpdateState(CagedShockerStateManager CagedShocker);
    public abstract void OnCollisionEnter2D(CagedShockerStateManager CagedShocker, Collision2D other);
    public abstract void OnTriggerStay2D(CagedShockerStateManager CagedShocker, Collider2D other);
    public abstract void EventTrigger(CagedShockerStateManager CagedShocker);
    public abstract void TakeDamage(CagedShockerStateManager CagedShocker);
}
