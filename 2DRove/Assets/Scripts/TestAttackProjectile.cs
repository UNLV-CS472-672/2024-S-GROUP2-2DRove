using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttackProjectile : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile") || collision.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject); // Destroy the collided projectile
            Destroy(gameObject); // Destroy this projectile
        }
        else if (collision.CompareTag("Enemy") && collision.GetComponent<EnemyGetDamage>() != null)
        {
            collision.GetComponent<EnemyGetDamage>().DealDamage(damage);
            Destroy(gameObject); 
        }
    }
}
