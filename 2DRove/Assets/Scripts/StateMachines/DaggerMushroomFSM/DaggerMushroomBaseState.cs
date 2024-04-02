using UnityEngine;

public abstract class DaggerMushroomBaseState
{
    public abstract void EnterState(DaggerMushroomStateManager mushroom);
    public abstract void UpdateState(DaggerMushroomStateManager mushroom);
    public abstract void OnCollisionEnter2D(DaggerMushroomStateManager mushroom, Collision2D other);
    public abstract void OnTriggerStay2D(DaggerMushroomStateManager mushroom, Collider2D other);
    public abstract void EventTrigger(DaggerMushroomStateManager mushroom);
    public abstract void TakeDamage(DaggerMushroomStateManager mushroom);
}
