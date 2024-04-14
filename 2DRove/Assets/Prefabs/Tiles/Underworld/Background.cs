using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class Background : MonoBehaviour
{
    [SerializeField] GameObject[] tile;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBackground();
    }    

    void GenerateBackground()
    {
        
        for(int x = -10; x < 10; x++)
        {
            for (int y = -10; y < 10; y++)
            {
                float xPos = (x * 6.5f + y * 6.5f) * 10.0f;
                float yPos = (x * 6.5f - y * 6.5f) * 5.0f;
                
                GameObject background = Instantiate(getRandomTile(tile), new Vector3(xPos, yPos, 0), Quaternion.identity);
                background.name = "Background(" + xPos + ", " + yPos + ")";
                background.transform.localScale = new Vector3(6.5f, 6.5f, 1);
                background.GetComponent<Renderer>().sortingOrder = -5;
            }
        }
    }
    private GameObject getRandomTile(GameObject[] tileSet){
        return tileSet[Random.Range(0, tileSet.Length)];
    }
}
