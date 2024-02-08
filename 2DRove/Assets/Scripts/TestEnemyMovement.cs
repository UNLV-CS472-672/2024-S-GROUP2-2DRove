using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Speed of movement
    public float changeDirectionInterval = 2.0f; // Time interval to change direction
    public float movementRange = 5.0f; // Maximum range for movement

    private float timer;
    private Vector2 randomDirection;

    void Start()
    {
        timer = changeDirectionInterval;
        GetRandomDirection();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GetRandomDirection();
            timer = changeDirectionInterval;
        }

        Move();
    }

    void GetRandomDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Move()
    {
        Vector3 newPosition = transform.position + (Vector3)(randomDirection * moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, newPosition) < movementRange)
        {
            transform.position = newPosition;
        }
        else
        {
            GetRandomDirection();
        }
    }
}
