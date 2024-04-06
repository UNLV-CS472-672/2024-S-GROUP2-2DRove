using System.Collections.Generic;
using UnityEngine;
// using CreateBorder;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

namespace MapGenDLANamespace
{
    public class MapGenDLA : MonoBehaviour
    {
        [SerializeField] GameObject tile;
        [SerializeField] GameObject borderPrefab;

        [SerializeField] GameObject exitPrefab;
        // [SerializeField] int cycles = 2;
        // [SerializeField] int steps = 50;
        
        [SerializeField] int tileNum = 25;
        // [SerializeField] int tileSizeX = 20;
        // [SerializeField] int tileSizeY = 20;

        [SerializeField] public int scale = 20;
        [SerializeField] public int maxX = 5;
        [SerializeField] public int maxY = 5;
        [SerializeField] public int minX = -5;
        [SerializeField] public int minY = -5;
        // Dictionary to store positions of tiles
        // public static Dictionary<Vector2Int, GameObject> tilePositions = new();
        enum Direction { UpRight, DownLeft, UpLeft, DownRight };
        public static HashSet<Vector2Int> tilePositions = new();
        // We need to make sure we are not creating border tiles on top of each other
        public static HashSet<Vector2Int> borderPositions = new();
        public static Dictionary<Vector2Int, GameObject> tileObjects = new();

        void Start()
        {
            //Clear at the start of a scene being loaded
            tilePositions.Clear();
            borderPositions.Clear();
            tileObjects.Clear();
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

            // Used for determining endpoint direction for implementing map generation direction bias
            Vector2Int endpoint = GenerateRandomBorderEndpoint();

            // Generate Endpoint tile 
            //CreatePreviousTile(endpoint);

            int i = 1;  // Tile counter 
            while(i < tileNum){

                // Get a random direction for walk
                currentPosition = RandomDirection();

                // Find open, non-border tile 
                FindOpenTile(ref currentPosition, ref previousPosition, endpoint);

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

            // //Add an exit
            // Vector2Int farthestTile = FindFarthestTile(new Vector2Int(0, 0));
            // AddExit(tileObjects[farthestTile], farthestTile);
            Vector2Int closestTile = FindClosestGeneratedTileToEndpoint(endpoint);
            AddExit(closestTile);



            // //todo: implement FillInEmptySpace to work for isometric
            // // FillInEmptySpace();

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

        private void FindOpenTile(ref Vector2Int currentPosition, ref Vector2Int previousPosition, Vector2Int targetPosition)
        {
            // if the any of the start sides already has a tile on top of it
            // technically this isnt DLA but code breaks if a tile lands on the border of the map
            // algorithm switches to doing a random walk until it doesnt land on a tile
            if(tilePositions.Contains(currentPosition)){
                while(tilePositions.Contains(currentPosition)){

                    //do random walks to place the next tile close by
                    int direction = Random.Range(0, 4);
                    currentPosition = UpdatePosition(direction, currentPosition, targetPosition);
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
                    currentPosition = UpdatePosition(direction, currentPosition, targetPosition);
                }
            }
        }

        private Vector2Int UpdatePosition(int direction, Vector2Int position, Vector2Int targetPosition)
        {
            Vector2Int biasDirection = GetBiasDirection(position, targetPosition);
            int bias = Random.Range(0, 10); // Introduce a bias probability, e.g., 20% chance to follow the bias direction

            if (bias < 2) // 20% chance to move towards the target
            {
                return new Vector2Int(
                    Mathf.Clamp(position.x + biasDirection.x, minX, maxX),
                    Mathf.Clamp(position.y + biasDirection.y, minY, maxY)
                );
            }
            else
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

        }

        private Vector2Int GetBiasDirection(Vector2Int currentPosition, Vector2Int targetPosition)
        {
            // Calculate the direction vector towards the target
            Vector2Int direction = targetPosition - currentPosition;
            direction.x = (int)Mathf.Sign(direction.x);
            direction.y = (int)Mathf.Sign(direction.y);
            return direction;
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
            GameObject border = Instantiate(borderPrefab, new Vector3(position.x * scale * maxX, position.y * scale * (maxY / 2), 0), Quaternion.identity);
            border.name = "Border(" + position.x + ", " + position.y + ")";
            border.transform.parent = tileObject.transform;
            border.transform.localScale = new Vector3(scale / 2, scale / 2, 1);
            // Place the tile lower in the layers
            border.GetComponent<Renderer>().sortingOrder = -1;
            // Apply a tilemap collider to give all the tiles on the tilemap a collider
            //Tilemap tilemap = border.GetComponent<Tilemap>();
            //TilemapCollider2D collider = border.AddComponent<TilemapCollider2D>();
            // Keep it in a list so we do not stack borders on top of each other when checking
            borderPositions.Add(position);
        }

        void AddExit(Vector2Int closestTile)
        {
            if (tileObjects.TryGetValue(closestTile, out GameObject tileObject))
            {
                // Use the tileObject's position, as it might have been adjusted for your game world
                Vector3 exitPosition = tileObject.transform.position + new Vector3(0, 0, -1); // Adjust Z if necessary to ensure visibility
                GameObject exit = Instantiate(exitPrefab, exitPosition, Quaternion.identity);
                exit.name = "Exit(" + closestTile.x + ", " + closestTile.y + ")";
                exit.transform.localScale = new Vector3(scale, scale, 1);
                BoxCollider2D collider = exit.AddComponent<BoxCollider2D>();
                collider.isTrigger = true;
            }
            else
            {
                Debug.LogError("Closest tile to endpoint not found in tileObjects.");
            }
        }


        Vector2Int FindClosestGeneratedTileToEndpoint(Vector2Int endpoint)
        {
            Vector2Int closestTile = new Vector2Int(0, 0);
            float minDistance = float.MaxValue; // Corrected initialization
            foreach (Vector2Int tilePosition in tilePositions)
            {
                float distance = Vector2.Distance(tilePosition, endpoint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTile = tilePosition;
                }
            }
            return closestTile;
        }




        Vector2Int FindFarthestTile(Vector2Int startPoint)
        {
            Vector2Int farthestTile = new Vector2Int(0, 0);
            float maxDistance = 0;
            foreach (var tile in tilePositions)
            {
                float distance = Vector2Int.Distance(startPoint, tile);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    farthestTile = tile;
                }
            }
            return farthestTile;
        }

        Vector2Int GenerateRandomBorderEndpoint()
        {
            int side = Random.Range(0, 4); // Randomly select one of the four sides
            int randomPosition;

            switch (side)
            {
                case 0: // Top
                    randomPosition = Random.Range(minX, maxX + 1);
                    return new Vector2Int(randomPosition, maxY);
                case 1: // Bottom
                    randomPosition = Random.Range(minX, maxX + 1);
                    return new Vector2Int(randomPosition, minY);
                case 2: // Left
                    randomPosition = Random.Range(minY, maxY + 1);
                    return new Vector2Int(minX, randomPosition);
                case 3: // Right
                    randomPosition = Random.Range(minY, maxY + 1);
                    return new Vector2Int(maxX, randomPosition);
                default:
                    return new Vector2Int(0, 0); // This case should never be reached
            }
        }

    }

}
