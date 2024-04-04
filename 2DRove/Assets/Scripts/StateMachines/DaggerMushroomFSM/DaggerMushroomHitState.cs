using UnityEngine;

public class DaggerMushroomHitState : DaggerMushroomBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = .41f;
    public override void EnterState(DaggerMushroomStateManager mushroom)
    {
        Debug.Log("Entering Hit State...");
        animator = mushroom.GetComponent<Animator>();
        //set animation bool hitstunn to true or smth
        // NO NEED TO SET TRIGGER bc its done in NewEnemy for now
        // animator.SetTrigger("hit");

        health = mushroom.GetComponent<NewEnemy>().CurrentHeath();
        if (health <= 0)
        {
            mushroom.SwitchState(mushroom.DeathState);
        }
    }

    public override void UpdateState(DaggerMushroomStateManager mushroom)
    {
        if (hitStun <= 0)
        {
            mushroom.SwitchState(mushroom.IdleState);
        }

        hitStun -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(DaggerMushroomStateManager mushroom, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(DaggerMushroomStateManager mushroom, Collider2D other) {
    }

    public override void EventTrigger(DaggerMushroomStateManager mushroom)
    {

    }

    public override void TakeDamage(DaggerMushroomStateManager mushroom)
    {
        mushroom.SwitchState(mushroom.HitState);
    }
}
