using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public float lifetime = 5f;
    public LayerMask hitLayers;

    private Rigidbody2D rb;
    private Vector2 launchDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    public void Launch(Vector2 direction)
    {
        launchDirection = direction.normalized * speed;
        rb.velocity = launchDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we hit a layer that we can damage
        if (hitLayers == (hitLayers | (1 << collision.gameObject.layer)))
        {
            // Attempt to deal damage
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.dealDamage(damage);
            }
            // Here you can add more interactions, like if it hits an enemy or an environment object.

            Destroy(gameObject);
        }
    }
}