/*
using System.Collections.Generic;
using UnityEngine;

namespace MapGen
{
    public class MapGen : MonoBehaviour
    {
        [SerializeField] GameObject tile;
        [SerializeField] int cycles = 2;
        [SerializeField] int steps = 50;
        [SerializeField] int tileSizeX = 20;
        [SerializeField] int tileSizeY = 20;
        [SerializeField] int maxX = 5;
        [SerializeField] int maxY = 5;
        [SerializeField] int minX = -5;
        [SerializeField] int minY = -5;

        enum Direction { UpRight, DownLeft, UpLeft, DownRight }

        // Dictionary to store positions of tiles
        public static List<Vector2> tilePositions = new();

        void Start()
        {
            // Generate tiles
            for (int j = 0; j < cycles; j++)
            {
                // Initial position
                Vector2 currentPosition = new(0,0);
                for (int i = 0; i < steps; i++)
                {
                    // Check if the position is already occupied and within the bounds
                    // This works but idk how it works. :)
                    if (!tilePositions.Contains(new Vector2Int((int)currentPosition.x, (int)currentPosition.y)) && 
                        currentPosition.x >= minX && 
                        currentPosition.x <= maxX && 
                        currentPosition.y >= minY && 
                        currentPosition.y <= maxY)
                    {
                        // Create a new tile at the current position
                        GameObject newTile = Instantiate(tile, new Vector3(currentPosition.x, currentPosition.y, 0), Quaternion.identity);
                        newTile.name = "Tile(" + currentPosition.x + ", " + currentPosition.y + ")";
                        newTile.transform.localScale = new Vector3(tileSizeX, tileSizeY, 1);
                        tilePositions.Add(new Vector2Int((int)currentPosition.x, (int)currentPosition.y));
                    } else 
                        i--; // Handle overlapping position or visited position

                    // Update current position based on random direction
                    switch ((Direction)Random.Range(0,4))
                    {
                        /*
                            Mathf.Min returns the smaller of the two numbers
                            Mathf.Max returns the larger of the two numbers
                            Example: Mathf.Min(5, 10) returns 5
                        */
                        /*
                        case Direction.UpRight:
                            currentPosition.x = Mathf.Min(currentPosition.x + (10 * tileSizeX), maxX);
                            currentPosition.y = Mathf.Min(currentPosition.y + (5 * tileSizeY), maxY);
                            break;
                        case Direction.DownLeft:
                            currentPosition.x = Mathf.Min(currentPosition.x - (10 * tileSizeX), maxX);
                            currentPosition.y = Mathf.Min(currentPosition.y - (5 * tileSizeY), maxY);
                            break;
                        case Direction.UpLeft:
                            currentPosition.x = Mathf.Min(currentPosition.x + (10 * tileSizeX), maxX);
                            currentPosition.y = Mathf.Min(currentPosition.y - (5 * tileSizeY), maxY);
                            break;
                        case Direction.DownRight: 
                            currentPosition.x = Mathf.Min(currentPosition.x - (10 * tileSizeX), maxX);
                            currentPosition.y = Mathf.Min(currentPosition.y + (5 * tileSizeY), maxY);
                            break;
                    }
                }
                Debug.Log("Tiles generated: " + tilePositions.Count);
                Debug.Log("Generating tiles for empty space in border");
            }
           FillInEmptySpace();
        }

        // Fills in the empty space created between tiles and the border. 
        // Works very similar to the tile generation process but with a different color and name.
        void FillInEmptySpace()
        {
            // Generate empty space
            for (int i = minX-1; i <= maxX+1; i++)
                for (int j = minY-1; j <= maxY+1; j++)
                {
                    Debug.Log("Generating empty space for: " + i + " " + j);
                    // Check if the position is already occupied
                    if (!tilePositions.Contains(new Vector2Int(i, j)))
                    {
                        GameObject emptyTiles = Instantiate(tile, new Vector3(10*i * tileSizeX, j *5* tileSizeY, 0), Quaternion.identity);
                        emptyTiles.name = "EmptyTile(" + i + ", " + j + ")";
                        emptyTiles.transform.localScale = new Vector3(tileSizeX, tileSizeY, 1);
                        tilePositions.Add(new Vector2Int(i*10, j*5));
                    }
                }
            Debug.Log("Empty Space generated: " + tilePositions.Count);
        }
    }
}
*/