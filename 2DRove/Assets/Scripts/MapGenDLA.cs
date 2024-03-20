using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MapGenDLA
{
    public class MapGenDLA : MonoBehaviour
    {
        [SerializeField] GameObject tile;
        [SerializeField] GameObject borderPrefab;
        // [SerializeField] int cycles = 2;
        // [SerializeField] int steps = 50;
        
        [SerializeField] int tileNum = 25;
        // [SerializeField] int tileSizeX = 20;
        // [SerializeField] int tileSizeY = 20;

        [SerializeField] int scale = 20;
        [SerializeField] int maxX = 5;
        [SerializeField] int maxY = 5;
        [SerializeField] int minX = -5;
        [SerializeField] int minY = -5;
        // Dictionary to store positions of tiles
        // public static Dictionary<Vector2Int, GameObject> tilePositions = new();
        enum Direction { UpRight, DownLeft, UpLeft, DownRight };
        public static HashSet<Vector2Int> tilePositions = new();
        // We need to make sure we are not creating border tiles on top of each other
        public static HashSet<Vector2Int> borderPositions = new();
        public static Dictionary<Vector2Int, GameObject> tileObjects = new();

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

            foreach (var tile in tileObjects)
            {
                Vector2Int position = tile.Key;
                GameObject tileObject = tile.Value;
                // Our directions considering we are isometric
                Vector2Int RightUp = Vector2Int.right + Vector2Int.up;
                Vector2Int LeftUp = Vector2Int.left + Vector2Int.up;
                Vector2Int RightDown = Vector2Int.right + Vector2Int.down;
                Vector2Int LeftDown = Vector2Int.left + Vector2Int.down;

                //Check the neighboring tiles
                CheckAndAddBorder(position + RightUp, tileObject);
                CheckAndAddBorder(position + LeftUp, tileObject);
                CheckAndAddBorder(position + RightDown, tileObject);
                CheckAndAddBorder(position + LeftDown, tileObject);
            }

            

            //todo: implement FillInEmptySpace to work for isometric
            // FillInEmptySpace();

        }

        // First tile: hard coding name and position because it should always start at 0.
        private void GenerateFirstTile(Vector2Int currentPosition)
        {
            GameObject firstTile = Instantiate(tile, new Vector3(0, 0, 0), Quaternion.identity);
            firstTile.name = "Tile(0,0)";
            //firstTile.layer = LayerMask.NameToLayer("TileLayer");
            firstTile.transform.localScale = new Vector3(scale, scale, 1);
            tilePositions.Add(new Vector2Int(0,0));
            tileObjects.Add(new Vector2Int(0,0), firstTile);

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
            int distance = 1;
            switch (direction)
            {
                case 0: return new Vector2Int(Mathf.Clamp(position.x + distance,minX, maxX), Mathf.Clamp(position.y + distance, minY, maxY));//ur
                case 1: return new Vector2Int(Mathf.Clamp(position.x - distance, minX, maxX), Mathf.Clamp(position.y - distance, minY, maxY));//dl
                case 2: return new Vector2Int(Mathf.Clamp(position.x - distance, minX, maxX), Mathf.Clamp(position.y + distance, minY, maxY));//ul
                case 3: return new Vector2Int(Mathf.Clamp(position.x + distance, minX, maxX), Mathf.Clamp(position.y - distance, minY, maxY));//dr

                default: return position;
            }
        }

        //Create a new tile where the previous tile was
        private void CreatePreviousTile(Vector2Int previousPosition)
        {
            GameObject newTile = Instantiate(tile, new Vector3(previousPosition.x * scale * 10, previousPosition.y * scale * 5, 0), Quaternion.identity);
            //newTile.layer = LayerMask.NameToLayer("TileLayer");
            newTile.name = "Tile(" + previousPosition.x + ", " + previousPosition.y + ")";
            newTile.transform.localScale = new Vector3(scale, scale, 1);
            tilePositions.Add(previousPosition);
            tileObjects.Add(previousPosition, newTile);

        }


        // Deprecated for now.
        // Fills in the empty space created between tiles and the border similarly to tile generation. 
        // void FillInEmptySpace()
        // {
        //     // Generate empty space
        //     for (int i = minX-1; i <= maxX+1; i++)
        //         for (int j = minY-1; j <= maxY+1; j++)
        //         {
        //             Debug.Log("Generating empty space for: " + i + " " + j);
        //             // Check if the position is already occupied
        //             if (!tilePositions.Contains(new Vector2Int(i, j)))
        //             {
        //                 GameObject emptyTiles = Instantiate(tile, new Vector3(i * scale*10, j * scale*5, 0), Quaternion.identity);
        //                 emptyTiles.name = "EmptyTile(" + i + ", " + j + ")";
        //                 emptyTiles.transform.localScale = new Vector3(scale, scale, 1);
        //                 emptyTiles.GetComponent<SpriteRenderer>().color = Color.black;
        //                 emptyTiles.AddComponent<BoxCollider2D>();
        //                 tilePositions.Add(new Vector2Int(i, j));
        //             }
        //         }
        //     Debug.Log("Empty Space generated: " + tilePositions.Count);
        // }

        

        void CheckAndAddBorder(Vector2Int position, GameObject tileObject)
        {
            // If the neighboring tile is empty (either a basic tile or border), add a border
            if (!tilePositions.Contains(position) && !borderPositions.Contains(position))
            {
                AddBorder(tileObject, position);
            }
        }
        void AddBorder(GameObject tileObject, Vector2Int position)
        {
            GameObject border = Instantiate(borderPrefab, new Vector3(position.x * scale * 10, position.y * scale * 5, 0), Quaternion.identity);
            border.name = "Border(" + position.x + ", " + position.y + ")";
            border.transform.parent = tileObject.transform;
            border.transform.localScale = new Vector3(scale / 2, scale / 2, 1);
            // Place the tile lower in the layers
            border.GetComponent<Renderer>().sortingOrder = -1;
            // Apply a tilemap collider to give all the tiles on the tilemap a collider
            Tilemap tilemap = border.GetComponent<Tilemap>();
            TilemapCollider2D collider = border.AddComponent<TilemapCollider2D>();
            // Keep it in a list so we do not stack borders on top of each other when checking
            borderPositions.Add(position);
        }
    }

}
