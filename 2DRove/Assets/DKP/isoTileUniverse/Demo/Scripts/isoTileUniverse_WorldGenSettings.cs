using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace isoTile_Universe
{

    public enum BiomeLayerTypes
    {
        Basic,
        Secondary,
        First,
        Second,
        Third,
        Fourth
    }

    public enum PerlinLayerTypes
    {
        LayerA,
        LayerB,
        LayerC,
        LayerD,
        LayerE,
        LayerF,
        LayerG
    }

    public class isoTileUniverse_WorldGenSettings : MonoBehaviour
    {

        void Start()
        {
            RiverBot_GOs_List = new List<GameObject>();
        }

        // Basic And Secondary Tiles Of Biome
        [Header("Basic Tiles Of Biome (Randomly Spawned):")]
        public Tile[] Basic_Tiles_Of_Biome_Array;

        // Perlin Based Tiles
        public Tile[] Tiles_Of_Biome_Perlin_LayerA_Array;
        public Tile[] Tiles_Of_Biome_Perlin_LayerB_Array;
        public Tile[] Tiles_Of_Biome_Perlin_LayerC_Array;
        public Tile[] Tiles_Of_Biome_Perlin_LayerD_Array;
        public Tile[] Tiles_Of_Biome_Perlin_LayerE_Array;
        public Tile[] Tiles_Of_Biome_Perlin_LayerF_Array;
        public Tile[] Tiles_Of_Biome_Perlin_LayerG_Array;
        private TileBase GetRandomTileFrom_PerlinArray(PerlinLayerTypes layerTypeToGet)
        {
            if (layerTypeToGet == PerlinLayerTypes.LayerA)
            {
                if (Tiles_Of_Biome_Perlin_LayerA_Array.Length > 0)
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerA_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerA_Array[randomTileIndex];
                }
                else
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerA_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerA_Array[randomTileIndex];
                }
            }
            else if (layerTypeToGet == PerlinLayerTypes.LayerB)
            {
                if (Tiles_Of_Biome_Perlin_LayerB_Array.Length > 0)
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerB_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerB_Array[randomTileIndex];
                }
                else
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerA_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerA_Array[randomTileIndex];
                }
            }
            else if (layerTypeToGet == PerlinLayerTypes.LayerC)
            {
                if (Tiles_Of_Biome_Perlin_LayerC_Array.Length > 0)
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerC_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerC_Array[randomTileIndex];
                }
                else
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerA_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerA_Array[randomTileIndex];
                }
            }
            else if (layerTypeToGet == PerlinLayerTypes.LayerD)
            {
                if (Tiles_Of_Biome_Perlin_LayerD_Array.Length > 0)
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerD_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerD_Array[randomTileIndex];
                }
                else
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerA_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerA_Array[randomTileIndex];
                }
            }
            else if (layerTypeToGet == PerlinLayerTypes.LayerE)
            {
                if (Tiles_Of_Biome_Perlin_LayerE_Array.Length > 0)
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerE_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerE_Array[randomTileIndex];
                }
                else
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerA_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerA_Array[randomTileIndex];
                }
            }
            else if (layerTypeToGet == PerlinLayerTypes.LayerF)
            {
                if (Tiles_Of_Biome_Perlin_LayerF_Array.Length > 0)
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerF_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerF_Array[randomTileIndex];
                }
                else
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerA_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerA_Array[randomTileIndex];
                }
            }
            else if (layerTypeToGet == PerlinLayerTypes.LayerG)
            {
                if (Tiles_Of_Biome_Perlin_LayerG_Array.Length > 0)
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerG_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerG_Array[randomTileIndex];
                }
                else
                {
                    int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerA_Array.Length);
                    return Tiles_Of_Biome_Perlin_LayerA_Array[randomTileIndex];
                }
            }
            else
            {
                int randomTileIndex = Random.Range(0, Tiles_Of_Biome_Perlin_LayerA_Array.Length);
                return Tiles_Of_Biome_Perlin_LayerA_Array[randomTileIndex];
            }
        }

        // River Spawning
        public Tilemap TilemapToGenerateOn;
        public bool Spawn_BiomeRivers = false;
        public int RiverBotsSpawned_Count = 0;
        public float ChanceToSpawn_RiverBot = 2f;
        public float ChanceToSpawn_02_RiverBot = 2f;
        public Tile Biome_River_BaseTile;
        public Tile[] Biome_River_PossibleTravelTiles;
        public List<GameObject> RiverBot_GOs_List;
        public void Spawn_RiverBot(Vector3Int riverBotStartingCell)
        {
            if (TilemapToGenerateOn != null)
            {
                if (Random.Range(0, 100) < ChanceToSpawn_02_RiverBot)
                {
                    //Debug.Log("Spawning River Bot...");
                    RiverBotsSpawned_Count++;
                    GameObject newRiverBotGO = new GameObject("RiverBot_" + RiverBotsSpawned_Count.ToString("##0"));
                    newRiverBotGO.transform.SetParent(gameObject.transform, false);
                    RiverBot_GOs_List.Add(newRiverBotGO);
                    isoTileUniverse_RiverAndRoadBot riverBotScript = newRiverBotGO.AddComponent<isoTileUniverse_RiverAndRoadBot>();
                    riverBotScript.Initialize_Bot(TilemapToGenerateOn, Biome_River_PossibleTravelTiles, Biome_River_BaseTile, riverBotStartingCell);
                    isoTileUniverse_TilemapGen.GlobalAccess.RiverAndRoad_Bots.Add(riverBotScript);
                }
            }
        }

        // Override Global Perlin Settings
        public float Override_xOrgLayer = 1000;
        public float Override_yOrgLayer = 1000;
        public float Override_WorldScale_MINValue = 0.5f;
        public float Override_WorldScale_MAXValue = 2f;

        public TileBase GetRandom_Biome_MapTile_Perlin(int x, int y)
        {
            // Choose Map Tiles Based On Perlin Noise
            // Perlin Noise

            if (Spawn_BiomeRivers)
            {
                if (Random.Range(0f, 100f) < ChanceToSpawn_RiverBot)
                {
                    Spawn_RiverBot(new Vector3Int(x, y, 0));
                }
            }

            // Overrides
            float WorldScale = Random.Range(Override_WorldScale_MINValue, Override_WorldScale_MAXValue);
            
            float xCoord = Override_xOrgLayer + x / (float)10 * WorldScale;
            float yCoord = Override_yOrgLayer + y / (float)10 * WorldScale;
            float sample = Mathf.PerlinNoise(xCoord, yCoord);

            TileBase tileToUse = GetRandomTileFrom_PerlinArray(PerlinLayerTypes.LayerA);

            if (sample < 0.1f)
            {
                tileToUse = GetRandomTileFrom_PerlinArray(PerlinLayerTypes.LayerB);
            }
            else if (sample >= 0.1f && sample < 0.15f)
            {
                tileToUse = GetRandomTileFrom_PerlinArray(PerlinLayerTypes.LayerC);
            }
            else if (sample >= 0.15f && sample < 0.23f)
            {
                tileToUse = GetRandomTileFrom_PerlinArray(PerlinLayerTypes.LayerD);
            }
            else if (sample >= 0.25f && sample < 0.3f)
            {
                tileToUse = GetRandomTileFrom_PerlinArray(PerlinLayerTypes.LayerE);
            }
            else if (sample >= 0.3f && sample < 0.35f)
            {
                tileToUse = GetRandomTileFrom_PerlinArray(PerlinLayerTypes.LayerF);
            }
            else if (sample >= 0.40f && sample < 0.55f)
            {
                //tileToUse = GetRandomTileFromArray(Tiles_Dirt);
                tileToUse = GetRandomTileFrom_PerlinArray(PerlinLayerTypes.LayerG); //Get_ActiveTileSet().Ocean_01;
            }
            else
            {
                tileToUse = GetRandomTileFrom_PerlinArray(PerlinLayerTypes.LayerA);
                //tileToUse = Get_ActiveTileSet().GetRandom_BaseGround_01();
            }

            if (tileToUse == null)
            {
                int randomTileIndex = Random.Range(0, Basic_Tiles_Of_Biome_Array.Length);
                return Basic_Tiles_Of_Biome_Array[randomTileIndex];
            }

            return tileToUse;
        }

    }
}