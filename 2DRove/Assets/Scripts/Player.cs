using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MapGen;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float sprintSpeed = 50.0f;
    [SerializeField] private float moveSpeed = 30.0f;
    private float speed;
    float xSpeed, ySpeed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    /// <summary>
    /// This method is called at a fixed interval and is used for physics-related updates.
    /// It calculates the player's movement speed based on input and applies forces to the rigidbody.
    /// It also checks for collisions and handles them accordingly.
    /// </summary>
    void FixedUpdate()
    {
        xSpeed = playerInput.actions["Left"].IsPressed() ? -1 : playerInput.actions["Right"].IsPressed() ? 1 : 0;
        ySpeed = playerInput.actions["Down"].IsPressed() ? -1 : playerInput.actions["Up"].IsPressed() ? 1 : 0;
        if (playerInput.actions["Sprint"].IsPressed())
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = moveSpeed;
        }
        rb.AddForce(new Vector2(xSpeed, ySpeed) * speed);
        // // Calculate the new position of the player
        // Vector2Int newPosition = new Vector2Int(Mathf.RoundToInt(rb.position.x / MapGen.MapGen.tileSizeX + xSpeed / MapGen.MapGen.tileSizeX), Mathf.RoundToInt(rb.position.y / MapGen.MapGen.tileSizeY + ySpeed / MapGen.MapGen.tileSizeY));
        // // Check if the new position is an empty space
        // RaycastHit2D hit = Physics2D.Raycast(rb.position, new Vector2(xSpeed, ySpeed), 1.0f);
        // // If the new position is an empty space, move the player to the new position
        // if (hit.collider != null)
        // {
        //     // Check if the new position is an empty space
        //     if (MapGen.MapGen.emptySpacePositions.TryGetValue(newPosition, out GameObject value))
        //     {
        //         // Check if the new position is an empty space
        //         if (hit.collider.gameObject == value)
        //         {
        //             Debug.Log("You hit an empty space!");
        //         }
        //     }
        //     else
        //     {
        //         // Move character if it's not an empty space
        //         rb.AddForce(new Vector2(xSpeed, ySpeed) * speed);
        //     }
        // }
    }
}
