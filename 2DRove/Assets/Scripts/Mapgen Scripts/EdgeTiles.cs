using System;
using System.Collections.Generic;
using UnityEngine;      
      
namespace EdgeTiles
{
    public class AssignEdges : MonoBehaviour
    {

        enum TileType { 
            None, NPeninsula, SPeninsula, WPeninsula, EPeninsula, NECorner, NWCorner, SECorner, SWCorner, // Corner Edges
            NSBridge, WEBridge, Nside, Sside, Wside, Eside,  // Side Edges
            CornerNW, CornerNE, CornerSW, CornerSE, // Bordering corners 
        }

        // Corner Edges
        [SerializeField] private GameObject humpEastPrefab;
        [SerializeField] private GameObject humpNorthPrefab;
        [SerializeField] private GameObject humpSouthPrefab;
        [SerializeField] private GameObject humpWestPrefab;
        [SerializeField] private GameObject NEcornerPrefab;
        [SerializeField] private GameObject NWcornerPrefab;
        [SerializeField] private GameObject SEcornerPrefab;
        [SerializeField] private GameObject SWcornerPrefab;
        
        // Side Edges
        [SerializeField] private GameObject NSBridgePrefab;
        [SerializeField] private GameObject WEBridgePrefab;
        [SerializeField] private GameObject NsidePrefab;
        [SerializeField] private GameObject SsidePrefab;
        [SerializeField] private GameObject WsidePrefab;
        [SerializeField] private GameObject EsidePrefab;

        // Bordering corners
        [SerializeField] private GameObject CornerNWPrefab;
        [SerializeField] private GameObject CornerNEPrefab;
        [SerializeField] private GameObject CornerSWPrefab;
        [SerializeField] private GameObject CornerSEPrefab;

        private Dictionary<Vector2Int, GameObject> tileObjects;
        private int scale;
        private HashSet<Vector2Int> tilePositions;
        private HashSet<Vector2Int> borderPositions;

        public void SetTilePositions(HashSet<Vector2Int> positions)
        {
            tilePositions = new HashSet<Vector2Int>(positions); // Create a copy to avoid unintended modifications
        }

        public void SetScale(int newScale)
        {
            scale = newScale;
        }

        public void SetTileObjects(Dictionary<Vector2Int, GameObject> objects)
        {
            tileObjects = objects;
        }

        public void SetBorderPositions(HashSet<Vector2Int> positions)
        {
            borderPositions = new HashSet<Vector2Int>(positions); // Create a copy to avoid unintended modifications
        }

        // This should be called after your map generation logic is complete
        public void AssignAndSwapEdgeTiles() {
            foreach (var tilePos in tilePositions) {
                TileType tileType = GetTileType(tilePos);
                switch (tileType) {
                    case TileType.NECorner:
                        SwapTile(tilePos, NEcornerPrefab); // Replace with North East Corner tile prefab
                        break;
                    case TileType.NWCorner:
                        SwapTile(tilePos, NWcornerPrefab); // Replace with North West Corner tile prefab
                        break;
                    case TileType.SECorner:
                        SwapTile(tilePos, SEcornerPrefab); // Replace with South East Corner tile prefab
                        break;
                    case TileType.SWCorner:
                        SwapTile(tilePos, SWcornerPrefab); // Replace with South West Corner tile prefab
                        break;
                    case TileType.NPeninsula:
                        SwapTile(tilePos, humpNorthPrefab); // Replace with North Peninsula tile prefab
                        break;
                    case TileType.SPeninsula:
                        SwapTile(tilePos, humpSouthPrefab); // Replace with South Peninsula tile prefab
                        break;
                    case TileType.WPeninsula:
                        SwapTile(tilePos, humpWestPrefab); // Replace with West Peninsula tile prefab
                        break;
                    case TileType.EPeninsula:
                        SwapTile(tilePos, humpEastPrefab); // Replace with East Peninsula tile prefab
                        break;
                    
                    case TileType.NSBridge:
                        SwapTile(tilePos, NSBridgePrefab); // Replace with East Peninsula tile prefab
                        break;
                    case TileType.WEBridge:
                        SwapTile(tilePos, WEBridgePrefab); // Replace with East Peninsula tile prefab
                        break;
                    case TileType.Nside:
                        SwapTile(tilePos, NsidePrefab); // Replace with East Peninsula tile prefab
                        break;
                    case TileType.Sside:
                        SwapTile(tilePos, SsidePrefab); // Replace with East Peninsula tile prefab
                        break;
                    case TileType.Wside:
                        SwapTile(tilePos, WsidePrefab); // Replace with East Peninsula tile prefab
                        break;
                    case TileType.Eside:
                        SwapTile(tilePos, EsidePrefab); // Replace with East Peninsula tile prefab
                        break;
                    case TileType.None:
                    default:
                        // No need to swap if it's a regular tile or the type is None
                        break;
                }
            }

            BorderingCorners();

        }


        // Check the type of tile based on its neighbors.
        TileType GetTileType(Vector2Int tilePos)
        {
            Vector2Int isoUp = new Vector2Int(-1, 1);
            Vector2Int isoDown = new Vector2Int(1, -1);
            Vector2Int isoLeft = new Vector2Int(-1, -1);
            Vector2Int isoRight = new Vector2Int(1, 1);
            
            // Adjust for the scale of your grid if necessary
            isoUp *= scale / 2;
            isoDown *= scale / 2;
            isoLeft *= scale / 2;
            isoRight *= scale / 2;

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
                bool flag2 = true;

                // Check corners
                if (!up && !right) {
                    type = TileType.NECorner;
                    flag2 = false;
                }
                if (!up && !left) {
                    type = TileType.NWCorner;
                    flag2 = false;
                } 
                if (!down && !right) {
                    type = TileType.SECorner;
                    flag2 = false;
                }
                if (!down && !left) {
                    type = TileType.SWCorner;
                    flag2 = false;
                }

                
                if (flag2) {
                    bool flag3 = true; 

                    // Check bridges
                    if (!left && !right) {
                        type = TileType.NSBridge;
                        flag3 = false;
                    }
                    if (!up && !down) {
                        type = TileType.WEBridge;
                        flag3 = false;
                    }

                    if (flag3) {
                        // Check sides
                        if (!up) type = TileType.Nside;
                        if (!down) type = TileType.Sside;
                        if (!left) type = TileType.Wside;
                        if (!right) type = TileType.Eside;
                    }
                }

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
        void SwapTile(Vector2Int position, GameObject newTilePrefab) {
            if (tileObjects.TryGetValue(position, out GameObject oldTile)) {
                Debug.Log("HERE: " + position);
                // Destroy or disable the old tile
                Destroy(oldTile);

                // Instantiate the new tile prefab and place it at the correct position
                GameObject newTile = Instantiate(newTilePrefab, new Vector3(position.x * scale * 10, position.y * scale * 5, 0), Quaternion.identity);
                newTile.name = newTilePrefab.name + "(" + position.x + ", " + position.y + ")";
                newTile.transform.localScale = new Vector3(scale, scale, 1);

                // Update the tileObjects dictionary
                tileObjects[position] = newTile;
            } else {
                Debug.LogError("Tile at position " + position + " not found in tileObjects.");
            }
        }

        public void BorderingCorners() {
            foreach (var tilePos in tilePositions) {
                TileType tileType = GetTileType(tilePos);

                // Only check for peninsulas to see if they create an L shape
                if (tileType == TileType.WPeninsula || tileType == TileType.NPeninsula ||
                    tileType == TileType.EPeninsula || tileType == TileType.SPeninsula ||
                    tileType == TileType.NECorner || tileType == TileType.NWCorner ||
                    tileType == TileType.SECorner || tileType == TileType.SWCorner 
                ) {
                    // Find the potential L-shape position based on the peninsula type
                    Vector2Int lShapePosition = Vector2Int.zero;
                    GameObject cornerPrefab = null;
                    bool enter = true;

                    if ((tileType == TileType.WPeninsula || tileType == TileType.NWCorner) && enter) {
                        lShapePosition = tilePos + new Vector2Int(0, 2); // Position to the northeast
                        cornerPrefab = CornerNWPrefab;
                        Debug.Log("W-Peninsula: " + tilePos);
                        
                        // Check if the L-shape is created by checking the neighboring positions that form an L
                        if (lShapePosition != Vector2Int.zero && !borderPositions.Contains(lShapePosition)) {
                            Vector2Int basePosition = tilePos + new Vector2Int(1, 1);
                            Debug.Log("L-shape at position: " + lShapePosition);
                            Debug.Log("Overlay at position: " + basePosition);
                            GameObject basePrefab = tileObjects[basePosition];
                            enter = false;
                            OverlayPrefabs(basePosition, basePrefab, cornerPrefab);
                        }
                    } 

                    if ((tileType == TileType.EPeninsula || tileType == TileType.NECorner) && enter) {
                        lShapePosition = tilePos + new Vector2Int(-2, 0); // Position to the northwest
                        cornerPrefab = CornerNEPrefab;
                        Debug.Log("E-Peninsula: " + tilePos);
                        
                        // Check if the L-shape is created by checking the neighboring positions that form an L
                        if (lShapePosition != Vector2Int.zero && !borderPositions.Contains(lShapePosition)) {
                            Vector2Int basePosition = tilePos + new Vector2Int(-1, -1);
                            Debug.Log("L-shape at position: " + lShapePosition);
                            Debug.Log("Overlay at position: " + basePosition);
                            GameObject basePrefab = tileObjects[basePosition];
                            enter = false;
                            OverlayPrefabs(basePosition, basePrefab, cornerPrefab);
                        }
                    } 

                    
                    if ((tileType == TileType.SPeninsula || tileType == TileType.SWCorner) && enter) {
                        lShapePosition = tilePos + new Vector2Int(-2, 0); // Position to the southwest
                        cornerPrefab = CornerSWPrefab;
                        Debug.Log("S-Peninsula: " + tilePos);
                        
                        // Check if the L-shape is created by checking the neighboring positions that form an L
                        if (lShapePosition != Vector2Int.zero && !borderPositions.Contains(lShapePosition)) {
                            Vector2Int basePosition = tilePos + new Vector2Int(-1, 1);
                            Debug.Log("S-L-shape at position: " + lShapePosition);
                            Debug.Log("S-Overlay at position: " + basePosition);
                            GameObject basePrefab = tileObjects[basePosition];
                            enter = false;
                            OverlayPrefabs(basePosition, basePrefab, cornerPrefab);
                        }
                    } 

                    if ((tileType == TileType.SPeninsula || tileType == TileType.SECorner) && enter) {
                        lShapePosition = tilePos + new Vector2Int(0, -2); // Position to the southeast
                        cornerPrefab = CornerSEPrefab;
                        Debug.Log("SW-Peninsula: " + tilePos);
                        
                        // Check if the L-shape is created by checking the neighboring positions that form an L
                        if (lShapePosition != Vector2Int.zero && !borderPositions.Contains(lShapePosition)) {
                            Vector2Int basePosition = tilePos + new Vector2Int(-1, -1);
                            Debug.Log("SW-L-shape at position: " + lShapePosition);
                            Debug.Log("SW-Overlay at position: " + basePosition);
                            GameObject basePrefab = tileObjects[basePosition];
                            enter = false;
                            OverlayPrefabs(basePosition, basePrefab, cornerPrefab);
                        }
                    } 

                    if ((tileType == TileType.NPeninsula || tileType == TileType.NECorner) && enter) {
                        lShapePosition = tilePos + new Vector2Int(2, 0); // Position to the southeast
                        cornerPrefab = CornerNEPrefab;
                        Debug.Log("N-Peninsula: " + tilePos);
                        
                        // Check if the L-shape is created by checking the neighboring positions that form an L
                        if (lShapePosition != Vector2Int.zero && !borderPositions.Contains(lShapePosition)) {
                            Vector2Int basePosition = tilePos + new Vector2Int(1, -1);
                            Debug.Log("N-L-shape at position: " + lShapePosition);
                            Debug.Log("N-Overlay at position: " + basePosition);
                            GameObject basePrefab = tileObjects[basePosition];
                            enter = false;
                            OverlayPrefabs(basePosition, basePrefab, cornerPrefab);
                        }
                    } 

                    if ((tileType == TileType.NPeninsula || tileType == TileType.NWCorner) && enter) {
                        lShapePosition = tilePos + new Vector2Int(0, -2); // Position to the southeast
                        cornerPrefab = CornerNWPrefab;
                        Debug.Log("NW-Peninsula: " + tilePos);
                        
                        // Check if the L-shape is created by checking the neighboring positions that form an L
                        if (lShapePosition != Vector2Int.zero && !borderPositions.Contains(lShapePosition)) {
                            Vector2Int basePosition = tilePos + new Vector2Int(1, -1);
                            Debug.Log("NW-L-shape at position: " + lShapePosition);
                            Debug.Log("NW-Overlay at position: " + basePosition);
                            GameObject basePrefab = tileObjects[basePosition];
                            enter = false;
                            OverlayPrefabs(basePosition, basePrefab, cornerPrefab);
                        }
                    } 

                }
            }
        }

        // Function to overlay two prefabs on the same tile
        void OverlayPrefabs(Vector2Int position, GameObject basePrefab, GameObject overlayPrefab) {
            // Find the existing tile object
            if (tileObjects.TryGetValue(position, out GameObject oldTile)) {

                // Instantiate the overlay prefab and place it at a slightly higher Z-axis to stack on top of the base
                GameObject overlayTile = Instantiate(overlayPrefab, new Vector3(position.x * scale * 10, position.y * scale * 5, 1f), Quaternion.identity);
                overlayTile.name = overlayPrefab.name + "(" + position.x + ", " + position.y + ")";
                overlayTile.transform.localScale = new Vector3(scale, scale, 1);

                // Update the tileObjects dictionary with the new base tile
                tileObjects[position] = overlayTile;

            } else {
                Debug.LogError("Tile at position " + position + " not found in tileObjects.");
            }
        }

    }
}