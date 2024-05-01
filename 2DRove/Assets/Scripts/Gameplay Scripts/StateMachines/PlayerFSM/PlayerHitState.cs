using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    private float health;
    private Animator animator;
    private float hitStun = .41f;
    public override void EnterState(PlayerStateManager Player)
    {
        //Debug.Log("Entering Hit State...");
        animator = Player.GetComponent<Animator>();
        //set animation bool hitstunn to true or smth
        // NO NEED TO SET TRIGGER bc its done in NewEnemy for now
        // animator.SetTrigger("hit");

        health = Player.GetComponent<PlayerController>().CurrentHealth();
        if (health <= 0)
        {
            Player.SwitchState(Player.DeathState);
        }
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        if (hitStun <= 0)
        {
            Player.SwitchState(Player.NeutralState);
        }

        hitStun -= Time.deltaTime;
    }

    public override void OnCollisionEnter2D(PlayerStateManager Player, Collision2D other)
    {
        
    } 

    public override void OnTriggerStay2D(PlayerStateManager Player, Collider2D other) {
    }

    public override void EventTrigger(PlayerStateManager Player)
    {

    }

    public override void TakeDamage(PlayerStateManager Player)
    {
        Player.SwitchState(Player.HitState);
    }
}
