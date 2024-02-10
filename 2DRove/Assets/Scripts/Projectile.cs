using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileLifetime;
    [SerializeField] private float velocity;
    [SerializeField] private float damage;

    private void Awake(){
        Destroy(gameObject, projectileLifetime); //Destroys the projectile, delayed by the lifetime in seconds
    }

    public void setDirection(Vector2 direction){
        GetComponent<Rigidbody2D>().velocity = direction.normalized * velocity; //Modifies the direction of the projectile by multiplying the velocity by the normalized direction values
    }

    private void OnTriggerEnter2D(Collider2D col){
        GameObject target = col.gameObject; //Saves the game object the projectile collided with

        if(target.CompareTag("Player")){ //If it collided with a valid player or enemy, deals damage to them
            target.GetComponent<PlayerController>().dealDamage(damage);
        }else if(target.CompareTag("Enemy")){
            target.GetComponent<Enemy>().dealDamage(damage);
        }

        Destroy(gameObject); //Immediately destroys the projectile on contact with a valid object
    }
}
