using UnityEngine;

public abstract class SpiderBaseState
{
    public abstract void EnterState(SpiderStateManager Spider);
    public abstract void UpdateState(SpiderStateManager Spider);
    public abstract void OnCollisionEnter2D(SpiderStateManager Spider, Collision2D other);
    public abstract void OnTriggerStay2D(SpiderStateManager Spider, Collider2D other);
}
