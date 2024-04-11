using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public enum pickupObject {
        COIN, 
        POTION
    }; //Item types

    [SerializeField] private pickupObject currentObject;
    [SerializeField] private int coinQuantity;
    [SerializeField] private int healAmount;
    [SerializeField] private float speed;
    [SerializeField] private bool tracksToPlayer;
    private GameObject player;

    void Start(){
        player = GameObject.Find("Player"); //Finds the player when created
    }

    void FixedUpdate(){
        if (tracksToPlayer && player != null){
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed); //Teleports the object towards the player by the distance specified in speed
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){ //If collides with the player
            if (currentObject == pickupObject.COIN){
                pickUpCoin();
            }
            else if (currentObject == pickupObject.POTION){
                pickUpPotion();
            }
        }
    }
    
    //Destroys the coin and adds the value to the player
    void pickUpCoin(){
        player.GetComponent<PlayerController>().addCoin(coinQuantity);
        Destroy(gameObject);
    }

    //Destroys the potion and heals the player
    void pickUpPotion(){
        player.GetComponent<PlayerController>().healPlayer(healAmount);
        Destroy(gameObject);
    }
}
