using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] GameObject tile;

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
                float xPos = (x * 2 + y * 2) * 10;
                float yPos = (x * 2 - y * 2) * 5;
                
                GameObject background = Instantiate(tile, new Vector3(xPos, yPos, 0), Quaternion.identity);
                background.name = "Background(" + xPos + ", " + yPos + ")";
                background.transform.localScale = new Vector3(2, 2, 1);
                //background.GetComponent<Renderer>().sortingOrder = -1;
            }
        }
    }
}
