using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class Background : MonoBehaviour
{
    // List of tiles that can be interchanged when needed
    [SerializeField] GameObject[] tile;

    // Start is called before the first frame update
    void Start()
    {
        // Call our background gen in first frame
        GenerateBackground();
    }    

    void GenerateBackground()
    {
        // Loop x by y size to accomadate for the size of largest our map can be
        for(int x = -10; x < 10; x++)
        {
            for (int y = -10; y < 10; y++)
            {
                // Calculate the position of the tile based on our tile params
                float xPos = (x * 6.5f + y * 6.5f) * 10.0f;
                float yPos = (x * 6.5f - y * 6.5f) * 5.0f;
                // Create a new background tile
                GameObject background = Instantiate(getRandomTile(tile), new Vector3(xPos, yPos, 0), Quaternion.identity);
                //background.name = "Background(" + xPos + ", " + yPos + ")";
                // Set the scale of the background tile
                background.transform.localScale = new Vector3(6.5f, 6.5f, 1);
                // Set the sorting order of the background tile, to ensure that it is behind all other assets
                background.GetComponent<Renderer>().sortingOrder = -5;
            }
        }
    }
    // Method to get a random tile from tile set
    private GameObject getRandomTile(GameObject[] tileSet){
        return tileSet[Random.Range(0, tileSet.Length)];
    }
}
