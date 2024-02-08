using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform player; //whenever you need player's position, use Transform
    public float minDamage;
    public float maxDamage;
    public float projectileSpeed;
    public float projectileLifetime = 3f; // the lifetime of the projectile is 5 seconds
    public float cooldown;

    void Start(){
        StartCoroutine(AttackPlayer());
    }

    IEnumerator AttackPlayer(){
        yield return new WaitForSeconds(cooldown);
        if (player != null){
            GameObject attack = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 thisPos = transform.position;
            Vector2 targetPos = player.position;
            Vector2 direction = (targetPos - thisPos).normalized;
            attack.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            attack.GetComponent<TestEnemyAttackProjectile>().damage = Random.Range(minDamage, maxDamage);
            StartCoroutine(AttackPlayer());

            Destroy(attack, projectileLifetime); 
        }
        
    }
}
