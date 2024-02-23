using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    [SerializeField] Sprite startSprite;
    [SerializeField] int cycles = 5;
    [SerializeField] int steps = 50;
    [SerializeField] int tileSizeX = 5;
    [SerializeField] int tileSizeY = 8;

    void Start()
    {
        // Dictionary to store positions of tiles
        var tilePositions = new Dictionary<Vector2Int, GameObject>();
    
        // Generate sprites
        for (int j = 0; j < cycles; j++)
        {
            // Initial position
            Vector2Int currentPosition = Vector2Int.zero;
            for (int i = 0; i < steps; i++)
            {
                // Check if the position is already occupied
                if (tilePositions.ContainsKey(currentPosition))
                {
                    // Handle overlapping position or visited position
                    Debug.Log("Overlap detected at position: " + currentPosition + " or position already visited.");
                    i--;
                }
                else
                {
                    // Instantiate new tile
                    GameObject go = new("Tile" + j + "-" + i);
                    go.transform.position = new Vector3(currentPosition.x * tileSizeX, currentPosition.y * tileSizeY, 0);
                    go.AddComponent<SpriteRenderer>().sprite = startSprite;
                    go.transform.localScale = new Vector3(tileSizeX, tileSizeY, 1);
                    go.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                    // Store tile position in the dictionary
                    tilePositions.Add(currentPosition, go);
                }

                // Randomly update current position
                switch (Random.Range(0, 4))
                {
                    case 0:
                        currentPosition.x++;
                        break;
                    case 1:
                        currentPosition.x--;
                        break;
                    case 2:
                        currentPosition.y++;
                        break;
                    case 3:
                        currentPosition.y--;
                        break;
                }
            }
            Debug.Log("Tiles generated: " + tilePositions.Count);
        }
    }
}
