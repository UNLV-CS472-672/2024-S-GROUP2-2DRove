using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileSpeed;
    public float projectileLifetime = 5f; // the lifetime of the projectile is 5 seconds

    void Update(){
        if(Input.GetMouseButtonDown(1)){
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            spell.GetComponent<TestAttackProjectile>().damage = Random.Range(minDamage, maxDamage);

            Destroy(spell, projectileLifetime); // destroy the projectile after 5 seconds
        }
    }
}
