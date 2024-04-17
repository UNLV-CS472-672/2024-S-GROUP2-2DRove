using System;
using System.Collections.Generic;
using UnityEngine;   
using Random=UnityEngine.Random;   
      
namespace EdgeTilesCity
{
    public class AssignEdgesCity : MonoBehaviour
    {

        enum TileType { None, NPeninsula, SPeninsula, WPeninsula, EPeninsula, NECorner, NWCorner, SECorner, SWCorner }

        [SerializeField] private GameObject[] humpEastPrefab;
        [SerializeField] private GameObject[] humpNorthPrefab;
        [SerializeField] private GameObject[] humpSouthPrefab;
        [SerializeField] private GameObject[] humpWestPrefab;
        [SerializeField] private GameObject[] NEcornerPrefab;
        [SerializeField] private GameObject[] NWcornerPrefab;
        [SerializeField] private GameObject[] SEcornerPrefab;
        [SerializeField] private GameObject[] SWcornerPrefab;

        private Dictionary<Vector2, GameObject> tileObjects;
        private float scale;
        private HashSet<Vector2> tilePositions;

        public void SetTilePositions(HashSet<Vector2> positions)
        {
            tilePositions = new HashSet<Vector2>(positions); // Create a copy to avoid unintended modifications
        }

        public void SetScale(float newScale)
        {
            scale = newScale;
        }

        public void SetTileObjects(Dictionary<Vector2, GameObject> objects)
        {
            tileObjects = objects;
        }

        // This should be called after your map generation logic is complete
        public void AssignAndSwapEdgeTiles() {
            foreach (var tilePos in tilePositions) {
                TileType tileType = GetTileType(tilePos);
                switch (tileType) {
                    case TileType.NECorner:
                        SwapTile(tilePos, getRandomTile(NEcornerPrefab)); // Replace with North East Corner tile prefab
                        break;
                    case TileType.NWCorner:
                        SwapTile(tilePos, getRandomTile(NWcornerPrefab)); // Replace with North West Corner tile prefab
                        break;
                    case TileType.SECorner:
                        SwapTile(tilePos, getRandomTile(SEcornerPrefab)); // Replace with South East Corner tile prefab
                        break;
                    case TileType.SWCorner:
                        SwapTile(tilePos, getRandomTile(SWcornerPrefab)); // Replace with South West Corner tile prefab
                        break;
                    case TileType.NPeninsula:
                        SwapTile(tilePos, getRandomTile(humpNorthPrefab)); // Replace with North Peninsula tile prefab
                        break;
                    case TileType.SPeninsula:
                        SwapTile(tilePos, getRandomTile(humpSouthPrefab)); // Replace with South Peninsula tile prefab
                        break;
                    case TileType.WPeninsula:
                        SwapTile(tilePos, getRandomTile(humpWestPrefab)); // Replace with West Peninsula tile prefab
                        break;
                    case TileType.EPeninsula:
                        SwapTile(tilePos, getRandomTile(humpEastPrefab)); // Replace with East Peninsula tile prefab
                        break;
                    case TileType.None:
                    default:
                        // No need to swap if it's a regular tile or the type is None
                        break;
                }
            }
        }

        private GameObject getRandomTile(GameObject[] tileSet){
            return tileSet[Random.Range(0, tileSet.Length)];
        }

        // Check the type of tile based on its neighbors.
        TileType GetTileType(Vector2 tilePos)
        {
            Vector2 isoUp = new Vector2(-1, 1);
            Vector2 isoDown = new Vector2(1, -1);
            Vector2 isoLeft = new Vector2(-1, -1);
            Vector2 isoRight = new Vector2(1, 1);
            
            // Adjust for the scale of your grid if necessary
            // isoUp *= scale / 2;
            // isoDown *= scale / 2;
            // isoLeft *= scale / 2;
            // isoRight *= scale / 2;

            bool up = tilePositions.Contains(tilePos + isoUp);
            bool down = tilePositions.Contains(tilePos + isoDown);
            bool left = tilePositions.Contains(tilePos + isoLeft);
            bool right = tilePositions.Contains(tilePos + isoRight);

            Debug.Log("Checking tile at position: " + tilePos);
            Debug.Log("Up: " + (up ? "Yes" : "No"));
            Debug.Log("Down: " + (down ? "Yes" : "No"));
            Debug.Log("Left: " + (left ? "Yes" : "No"));
            Debug.Log("Right: " + (right ? "Yes" : "No"));

            TileType type = TileType.None;
            bool flag = true; 

            // Check peninsulas
            if (!up && !left && !right && down) {
                type = TileType.NPeninsula;
                flag = false;
            } 
            if (up && !down && !left && !right) {
                type = TileType.SPeninsula;
                flag = false;
            } 
            if (!up && !down && !left && right) {
                type = TileType.WPeninsula;
                flag = false;
            }
            if (!up && !down && left && !right) {
                type = TileType.EPeninsula;
                flag = false;
            }
            if(flag) {
                // Check corners
                if (!up && !right) type = TileType.NECorner;
                if (!up && !left) type = TileType.NWCorner;
                if (!down && !right) type = TileType.SECorner;
                if (!down && !left) type = TileType.SWCorner;
            }


            //  Log the tile position and type for debugging
            Debug.Log("Tile at position " + tilePos + " is of type: " + type);

            // Label the GameObject for visualization in the editor
            if (tileObjects.TryGetValue(tilePos, out GameObject tileObject)) {
                tileObject.name = "Tile(" + tilePos.x + ", " + tilePos.y + ") - " + type;
            }

            return type;

        }

        // Swap the tile at the given position with a new tile based on type
        public void SwapTile(Vector2 position, GameObject newTilePrefab) {
            if (position == new Vector2(0,0))
                return;

            if (tileObjects.TryGetValue(position, out GameObject oldTile)) {
                Debug.Log("HERE: " + position);
                // Destroy or disable the old tile
                Destroy(oldTile);

                // Instantiate the new tile prefab and place it at the correct position
                GameObject newTile = Instantiate(newTilePrefab, new Vector3(position.x * 26, position.y *13, 0), Quaternion.identity);
                newTile.name = newTilePrefab.name + "(" + position.x + ", " + position.y + ")";
                // newTile.transform.localScale = new Vector3(scale, scale, 1);

                // Update the tileObjects dictionary
                tileObjects[position] = newTile;
            } else {
                Debug.LogError("Tile at position " + position + " not found in tileObjects.");
            }
        }
    }
}