using System.Collections.Generic;
using UnityEngine;

namespace MapGenDLA
{
    public class MapGenDLA : MonoBehaviour
    {
        [SerializeField] GameObject tile;
        // [SerializeField] int cycles = 2;
        // [SerializeField] int steps = 50;
        [SerializeField] int tileNum = 25;
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

            //will be used to clarify which border the next walk will come from
            Vector2Int north = new (0,maxY);
            Vector2Int east  = new (maxX,0);
            Vector2Int south = new (0,minY);
            Vector2Int west  = new (minX,0);
            Vector2Int currentPosition = new (0,0);     //location of current tile in the walk
            Vector2Int previousPosition = new (0,0);    //location of the previous tile in the walk
            int startSide = 0;  //determines which side the walk will start

            // Generate tiles
            int i = 0;  //counter for amount of tiles made

            //first tile: hard coding name and position because it should always start at 0
            GameObject firstTile = Instantiate(tile, new Vector3(0,0,0), Quaternion.identity);
            firstTile.name = "Tile(0,0)";
            firstTile.transform.localScale = new Vector3(tileSizeX, tileSizeY, 1);
            firstTile.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
            tilePositions.Add(currentPosition, firstTile);
            i++;
            Debug.Log("Starting Tile Made!");

            //will keep looping until it generates the amount of tiles wanted
            //NOTE!! will loop infinitely if there are more tiles requested than available on the grid.
            while(i < tileNum){
                startSide = Random.Range(0,4);  //true random: each loop will randomly choose which side to start its walk
                // startSide = (startSide+1)%4; //fair random: starting position will rotate clockwise after every tile made
                switch (startSide){
                    case 0:
                        currentPosition = north;
                        break;
                    case 1:
                        currentPosition = east;
                        break;
                    case 2:
                        currentPosition = south;
                        break;
                    case 3:
                        currentPosition = west;
                        break;
                }

                //if the any of the start sides already has a tile on top of it
                //technically this isnt dla but code breaks if a tile lands on the border of the map
                //algorithm switches to doing a random walk until it doesnt land on a tile
                if(tilePositions.ContainsKey(currentPosition)){
                    while(tilePositions.ContainsKey(currentPosition)){
                        //do random walks to place the next tile close by
                        switch (Random.Range(0,4)){
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
                    previousPosition = currentPosition;
                }

                else{
                    //resetting previousPosition
                    previousPosition = currentPosition;

                    //start a walk until it reaches a tile that already exists
                    while(!tilePositions.ContainsKey(currentPosition)){
                        
                        //previousPosition will have previous loop's tile coords
                        previousPosition = currentPosition;

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
                }

                //Create a new tile where the previous tile was
                GameObject newTile = Instantiate(tile, new Vector3(previousPosition.x * tileSizeX, previousPosition.y * tileSizeY, 0), Quaternion.identity);
                newTile.name = "Tile(" + previousPosition.x + ", " + previousPosition.y + ")";
                newTile.transform.localScale = new Vector3(tileSizeX, tileSizeY, 1);
                newTile.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
                tilePositions.Add(previousPosition, newTile);
                
                //new tile was made so increment counter
                i++;
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