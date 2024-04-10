using UnityEngine;

public class BomberHitState : BomberBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = 0.41f;
    public override void EnterState(BomberStateManager bomber)
    {
        Debug.Log("Entering Hit State...");
        animator = bomber.GetComponent<Animator>();

        health = bomber.GetComponent<NewEnemy>().CurrentHeath();
        Debug.Log("HP: " + health);
        if (health <= 0)
        {
            bomber.SwitchState(bomber.DeathState);
        }
    }

    public override void UpdateState(BomberStateManager bomber)
    {
        if (hitStun <= 0)
        {
            bomber.SwitchState(bomber.IdleState);
        }

        hitStun -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(BomberStateManager bomber, Collision2D other)
    {

    }

    public override void OnTriggerStay2D(BomberStateManager bomber, Collider2D other)
    {

    }
    public override void EventTrigger(BomberStateManager bomber)
    {

    }


 
    public override void TakeDamage(BomberStateManager bomber)
    {
        bomber.SwitchState(bomber.HitState);
    }
}
