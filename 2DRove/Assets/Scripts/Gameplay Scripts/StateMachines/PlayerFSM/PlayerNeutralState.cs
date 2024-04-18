using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class PlayerNeutralState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager Player)
    {
        //Idle and walking handled in this state
        Debug.Log("Entering Neutral State...");
        // Player.animator.ResetTrigger("neutral");
        Player.animator.ResetTrigger("slash1");
        Player.animator.ResetTrigger("slash2");
        Player.animator.ResetTrigger("slash3");
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        Player.inputDirection = new Vector2(Player.findDirectionFromInputs("Left", "Right"), Player.findDirectionFromInputs("Down", "Up")).normalized;
        // Player.lastInput = (Player.inputDirection != Vector2.zero) ? ((Player.lastInput * .80f) + Player.inputDirection * .20f).normalized : Player.lastInput;
        // Player.lastInput = inputDirection;

        //Multiplies the direction by the speed and applies it as a force. Default force type is ForceMode2D.Force
        Player.animator.SetFloat("yDir", Mathf.Abs(Player.inputDirection.y)); //Sets the vertical direction parameter in the animator to the player's y velocity
        Player.animator.SetFloat("xDir", Mathf.Abs(Player.inputDirection.x)); //Sets the velocity parameter in the animator to the absolute value of the player's x velocity. This is used to determine if the player is moving or not
        Player.rb.AddForce(Player.inputDirection * Player.MovementSpeed);

        if (Player.inputDirection.x != 0){ //If the player is moving horizontally
            Player.flipped = Player.inputDirection.x < 0; //If the player is moving left, flipped is true, if the player is moving right, flipped is false
        }

        
        Player.transform.rotation = Quaternion.Euler(new Vector3(0f, Player.flipped ? 180f: 0f, 0f));
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Player.SwitchState(Player.Slash1State);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > Player.lastDashedTime + Player.dashCooldown && Player.inputDirection != Vector2.zero)
        {
            Player.SwitchState(Player.DashState);
        }
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
