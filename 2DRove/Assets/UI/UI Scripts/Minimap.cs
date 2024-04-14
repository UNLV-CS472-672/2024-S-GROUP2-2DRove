using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{   
    private Transform MinimapTransform; // Reference to the minimap transform

    private Transform player; // Reference to the minimap transform

    private Camera MinimapCamera;       // Reference to the minimap camera

    private float MinimapScale;         // Variable to store the minimap zoom

    // Start is called before the first frame update
    private void Start(){
        player = GameObject.Find("Player").GetComponent<Transform>(); // Get the player transform component
        MinimapTransform = GameObject.Find("MinimapCamera").GetComponent<Transform>();
        MinimapCamera = GameObject.Find("MinimapCamera").GetComponent<Camera>();       // Get the minimap camera component
        MinimapScale = MinimapCamera.orthographicSize;                                  // Get the initial zoom of the minimap
    }                    

    // Update is called once per frame
    private void Update(){
        MinimapTransform.position = new Vector3(player.position.x, player.position.y, -1); // Update the minimap position to the player position
        
    }        

    // Function to zoom in the minimap
    public void MinimapZoomIn()
    {
        Debug.Log("Zoom In");
        if(MinimapScale > 6){
            MinimapScale -= 3;    // Decaease the scale of the minimap by 3
            MinimapCamera.orthographicSize = MinimapScale;
        }
    }

    // Function to zoom out the minimap
    public void MinimapZoomOut()
    {
        if(MinimapScale < 20){
            MinimapScale += 3;    // Increase the scale of the minimap by 3
            MinimapCamera.orthographicSize = MinimapScale;
        }
    }

}

