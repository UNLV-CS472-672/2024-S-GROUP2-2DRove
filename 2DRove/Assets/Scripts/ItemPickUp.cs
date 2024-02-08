using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public enum pickupObject {COIN, POTION}; //potion for future dev
    public pickupObject currentObject;
    public int coinQuantity;
    public int healAmount;
    

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            if (currentObject == pickupObject.COIN){
                PickUpCoin();
            }
            else if (currentObject == pickupObject.POTION){
                PickUpPotion();
            }
        }
    }

    void PickUpCoin(){
        PlayerStats.playerStats.AddCoin(this);
        Debug.Log("Picked up " + coinQuantity + " coins"); // for testing
        Destroy(gameObject);
    }

    void PickUpPotion(){
        PlayerStats.playerStats.HealCharacter(healAmount);
        Debug.Log("Healed " + healAmount + " health"); // for testing
        Destroy(gameObject);
    }
}
