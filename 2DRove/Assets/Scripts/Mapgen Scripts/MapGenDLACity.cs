using System.Collections.Generic;
using UnityEngine;
// using CreateBorder;
using EdgeTilesCity; 
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


namespace MapGenDLACityNamespace
{
    public class MapGenDLACity : MonoBehaviour
    {
        [SerializeField] GameObject beginningTile;
        [SerializeField] GameObject[] tile;
        [SerializeField] GameObject borderPrefab;

        [SerializeField] GameObject exitPrefab;
        // [SerializeField] int cycles = 2;
        // [SerializeField] int steps = 50;
        
        [SerializeField] int tileNum = 25;
        // [SerializeField] int tileSizeX = 20;
        // [SerializeField] int tileSizeY = 20;


        public int leftMost = 0;
        public int rightMost = 0;
        public int topMost = 0;
        public int bottomMost = 0;

        // private DrawBorder drawBorder;
        public AssignEdgesCity assignEdgesCity; 


        public float scale = 6.5f;
        [SerializeField] public int maxX = 5;
        [SerializeField] public int maxY = 5;
        [SerializeField] public int minX = -5;
        [SerializeField] public int minY = -5;
        // Dictionary to store positions of tiles
        // public static Dictionary<Vector2Int, GameObject> tilePositions = new();
        enum Direction { UpRight, DownLeft, UpLeft, DownRight };
        public static HashSet<Vector2> tilePositions = new();
        // We need to make sure we are not creating border tiles on top of each other
        public static HashSet<Vector2> borderPositions = new();
        public static Dictionary<Vector2, GameObject> tileObjects = new();

        void Start()
        {
            //Clear at the start of a scene being loaded
            tilePositions.Clear();
            borderPositions.Clear();
            tileObjects.Clear();
            //checks for SerializeFields
            int possibleTiles = (Mathf.Abs(maxX-minX+1) * Mathf.Abs(maxY-minY+1))/2+1;
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
            
            Vector2 currentPosition = new (0, 0);    // Location of current tile in the walk.
            Vector2 previousPosition = new (0, 0);   // Location of the previous tile in the walk.
            

            // Generate first tile at (0, 0)
            GenerateFirstTile(currentPosition);

            // Used for determining endpoint direction for implementing map generation direction bias
            Vector2 endpoint = GenerateRandomBorderEndpoint();

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
                Vector2 position = tile.Key;
                GameObject tileObject = tile.Value;
                // Our directions considering we are isometric
                Vector2 RightUp = Vector2.right + Vector2.up;
                Vector2 LeftUp = Vector2.left + Vector2.up;
                Vector2 RightDown = Vector2.right + Vector2.down;
                Vector2 LeftDown = Vector2.left + Vector2.down;

                //Check the neighboring tiles
                CheckAndAddBorder(position + RightUp, tileObject);
                CheckAndAddBorder(position + LeftUp, tileObject);
                CheckAndAddBorder(position + RightDown, tileObject);
                CheckAndAddBorder(position + LeftDown, tileObject);
            }

            // //Add an exit
            // Vector2Int farthestTile = FindFarthestTile(new Vector2Int(0, 0));
            // AddExit(tileObjects[farthestTile], farthestTile);
            Vector2 closestTile = FindClosestGeneratedTileToEndpoint(endpoint);
            AddExit(closestTile);



            // //todo: implement FillInEmptySpace to work for isometric
            // // FillInEmptySpace();

            // After all tiles have been generated:
            assignEdgesCity = GetComponent<AssignEdgesCity>();
            if(assignEdgesCity == null) {
                Debug.LogError("AssignEdges component not found on the same GameObject.");
            } else {
                assignEdgesCity.SetTileObjects(tileObjects);
                assignEdgesCity.SetScale(6.5f);
                assignEdgesCity.SetTilePositions(tilePositions); 
                assignEdgesCity.AssignAndSwapEdgeTiles();
            }


        }

        // First tile: hard coding name and position because it should always start at 0.
        private void GenerateFirstTile(Vector2 currentPosition)
        {
            GameObject firstTile = Instantiate(beginningTile, new Vector3(0, 0, 0), Quaternion.identity);
            firstTile.name = "Tile(0,0)";
            //firstTile.layer = LayerMask.NameToLayer("TileLayer");
            // firstTile.transform.localScale = new Vector3(6.5f, 6.5f, 1f);
            tilePositions.Add(new Vector2(0,0));
            tileObjects.Add(new Vector2(0,0), firstTile);

            Debug.Log("Starting Tile Made!");
        }

        // Get a random direction for walk
        private Vector2 RandomDirection()
        {
            Vector2[] borderDirections = new Vector2[]
            {
                new Vector2(0, maxY),    // North
                new Vector2(maxX, 0),    // East
                new Vector2(0, minY),    // South
                new Vector2(minX, 0)     // West
            };

            int startSide = Random.Range(0, borderDirections.Length);  // Randomly select a start side
            return borderDirections[startSide];  // Return the selected border position
        }

        private void FindOpenTile(ref Vector2 currentPosition, ref Vector2 previousPosition, Vector2 targetPosition)
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

        private Vector2 UpdatePosition(int direction, Vector2 position, Vector2 targetPosition)
        {   
            float oldX = position.x;
            float oldY = position.y;
            float distance = 1;
            
            Vector2 biasDirection = GetBiasDirection(position, targetPosition);
            int bias = Random.Range(0, 10); // Introduce a bias probability, e.g., 20% chance to follow the bias direction

            if (bias < 2) // 20% chance to move towards the target
            {
                position.x += biasDirection.x;
                position.y += biasDirection.y;
            }
            else
            {
                /*
                    Mathf.Min returns the smaller of the two numbers
                    Mathf.Max returns the larger of the two numbers
                    Example: Mathf.Min(5, 10) returns 5
                */
            
                switch (direction)
                {
                    case 0: 
                        // return new Vector2Int(Mathf.Clamp(position.x + distance,minX, maxX), Mathf.Clamp(position.y + distance, minY, maxY));//ur
                        position.x += distance;
                        position.y += distance;
                        break;
                    case 1:
                        position.x -= distance;
                        position.y -= distance;                    
                        break;
                        // return new Vector2Int(Mathf.Clamp(position.x - distance, minX, maxX), Mathf.Clamp(position.y - distance, minY, maxY));//dl
                    case 2: 
                        // return new Vector2Int(Mathf.Clamp(position.x - distance, minX, maxX), Mathf.Clamp(position.y + distance, minY, maxY));//ul
                        position.x -= distance;
                        position.y += distance;
                        break;
                    case 3: 
                        //return new Vector2Int(Mathf.Clamp(position.x + distance, minX, maxX), Mathf.Clamp(position.y - distance, minY, maxY));//dr
                        position.x += distance;
                        position.y -= distance;
                        break;
                    default: return position;
                }
            }   

            //if UpdatePosition ends up going out of bounds it is going to return another call of the function and it will keep going
            //until the next update position gives a location that is not out of bounds
            if(position.x > maxX || position.x < minX || position.y > maxY || position.y < minY){
                direction = (direction+1)%4;//it will rotate direction to choose a different area 
                position.x = oldX;//get the values of x and y before they were changed
                position.y = oldY;//to stay in place and call the function again
                return UpdatePosition(direction, position, targetPosition);
            }
            else
                return new Vector2(position.x, position.y);

        }

        private Vector2 GetBiasDirection(Vector2 currentPosition, Vector2 targetPosition)
        {
            // Calculate the direction vector towards the target
            Vector2 direction = targetPosition - currentPosition;
            direction.x = (int)Mathf.Sign(direction.x);
            direction.y = (int)Mathf.Sign(direction.y);
            return direction;
        }

        //Create a new tile where the previous tile was
        private void CreatePreviousTile(Vector2 previousPosition)
        {
            GameObject newTile = Instantiate(getRandomTile(tile), new Vector3(previousPosition.x *  26, previousPosition.y * 13, 0), Quaternion.identity);
            //newTile.layer = LayerMask.NameToLayer("TileLayer");
            newTile.name = "Tile(" + previousPosition.x + ", " + previousPosition.y + ")";
            // newTile.transform.localScale = new Vector3(6.5f, 6.5f, 1f);
            tilePositions.Add(previousPosition);
            tileObjects.Add(previousPosition, newTile);
        }

        private GameObject getRandomTile(GameObject[] tileSet){
            return tileSet[Random.Range(0, tile.Length)];
        }

        void CheckAndAddBorder(Vector2 position, GameObject tileObject)
        {
            // If the neighboring tile is empty (either a basic tile or border), add a border
            if (!tilePositions.Contains(position) && !borderPositions.Contains(position))
            {
                AddBorder(tileObject, position);
            }
        }
        void AddBorder(GameObject tileObject, Vector2 position)
        {
            GameObject border = Instantiate(borderPrefab, new Vector3(position.x * 26, position.y * 13, 0), Quaternion.identity);
            border.name = "Border(" + position.x + ", " + position.y + ")";
            //border.transform.parent = tileObject.transform;
            // border.transform.localScale = new Vector3(6.5f, 6.5f, 1f);
            // Place the tile lower in the layers
            // border.GetComponent<Renderer>().sortingOrder = -1;
            // Apply a tilemap collider to give all the tiles on the tilemap a collider
            // Tilemap tilemap = border.GetComponent<Tilemap>();
            // TilemapCollider2D collider = border.AddComponent<TilemapCollider2D>();
            // Keep it in a list so we do not stack borders on top of each other when checking
            borderPositions.Add(position);
        }

        void AddExit(Vector2 closestTile)
        {
            if (tileObjects.TryGetValue(closestTile, out GameObject tileObject))
            {
                // Use the tileObject's position, as it might have been adjusted for your game world
                Vector3 exitPosition = tileObject.transform.position + new Vector3(0, 0, -1); // Adjust Z if necessary to ensure visibility
                GameObject exit = Instantiate(exitPrefab, exitPosition, Quaternion.identity);
                exit.name = "Exit(" + closestTile.x + ", " + closestTile.y + ")";
                exit.transform.localScale = new Vector3(6.5f, 6.5f, 1f);
                BoxCollider2D collider = exit.AddComponent<BoxCollider2D>();
                collider.isTrigger = true;
            }
            else
            {
                Debug.LogError("Closest tile to endpoint not found in tileObjects.");
            }
        }


        Vector2 FindClosestGeneratedTileToEndpoint(Vector2 endpoint)
        {
            Vector2 closestTile = new Vector2(0, 0);
            float minDistance = float.MaxValue; // Corrected initialization
            foreach (Vector2 tilePosition in tilePositions)
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




        Vector2 FindFarthestTile(Vector2 startPoint)
        {
            Vector2 farthestTile = new Vector2(0, 0);
            float maxDistance = 0;
            foreach (var tile in tilePositions)
            {
                float distance = Vector2.Distance(startPoint, tile);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    farthestTile = tile;
                }
            }
            return farthestTile;
        }

        Vector2 GenerateRandomBorderEndpoint()
        {
            int side = Random.Range(0, 4); // Randomly select one of the four sides
            int randomPosition;

            switch (side)
            {
                case 0: // Top
                    randomPosition = Random.Range(minX, maxX + 1);
                    return new Vector2(randomPosition, maxY);
                case 1: // Bottom
                    randomPosition = Random.Range(minX, maxX + 1);
                    return new Vector2(randomPosition, minY);
                case 2: // Left
                    randomPosition = Random.Range(minY, maxY + 1);
                    return new Vector2(minX, randomPosition);
                case 3: // Right
                    randomPosition = Random.Range(minY, maxY + 1);
                    return new Vector2(maxX, randomPosition);
                default:
                    return new Vector2(0, 0); // This case should never be reached
            }
        }

    }

}
