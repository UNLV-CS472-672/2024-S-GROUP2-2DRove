using UnityEngine;

public abstract class RockBossBaseState
{
    public abstract void EnterState(RockBossStateManager RockBoss);
    public abstract void UpdateState(RockBossStateManager RockBoss);
    public abstract void OnCollisionEnter2D(RockBossStateManager RockBoss, Collision2D other);
    public abstract void OnTriggerStay2D(RockBossStateManager RockBoss, Collider2D other);
    public abstract void EventTrigger(RockBossStateManager RockBoss);
    public abstract void TakeDamage(RockBossStateManager RockBoss);
}
