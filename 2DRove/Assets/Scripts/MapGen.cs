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
        // Dictionary to store positions of tiles
        public static Dictionary<Vector2Int, GameObject> tilePositions = new();

        void Start()
        {
            // // Check if the number of steps and cycles is too large for the given bounds
            // if(steps*cycles > (maxX+maxY)*(Mathf.Abs(minX)+Mathf.Abs(minY)))
            // {
            //     Debug.LogError("The number of steps and cycles is too large for the given bounds.");
            //     return;
            // }
            // ONLY TO BE USED FOR TESTING PURPOSES, BORDER IS DRAWN BY EMPTY SPACE METHOD
            // //Create Black Border Line reprenseting the outline boundary
            // DrawBorder drawBorder = new();
            // drawBorder.DrawBorderLine((minX-1)*tileSizeX, (minY-1)*tileSizeY, (maxX+1)*tileSizeX, (maxY+1)*tileSizeY);
        
            // Generate tiles
            for (int j = 0; j < cycles; j++)
            {
                // Initial position
                Vector2Int currentPosition = Vector2Int.zero;
                for (int i = 0; i < steps; i++)
                {
                    // Check if the position is already occupied
                    if (!tilePositions.ContainsKey(currentPosition))
                    {
                        // Create a new tile at the current position
                        GameObject newTile = Instantiate(tile, new Vector3(currentPosition.x * tileSizeX, currentPosition.y * tileSizeY, 0), Quaternion.identity);
                        newTile.name = "Tile(" + currentPosition.x + ", " + currentPosition.y + ")";
                        newTile.transform.localScale = new Vector3(tileSizeX, tileSizeY, 1);
                        newTile.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
                        tilePositions.Add(currentPosition, newTile);
                    } else 
                        i--; // Handle overlapping position or visited position

                    // Update current position based on random direction
                    switch (Random.Range(0,4))
                    {
                        /*
                            Mathf.Min returns the smaller of the two numbers
                            Mathf.Max returns the larger of the two numbers
                            Example: Mathf.Min(5, 10) returns 5
                        */
                        case 0:
                            currentPosition.x = Mathf.Min(currentPosition.x + 1, maxX);
                            break;
                        case 1:
                            currentPosition.x = Mathf.Max(currentPosition.x - 1, minX);
                            break;
                        case 2:
                            currentPosition.y = Mathf.Min(currentPosition.y + 1, maxY);
                            break;
                        case 3:
                            currentPosition.y = Mathf.Max(currentPosition.y - 1, minY);
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
                    if (!tilePositions.ContainsKey(new Vector2Int(i, j)))
                    {
                        GameObject emptyTiles = Instantiate(tile, new Vector3(i * tileSizeX, j * tileSizeY, 0), Quaternion.identity);
                        emptyTiles.name = "EmptyTile(" + i + ", " + j + ")";
                        emptyTiles.transform.localScale = new Vector3(tileSizeX, tileSizeY, 1);
                        emptyTiles.GetComponent<SpriteRenderer>().color = Color.black;
                        emptyTiles.AddComponent<BoxCollider2D>();
                        tilePositions.Add(new Vector2Int(i, j), emptyTiles);
                    }
                }
            Debug.Log("Empty Space generated: " + tilePositions.Count);
        }
    }
}