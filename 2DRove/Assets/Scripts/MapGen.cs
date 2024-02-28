using System;
using System.Collections.Generic;
using UnityEngine;
using CreateBorder;

public class MapGen : MonoBehaviour
{
    [SerializeField] Sprite startSprite;
    [SerializeField] int cycles = 2;
    [SerializeField] int steps = 50;
    [SerializeField] int tileSizeX = 20;
    [SerializeField] int tileSizeY = 20;
    [SerializeField] int maxX = 5;
    [SerializeField] int maxY = 5;
    [SerializeField] int minX = -5;
    [SerializeField] int minY = -5;

    void Start()
    {
        // Check if the number of steps and cycles is too large for the given bounds
        if(steps*cycles > (maxX+maxY)*(Math.Abs(minX)+Math.Abs(minY)))
        {
            Debug.LogError("The number of steps and cycles is too large for the given bounds.");
            return;
        }
        //Create Black Border Line reprenseting the outline boundary
        DrawBorder drawBorder = new();
        drawBorder.DrawBorderLine((minX-1)*tileSizeX, (minY-1)*tileSizeY, (maxX+1)*tileSizeX, (maxY+1)*tileSizeY);
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
                    go.GetComponent<SpriteRenderer>().color = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
                    // Store tile position in the dictionary
                    tilePositions.Add(currentPosition, go);
                }

                // Randomly update current position
                switch (UnityEngine.Random.Range(0,4))
                {
                    case 0:
                        if (currentPosition.x < maxX) // maxX is the upper bound for x
                        {
                            currentPosition.x++;
                        }
                        else if(currentPosition.x >= maxX)
                        {
                            Debug.Log("Max X reached, moving back");
                            currentPosition.x--;
                        }
                        break;
                    case 1:
                        if (currentPosition.x > minX) // minX is the lower bound for x
                        {
                            currentPosition.x--;
                        }
                        else if(currentPosition.x <= minX)
                        {
                            Debug.Log("Min X reached, moving forward");
                            currentPosition.x++;
                        }
                        break;
                    case 2:
                        if (currentPosition.y < maxY) // maxY is the upper bound for y
                        {
                            currentPosition.y++;
                        }
                        else if(currentPosition.y >= maxY)
                        {
                            Debug.Log("Max Y reached, moving down");
                            currentPosition.y--;
                        }
                        break;
                    case 3:
                        if (currentPosition.y > minY) // minY is the lower bound for y
                        {
                            currentPosition.y--;
                        }
                        else if(currentPosition.y <= minY)
                        {
                            Debug.Log("Min Y reached, moving up");
                            currentPosition.y++;
                        }
                        break;
                }
            }
            Debug.Log("Tiles generated: " + tilePositions.Count);
        }
    }

}


