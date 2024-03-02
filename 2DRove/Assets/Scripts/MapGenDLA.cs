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
            // Check if the number of steps and cycles is too large for the given bounds
            // if(steps*cycles > (maxX+maxY)*(Mathf.Abs(minX)+Mathf.Abs(minY)))
            // {
            //     Debug.LogError("The number of steps and cycles is too large for the given bounds.");
            //     return;
            // }
            
            // ONLY TO BE USED FOR TESTING PURPOSES, BORDER IS DRAWN BY EMPTY SPACE METHOD
            // Create Black Border Line reprenseting the outline boundary
            // DrawBorder drawBorder = new();
            // drawBorder.DrawBorderLine((minX-1)*tileSizeX, (minY-1)*tileSizeY, (maxX+1)*tileSizeX, (maxY+1)*tileSizeY);

            Vector2Int currentPosition = new (0, 0);    // Location of current tile in the walk.
            Vector2Int previousPosition = new (0, 0);   // Location of the previous tile in the walk.
            

            // Generate first tile at (0, 0)
            GenerateFirstTile(currentPosition);

            // Will keep looping until it generates the amount of tiles wanted.
            // NOTE!! will loop infinitely if there are more tiles requested than available on the grid.
            int i = 1;  // Tile counter 
            while(i < tileNum){

                // Get a random direction for walk
                currentPosition = RandomDirection();

                // Find open, non-border tile 
                FindOpenTile(ref currentPosition, ref previousPosition);

                //Create a new tile where the previous tile was
                CreatePreviousTile(previousPosition);
                i++;
            }
            FillInEmptySpace();
        }

        // First tile: hard coding name and position because it should always start at 0.
        private void GenerateFirstTile(Vector2Int currentPosition)
        {               
            GameObject firstTile = Instantiate(tile, new Vector3(0, 0, 0), Quaternion.identity);
            firstTile.name = "Tile(0,0)";
            firstTile.transform.localScale = new Vector3(tileSizeX, tileSizeY, 1);
            firstTile.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
            tilePositions.Add(currentPosition, firstTile);
            Debug.Log("Starting Tile Made!");
        }

        // Get a random direction for walk
        private Vector2Int RandomDirection()
        {
            Vector2Int[] borderDirections = new Vector2Int[]
            {
                new Vector2Int(0, maxY),    // North
                new Vector2Int(maxX, 0),    // East
                new Vector2Int(0, minY),    // South
                new Vector2Int(minX, 0)     // West
            };

            int startSide = Random.Range(0, borderDirections.Length);  // Randomly select a start side
            return borderDirections[startSide];  // Return the selected border position
        }

        private void FindOpenTile(ref Vector2Int currentPosition, ref Vector2Int previousPosition)
        {
            // if the any of the start sides already has a tile on top of it
            // technically this isnt DLA but code breaks if a tile lands on the border of the map
            // algorithm switches to doing a random walk until it doesnt land on a tile
            if(tilePositions.ContainsKey(currentPosition)){
                while(tilePositions.ContainsKey(currentPosition)){
                    //do random walks to place the next tile close by
                    int direction = Random.Range(0, 4);
                    currentPosition = UpdatePosition(direction, currentPosition);
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
                    int direction = Random.Range(0, 4);
                    currentPosition = UpdatePosition(direction, currentPosition);
                }
            }
        }

        private Vector2Int UpdatePosition(int direction, Vector2Int position)
        {
            /*
                Mathf.Min returns the smaller of the two numbers
                Mathf.Max returns the larger of the two numbers
                Example: Mathf.Min(5, 10) returns 5
            */
            switch (direction)
            {
                case 0: return new Vector2Int(Mathf.Min(position.x + 1, maxX), position.y);
                case 1: return new Vector2Int(Mathf.Max(position.x - 1, minX), position.y);
                case 2: return new Vector2Int(position.x, Mathf.Min(position.y + 1, maxY));
                case 3: return new Vector2Int(position.x, Mathf.Max(position.y - 1, minY));
                default: return position;
            }
        }

        //Create a new tile where the previous tile was
        private void CreatePreviousTile(Vector2Int previousPosition)
        {
            GameObject newTile = Instantiate(tile, new Vector3(previousPosition.x * tileSizeX, previousPosition.y * tileSizeY, 0), Quaternion.identity);
            newTile.name = "Tile(" + previousPosition.x + ", " + previousPosition.y + ")";
            newTile.transform.localScale = new Vector3(tileSizeX, tileSizeY, 1);
            newTile.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
            tilePositions.Add(previousPosition, newTile);
        }

        // Fills in the empty space created between tiles and the border similarly to tile generation. 
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
