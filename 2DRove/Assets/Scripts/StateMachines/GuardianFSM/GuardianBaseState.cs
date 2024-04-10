using UnityEngine;

public abstract class GuardianBaseState
{
    public abstract void EnterState(GuardianStateManager Guardian);
    public abstract void UpdateState(GuardianStateManager Guardian);
    public abstract void OnCollisionEnter2D(GuardianStateManager Guardian, Collision2D other);
    public abstract void OnTriggerStay2D(GuardianStateManager Guardian, Collider2D other);
    public abstract void EventTrigger(GuardianStateManager Guardian, int attackID);
    public abstract void TakeDamage(GuardianStateManager Guardian);
}
