using System.Collections.Generic;
using UnityEngine;
using CreateBorder;

namespace MapGenDLANamespace
{
    public class MapGenDLA : MonoBehaviour
    {
        [SerializeField] GameObject tile;
        // [SerializeField] int cycles = 2;
        // [SerializeField] int steps = 50;
        
        [SerializeField] int tileNum = 25;
        // [SerializeField] int tileSizeX = 20;
        // [SerializeField] int tileSizeY = 20;

        public int leftMost = 0;
        public int rightMost = 0;
        public int topMost = 0;
        public int bottomMost = 0;

        private DrawBorder drawBorder;


        [SerializeField] public int scale = 20;
        [SerializeField] public int maxX = 5;
        [SerializeField] public int maxY = 5;
        [SerializeField] public int minX = -5;
        [SerializeField] public int minY = -5;
        // Dictionary to store positions of tiles
        // public static Dictionary<Vector2Int, GameObject> tilePositions = new();
        enum Direction { UpRight, DownLeft, UpLeft, DownRight };
        public static HashSet<Vector2Int> tilePositions = new();

        void Start()
        {
            //checks for SerializeFields
            int possibleTiles = Mathf.Abs(maxX-minX+1) * Mathf.Abs(maxY-minY+1);
            if(possibleTiles < tileNum){
                Debug.LogError("Cannot request more tiles than available");
                return;
            }
            if(tileNum <= 0){
                Debug.LogError("must create 1 or more tiles");
                return;
            }
            if(scale <= 0){
                Debug.LogError("Scale must be greater than 0");
            }
            
            Vector2Int currentPosition = new (0, 0);    // Location of current tile in the walk.
            Vector2Int previousPosition = new (0, 0);   // Location of the previous tile in the walk.
            

            // Generate first tile at (0, 0)
            GenerateFirstTile(currentPosition);

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
            Debug.Log(leftMost);
            Debug.Log(bottomMost);
            Debug.Log(rightMost);
            Debug.Log(topMost);
            drawBorder = GetComponent<DrawBorder>();
            drawBorder.DrawBorderLine(leftMost * 35, bottomMost * 35, rightMost * 35, topMost * 35);


            //todo: implement FillInEmptySpace to work for isometric
            // FillInEmptySpace();

        }

        // First tile: hard coding name and position because it should always start at 0.
        private void GenerateFirstTile(Vector2Int currentPosition)
        {
            GameObject firstTile = Instantiate(tile, new Vector3(0, 0, 0), Quaternion.identity);
            firstTile.name = "Tile(0,0)";
            firstTile.transform.localScale = new Vector3(scale, scale, 1);
            tilePositions.Add(new Vector2Int(0,0));

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
            if(tilePositions.Contains(currentPosition)){
                while(tilePositions.Contains(currentPosition)){

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
                while(!tilePositions.Contains(currentPosition)){

                    
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
                case 0: return new Vector2Int(Mathf.Clamp(position.x + 1,minX, maxX), Mathf.Clamp(position.y+1, minY, maxY));//ur
                case 1: return new Vector2Int(Mathf.Clamp(position.x - 1, minX, maxX), Mathf.Clamp(position.y-1, minY, maxY));//dl
                case 2: return new Vector2Int(Mathf.Clamp(position.x-1, minX, maxX), Mathf.Clamp(position.y + 1, minY, maxY));//ul
                case 3: return new Vector2Int(Mathf.Clamp(position.x+1, minX, maxX), Mathf.Clamp(position.y - 1, minY, maxY));//dr

                default: return position;
            }
        }

        //Create a new tile where the previous tile was
        private void CreatePreviousTile(Vector2Int previousPosition)
        {
            GameObject newTile = Instantiate(tile, new Vector3(previousPosition.x * scale * 10, previousPosition.y * scale * 5, 0), Quaternion.identity);
            newTile.name = "Tile(" + previousPosition.x + ", " + previousPosition.y + ")";
            newTile.transform.localScale = new Vector3(scale, scale, 1);
            tilePositions.Add(previousPosition);

            leftMost = Mathf.Min(leftMost, previousPosition.x);
            rightMost = Mathf.Max(rightMost, previousPosition.x);
            topMost = Mathf.Max(topMost, previousPosition.y);
            bottomMost = Mathf.Min(bottomMost, previousPosition.y);

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
                    if (!tilePositions.Contains(new Vector2Int(i, j)))
                    {
                        GameObject emptyTiles = Instantiate(tile, new Vector3(i * scale*10, j * scale*5, 0), Quaternion.identity);
                        emptyTiles.name = "EmptyTile(" + i + ", " + j + ")";
                        emptyTiles.transform.localScale = new Vector3(scale, scale, 1);
                        emptyTiles.GetComponent<SpriteRenderer>().color = Color.black;
                        emptyTiles.AddComponent<BoxCollider2D>();
                        tilePositions.Add(new Vector2Int(i, j));
                    }
                }
            Debug.Log("Empty Space generated: " + tilePositions.Count);
        }
    }

}
