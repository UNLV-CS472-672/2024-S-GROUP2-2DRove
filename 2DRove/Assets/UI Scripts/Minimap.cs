using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{   
    private Camera MinimapCamera;       // Reference to the minimap camera

    private float MinimapScale;         // Variable to store the minimap zoom

    // Start is called before the first frame update
    private void Start(){
        MinimapCamera = GameObject.Find("Camera").GetComponent<Camera>();       // Get the minimap camera component
        MinimapScale = MinimapCamera.orthographicSize;                                  // Get the initial zoom of the minimap
    }                            

    // Function to zoom in the minimap
    public void MinimapZoomIn()
    {
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
