using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class PlayerDashState : PlayerBaseState
{
    private float dashDistance;
    private float dashDuration;
    private float dashCooldown;
    private Vector2 startLoc;
    private Vector2 dashLoc;
    private RaycastHit2D hit;
    public override void EnterState(PlayerStateManager Player)
    {
        //Idle and walking handled in this state
        Debug.Log("Entering Dash State...");
        Player.afterImage.makeGhost = true;
        dashDistance = Player.dashDistance;
        dashDuration = Player.dashDuration;
        dashCooldown = Player.dashCooldown;

        //First we raycast from the player in the direction and distance specified of the blink. The layermask is there so it only collides with colliders in the Default layer. We raycast to get collisions so the player cant teleport into/through walls or other objects.
        hit = Physics2D.Raycast(Player.transform.position, Player.inputDirection, dashDistance, LayerMask.GetMask("Default"));

        if(hit){ //If the raycast collides with an object
            dashLoc = (Player.inputDirection * (hit.distance - 1f)) + (Vector2)Player.transform.position; //Teleports the player to the object that the raycast collided with, we subtract 1 from hit.distance to prevent the player from teleporting into the block
            Debug.DrawRay(Player.transform.position, Player.inputDirection * (hit.distance - 1f), Color.red, 10f);
        }else{//If the raycast doesnt collide with anything then there is nothing in the way of the player blinking
            dashLoc = (Player.inputDirection * dashDistance) + (Vector2)Player.transform.position; //Teleports the player to the object that the raycast collided with, we subtract 1 from hit.distance to prevent the player from teleporting into the block
            Debug.DrawRay(Player.transform.position, Player.inputDirection * dashDistance, Color.red, 10f);
        }

        startLoc = Player.transform.position;
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        if(dashDuration < 0)
        {
            // Player.animator.SetTrigger("neutral");
            Player.afterImage.makeGhost = false;
            Player.lastDashedTime = Time.time; //Updates when the player blinked last, putting the blink on cooldown
            Player.SwitchState(Player.NeutralState);
        }

        
        Player.GetComponent<Transform>().position = Vector2.Lerp(dashLoc, startLoc, dashDuration/Player.dashDuration);
        // Debug.Log(Vector2.Lerp(dashLoc, startLoc, dashDuration/Player.dashDuration));
        // Debug.Log(dashDuration);
        dashDuration -= Time.deltaTime;
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
    }

}
