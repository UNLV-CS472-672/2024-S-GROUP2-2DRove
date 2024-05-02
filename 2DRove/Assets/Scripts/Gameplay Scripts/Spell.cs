using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private float lastDamageTick;
    [SerializeField] private float damageTickRate;
    [SerializeField] private float projectileLifetime;
    private void Awake()
    {
        Destroy(gameObject, projectileLifetime); //Destroys the projectile, delayed by the lifetime in seconds
    }

    public void enableHitBox()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnTriggerStay2D(Collider2D other) {

        if (other.tag == "enemy" && Time.time > lastDamageTick + (1/damageTickRate))
        {
            NewEnemy enemy = other.GetComponent<NewEnemy>();
            enemy.TakeDamage(5);
            lastDamageTick = Time.time;
        }
    }
}
