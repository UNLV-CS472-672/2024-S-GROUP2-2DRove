using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dashRange;
    private Vector2 direction; // vector2 is for 2d movement
    private Vector2 lastDirection; // for standing still
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

    void Update(){
        TakeInput();
        Move();
        checkDeath(); // death can be updated here
    }
    private void Move(){
        transform.Translate(direction * speed * Time.deltaTime); // movement

        if (direction.x != 0 || direction.y != 0){ // if moving, set animator
            SetAnimatorMovement(lastDirection);
        } 
        else{
            animator.SetLayerWeight(1, 0); // if not moving, set animator to idle
        } // setting the weight to 0 will make it so that the idle animation is not played, therefore walking animation is played
        
    }

    private void TakeInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W)){
            direction += Vector2.up;        // if pressing W and D, move diagonally, this is for dashing too
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }
        }
        else if (Input.GetKey(KeyCode.S)){
            direction += Vector2.down;      // if pressing W and D, move diagonally, this is for dashing too
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }
        }
        else if (Input.GetKey(KeyCode.D)){
            direction += Vector2.right;
        }
        else if (Input.GetKey(KeyCode.A)){
            direction += Vector2.left;
        }
        
        // Dash
        if (Input.GetKeyDown(KeyCode.Space)){
            Vector2 targetPos = (Vector2)transform.position + direction.normalized * dashRange;
            StartCoroutine(DashMovement(targetPos));
        }

        if (direction != Vector2.zero){
            lastDirection = direction;
        }
    }

    private IEnumerator DashMovement(Vector2 targetPos){
        float elapsedTime = 0f;
        Vector2 startPos = transform.position;

        while (elapsedTime < 1f){
            transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * speed; // Adjust the speed of the dash
            yield return null;
        }

        transform.position = targetPos; // Ensure the final position is set accurately
    }


    private void SetAnimatorMovement(Vector2 direction){
        animator.SetLayerWeight(1, 1); // if moving, set the weight to 1 so that the walking animation is played
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }

    public void checkDeath(){
        if (PlayerStats.playerStats.health <= 0){
            speed = 0;
            Destroy(gameObject);
        }
    }
}
