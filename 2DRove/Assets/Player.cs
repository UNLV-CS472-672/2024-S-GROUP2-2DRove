using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }
}
