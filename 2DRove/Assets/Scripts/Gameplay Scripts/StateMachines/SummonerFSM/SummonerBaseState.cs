using UnityEngine;

public abstract class SummonerBaseState
{
    public abstract void EnterState(SummonerStateManager summoner);
    public abstract void UpdateState(SummonerStateManager summoner);
    public abstract void OnCollisionEnter2D(SummonerStateManager summoner, Collision2D other);
    public abstract void OnTriggerStay2D(SummonerStateManager summoner, Collider2D other);
    public abstract void EventTrigger(SummonerStateManager summoner);
    public abstract void TakeDamage(SummonerStateManager summoner);
}
