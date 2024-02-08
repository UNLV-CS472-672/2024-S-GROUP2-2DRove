using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAttackProjectile : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile") || collision.CompareTag("EnemyProjectile")){
            Destroy(collision.gameObject); // Destroy the collided projectile
            Destroy(gameObject); // Destroy this projectile
        }
        else if (collision.CompareTag("Player") && collision.GetComponent<EnemyGetDamage>() != null){ //friendly fire between enemies can be enabled here
            collision.GetComponent<EnemyGetDamage>().DealDamage(damage);
            Destroy(gameObject); 
        }
        else if (collision.tag == "Player"){ // player gets damaged
            PlayerStats.playerStats.DealDamage(damage);
            Destroy(gameObject);
        }
    }
}
