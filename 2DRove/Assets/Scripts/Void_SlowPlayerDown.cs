using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void_SlowPlayerDown : MonoBehaviour
{
    [SerializeField] float basePlayerSpeed = 20f,
        slowSpeed = 5f;
    bool playerInZone = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !playerInZone)
        {
            playerInZone = true;
            Debug.Log("Player entered slow zone");
            other.gameObject.GetComponent<PlayerStateManager>().MovementSpeed = slowSpeed;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && playerInZone)
        {
            playerInZone = false;
            Debug.Log("Player exited slow zone");
            other.gameObject.GetComponent<PlayerStateManager>().MovementSpeed = basePlayerSpeed;
        }
    }
}
