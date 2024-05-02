using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSpitterProjectile : MonoBehaviour
{
    [SerializeField] private float projectileLifetime;
    [SerializeField] private float velocity;
    [SerializeField] private float damage;
    [SerializeField] private float rangedDamage;
    [SerializeField] private float knockbackStrength = 2f;

    private void Awake()
    {
        Destroy(gameObject, projectileLifetime); //Destroys the projectile, delayed by the lifetime in seconds
    }

    public void setDirection(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction.normalized * velocity; //Modifies the direction of the projectile by multiplying the velocity by the normalized direction values

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject target = col.gameObject; //Saves the game object the projectile collided with
        Vector2 knockbackDirection = (Vector2)(transform.position - target.transform.position).normalized;
        if (target.CompareTag("Player"))
        { //If it collided with a valid player or enemy, deals damage to them
            target.GetComponent<PlayerController>().dealDamage(damage);
        }
        // wait for 1 second for animation
        Destroy(gameObject, 0.3f);
    }
}
