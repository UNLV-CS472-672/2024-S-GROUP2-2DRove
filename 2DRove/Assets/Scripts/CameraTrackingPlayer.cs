using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackingPlayer : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed; // 0.05 on the inspector with fixed frame rate is recommended
    public Vector3 offset; //set to -1 on the z axis so the camera is behind the player

    void FixedUpdate(){ // fixed frame rate makes the camera movement identical on all devices
    // if i use Update() the camera movement will be faster on a device with higher frame rate, the smoothSpeed wont be accurate
        if(player != null){ // check if player is dead
            Vector3 desiredPosition = Vector3.Lerp(transform.position, player.transform.position + offset, smoothSpeed);
            transform.position = desiredPosition;
        }
    }
}
