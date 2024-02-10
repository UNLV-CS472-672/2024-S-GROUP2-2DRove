using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackingPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed; //0.05 on the inspector with fixed frame rate is recommended. How long it takes for the camera to reach the player
    [SerializeField] private Vector3 offset; //Set to -1 on the z axis so the camera is behind the player

    private void FixedUpdate(){
        if(player != null){
            Vector3 desiredPosition = Vector3.Lerp(transform.position, player.transform.position + offset, smoothSpeed); //Linear interpolates the camera's postion to the player
            transform.position = desiredPosition;
        }
    }
}
