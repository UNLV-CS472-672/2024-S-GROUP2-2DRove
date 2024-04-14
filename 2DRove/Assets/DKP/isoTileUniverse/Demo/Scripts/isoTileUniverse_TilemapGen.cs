using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace isoTile_Universe
{
    public enum TileWorldGenerationTypes
    {
        SpecifiedTiles,
        BiomeWorld_Generation
    }
    public enum BiomeTypes
    {
        GreenForest,
        SnowForest,
        Desert,
        Tundra,
        Swamp,
        RocksAndMountains,
        TundraRocksAndMountains,
        Volcanic,
        BlightedLands,
        GrassOcean,
        TundraOcean,
        SwampOcean,
        DesertOcean,
        VolcanicOcean,
        SandLake,
        BlightedOcean,
        MetalWorld,
        MetalWorldOcean
    }
    public enum Direction4_TilesetTypes
    {
        LightShallowRiver,
        SandLake,
        SandRoad_Tier1,
        SwampRiver,
        TundraOcean,
        CementRoad_Tier1,
        GrassRoad_Tier1,
        DesertRoad_Tier1,
        SwampRoad_Tier1,
        TundraRoad_Tier1,
        GrassOcean,
        SwampOcean,
        DesertOcean,
        LavaRiver,
        VolcanicOcean,
        SmallMagmaRiver,
        BlightedRiver,
        BlightedOcean,
        MetalWorldOcean
    }

    public enum DirectionTypes
    {
        Top,
        Bottom,
        Left,
        Right,
        North,
        South,
        East,
        West
    }

    public class isoTileUniverse_TilemapGen : MonoBehaviour
    {
        public static isoTileUniverse_TilemapGen GlobalAccess;
        void Awake()
        {
            GlobalAccess = this;
        }

        [Header("Tilemap Camera:")]
        public Transform MainCamera_Transform;

        public bool Show_DemoInstructionsGUI = true;

        public Tilemap Tilemap_ToGenerateOn;
        public Tile[] Tiles_ToUseWhenGenerating;
        public float RandomChanceToSpawn_BelowGenArray = 25f;
        public Tile[] Tiles_ToUseWhenGenerating_Random;

        public bool Regenerate_IsoMap = false;

        [Header("Generated Map Stats")]
        public TileWorldGenerationTypes WorldGenType = TileWorldGenerationTypes.BiomeWorld_Generation;
        public bool RandomlySpawn_TilesIn = false;
        public int RandomSeed = 1000;
        public int IsoMapSize_Width = 20;
        public int IsoMapSize_Height = 20;
        public float xOrg_Layer1;
        public float yOrg_Layer1;
        public float WorldScale = 20.0F;
        public int Snow_GenerationDepth_MIN = 76;
        public int Snow_GenerationDepth_MAX = 84;

        // Single Biome Generation -
        [Header("Single Biome Generation:")]
        public bool GenerateSingleBiome = false;
        public int Single_BiomeInterations = 4;
        public BiomeTypes Single_BiomeType = BiomeTypes.GreenForest;
        public BiomeTypes Single_BiomeOceanType = BiomeTypes.GrassOcean;

        // Biome Settings
        [Header("Biome Settings:")]
        public isoTileUniverse_WorldGenSettings GreenForest_BiomeSettings;
        public isoTileUniverse_WorldGenSettings SnowForest_BiomeSettings;
        public isoTileUniverse_WorldGenSettings RocksAndMountains_BiomeSettings;
        public isoTileUniverse_WorldGenSettings TundraRocksAndMountains_BiomeSettings;
        public isoTileUniverse_WorldGenSettings Swamp_BiomeSettings;
        public isoTileUniverse_WorldGenSettings Desert_BiomeSettings;
        public isoTileUniverse_WorldGenSettings Tundra_BiomeSettings;
        public isoTileUniverse_WorldGenSettings Volcanic_BiomeSettings;
        public isoTileUniverse_WorldGenSettings BlightedLands_BiomeSettings;
        public isoTileUniverse_WorldGenSettings GrassOcean_BiomeSettings;
        public isoTileUniverse_WorldGenSettings TundraOcean_BiomeSettings;
        public isoTileUniverse_WorldGenSettings SwampOcean_BiomeSettings;
        public isoTileUniverse_WorldGenSettings DesertOcean_BiomeSettings;
        public isoTileUniverse_WorldGenSettings VolcanicOcean_BiomeSettings;
        public isoTileUniverse_WorldGenSettings SandLake_BiomeSettings;
        public isoTileUniverse_WorldGenSettings BlightedOcean_BiomeSettings;

        // Metal World Settings
        [Header("Metal World Settings:")]
        public isoTileUniverse_WorldGenSettings MetalWorld_BiomeSettings;
        public isoTileUniverse_WorldGenSettings MetalWorldOcean_BiomeSettings;

        // Blank Map
        [Header("Blank Map Tiles")]
        public Tile BlankGrid_MapTile;

        // Specific Tilesets
        [Header("Tile Arrays")]
        public Tile[] Tiles_BiomeMarkers;
        public Tile[] Tiles_Dirt;
        public Tile[] Tiles_Sand;
        public Tile[] Tiles_Grass;
        public Tile[] Tiles_SnowyGrass;
        public Tile[] Tiles_Forest;
        public Tile[] Tiles_SnowyForest;
        public Tile[] Tiles_SwampMud;
        public Tile[] Tiles_SwampForest;
        public Tile[] Tiles_DesertSands;
        public Tile[] Tiles_DesertRocks;
        public Tile[] Tiles_DesertMountains;
        public Tile[] Tiles_Rocks;
        public Tile[] Tiles_Rocks_Set02;
        public Tile[] Tiles_Mountains;
        public Tile[] Tiles_Mountains_Set02;
        public Tile[] Tiles_TundraRocks;
        public Tile[] Tiles_TundraMountains;
        public Tile[] Tiles_BurntGrass;
        public Tile[] Tiles_BurntForest;
        public Tile[] Tiles_LavaGrounds;
        public Tile[] Tiles_CooledLavaGrounds;

        [Header("Resources In Rocks:")]
        public bool Spawn_Copper_OnMap = false;
        public float CopperSpawnChance = 10f;
        public Tile[] Tiles_Resource_Copper_Set01;
        public bool Spawn_Iron_OnMap = false;
        public float IronSpawnChance = 10f;
        public Tile[] Tiles_Resource_Iron_Set01;
        public bool Spawn_Silver_OnMap = false;
        public float SilverSpawnChance = 10f;
        public Tile[] Tiles_Resource_Silver_Set01;
        public bool Spawn_Gold_OnMap = false;
        public float GoldSpawnChance = 10f;
        public Tile[] Tiles_Resource_Gold_Set01;

        [Header("Metal World:")]
        public Tile[] Tiles_MetalWorld_GroundPlates_Tier1;
        public Tile[] Tiles_MetalWorld_IronForest_Tier1;
        public Tile[] Tiles_MetalWorld_MetalRocks_Tier1;
        public Tile[] Tiles_MetalWorld_MetalMountains_Tier1;

        [Header("Colony Buildings:")]
        public Tile[] Tiles_ColonyBuildings_Tier1;

        [Header("City Buildings - Tier 1:")]
        public Tile[] Tiles_CityBuildings_Tier1;
        public Tile[] Tiles_CityBuildings_Tier1_Damage_Light;
        public Tile[] Tiles_CityBuildings_Tier1_Damage_Medium;
        public Tile[] Tiles_CityBuildings_Tier1_Damage_Heavy;
        public Tile[] Tiles_CityBuildings_Tier1_Damage_Ruins;

        [Header("City Buildings - Tier 3:")]
        public Tile[] Tiles_CityBuildings_Tier2;
        public Tile[] Tiles_CityBuildings_Tier2_Damage_Light;
        public Tile[] Tiles_CityBuildings_Tier2_Damage_Medium;
        public Tile[] Tiles_CityBuildings_Tier2_Damage_Heavy;
        public Tile[] Tiles_CityBuildings_Tier2_Damage_Ruins;

        [Header("City Buildings - Tier 3:")]
        public Tile[] Tiles_CityBuildings_Tier3;
        public Tile[] Tiles_CityBuildings_Tier3_Damage_Light;
        public Tile[] Tiles_CityBuildings_Tier3_Damage_Medium;
        public Tile[] Tiles_CityBuildings_Tier3_Damage_Heavy;
        public Tile[] Tiles_CityBuildings_Tier3_Damage_Ruins;

        [Header("Volcanic Biome:")]
        public Tile[] Tiles_VolcanicAshGround;
        public Tile[] Tiles_VolcanicAshGroundDark;
        public Tile[] Tiles_VolcanicLavaGround;
        public Tile[] Tiles_VolcanicCooledLavaGround;
        public Tile[] Tiles_VolcanicRocks;
        public Tile[] Tiles_VolcanicMountains;

        [Header("Blighted Lands Biome:")]
        public Tile[] Tiles_BlightedLands_InfectedGrounds;
        public Tile[] Tiles_BlightedLands_InfectedNodules;
        public Tile[] Tiles_BlightedLands_InfectedGrass;
        public Tile[] Tiles_BlightedLands_InfectedForest;
        public Tile[] Tiles_BlightedLands_InfectedRocks;
        public Tile[] Tiles_BlightedLands_InfectedMountains;

        // Light Shallow River
        public TileBase Base_LightShallowRiver_01_Tile;
        public TileBase[] LightShallowRiver_01_Tile_Array;

        // Sand Lake
        public TileBase Base_SandLake_01_Tile;
        public TileBase[] SandLake_01_Tile_Array;

        // Sand Road - Tier 1
        public TileBase Base_SandRoad_Tier1_01_Tile;
        public TileBase[] SandRoad_Tier1_01_Tile_Array;

        // Cement Road - Tier 1
        public TileBase Base_CementRoad_Tier1_01_Tile;
        public TileBase[] CementRoad_Tier1_01_Tile_Array;

        // Desert Road - Tier 1
        public TileBase Base_DesertRoad_Tier1_01_Tile;
        public TileBase[] DesertRoad_Tier1_01_Tile_Array;

        // Swamp Road - Tier 1
        public TileBase Base_SwampRoad_Tier1_01_Tile;
        public TileBase[] SwampRoad_Tier1_01_Tile_Array;

        // Grass Road - Tier 1
        public TileBase Base_GrassRoad_Tier1_01_Tile;
        public TileBase[] GrassRoad_Tier1_01_Tile_Array;

        // Tundra Road - Tier 1
        public TileBase Base_TundraRoad_Tier1_01_Tile;
        public TileBase[] TundraRoad_Tier1_01_Tile_Array;

        // Swamp River
        public TileBase Base_SwampRiver_01_Tile;
        public TileBase[] SwampRiver_01_Tile_Array;

        // Lava River
        public TileBase Base_LavaRiver_01_Tile;
        public TileBase[] LavaRiver_01_Tile_Array;

        // Blighted River
        public TileBase Base_BlightedRiver_01_Tile;
        public TileBase[] BlightedRiver_01_Tile_Array;

        // Small Magma River
        public TileBase Base_SmallMagmaRiver_01_Tile;
        public TileBase[] SmallMagmaRiver_01_Tile_Array;

        // Tundra Ocean
        public TileBase Base_TundraOcean_01_Tile;
        public TileBase[] TundraOcean_01_Tile_Array;

        // Grass Ocean
        public TileBase Base_GrassOcean_01_Tile;
        public TileBase[] GrassOcean_01_Tile_Array;

        // Swamp Ocean
        public TileBase Base_SwampOcean_01_Tile;
        public TileBase[] SwampOcean_01_Tile_Array;

        // Desert Ocean
        public TileBase Base_DesertOcean_01_Tile;
        public TileBase[] DesertOcean_01_Tile_Array;

        // Volcanic Ocean
        public TileBase Base_VolcanicOcean_01_Tile;
        public TileBase[] VolcanicOcean_01_Tile_Array;

        // Blighted Ocean
        public TileBase Base_BlightedOcean_01_Tile;
        public TileBase[] BlightedOcean_01_Tile_Array;

        // Metal World Ocean
        public TileBase Base_MetalWorldOcean_01_Tile;
        public TileBase[] MetalWorldOcean_01_Tile_Array;

        // Tileset Displays
        [Header("Tileset Displays:")]
        public bool Display_Tileset = false;
        public Color Camera_BackgroundColor = Color.white;
        public List<TileBase> TilesToDisplay_AsSet_List;

        // Start is called before the first frame update
        void Start()
        {
            Volcanic_BiomeSettings.TilemapToGenerateOn = Tilemap_ToGenerateOn;
            Swamp_BiomeSettings.TilemapToGenerateOn = Tilemap_ToGenerateOn;

            // Setup Camera - Restraining It To The Generated Map
            MainCamera_Transform.SetParent(null);
            isoTileUniverse_CameraController.GlobalAccess.DisconnectMapBoundGOs();
            Vector3Int mapStartingPos = new Vector3Int(-(IsoMapSize_Width / 2), -(IsoMapSize_Height / 2), 0);
            isoTileUniverse_CameraController.GlobalAccess.SetupCameraBounds(Tilemap_ToGenerateOn.GetCellCenterWorld(mapStartingPos));
            Setup_isoTileUniverse_Camera();
            isoTileUniverse_CameraController.GlobalAccess.Set_CameraZoom_Level2();

            RiverAndRoad_Bots = new List<isoTileUniverse_RiverAndRoadBot>();

            Generate_TileSet_Sample();

            WorldGenComplete = true;
        }

        private void Setup_isoTileUniverse_Camera()
        {
            // Generate Tiles
            Vector3Int currentCell = new Vector3Int(0, 0, 0);
            Vector3Int mapStartingPos = new Vector3Int(-(IsoMapSize_Width / 2), -(IsoMapSize_Height / 2), 0);
            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
            {
                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                {
                    // Set Map Tile
                    currentCell = new Vector3Int(w, h, 0);
                    isoTileUniverse_CameraController.GlobalAccess.Update_CameraBounds(Tilemap_ToGenerateOn.GetCellCenterLocal(currentCell));
                }
            }
        }

        private void Generate_TileSet_Sample()
        {
            // Generate Tiles On Tilemap
            if (Tilemap_ToGenerateOn != null)
            {
                // Destroy All River And Road Bots
                if (RiverAndRoad_Bots.Count > 0)
                {
                    for (int i = 0; i < RiverAndRoad_Bots.Count; i++)
                    {
                        Destroy(RiverAndRoad_Bots[i].gameObject);
                    }
                    RiverAndRoad_Bots.Clear();
                }
                Process_RiverAndRoadBot_Activities = true;

                if (Tiles_ToUseWhenGenerating.Length > 0)
                {
                    // Set Randomize Seed
                    RandomSeed = Random.Range(0, 10000);
                    Random.InitState(RandomSeed);

                    // Clear Any Tiles On Tilemap
                    Tilemap_ToGenerateOn.ClearAllTiles();

                    if (Display_Tileset)
                    {

                        // Set Camera Background
                        isoTileUniverse_CameraController.GlobalAccess.CameraScript.backgroundColor = isoTileUniverse_CameraController.GlobalAccess.Normal_Camera_TilesetDisplayColor;

                        // Scripting Override Of Tiles
                        TilesToDisplay_AsSet_List = new List<TileBase>();
                        for (int i = 0; i < BlightedOcean_01_Tile_Array.Length; i++)
                        {
                            TilesToDisplay_AsSet_List.Add(BlightedOcean_01_Tile_Array[i]);
                        }
                        //for (int i = 0; i < Tiles_BlightedLands_InfectedMountains.Length; i++)
                        //{
                        //    TilesToDisplay_AsSet_List.Add(Tiles_BlightedLands_InfectedMountains[i]);
                        //}

                        // Setup Camera
                        Vector3 cameraPosition = new Vector3(0.5f, 1.1f, -10f);
                        if (TilesToDisplay_AsSet_List.Count == 8)
                            cameraPosition = new Vector3(0.5f, 0.9f, -10f);
                        else if (TilesToDisplay_AsSet_List.Count == 16)
                            cameraPosition = new Vector3(1.5f, 1.35f, -10f);
                        Camera.main.transform.position = cameraPosition;
                        Camera cameraScript = Camera.main.gameObject.GetComponent<Camera>();
                        if (TilesToDisplay_AsSet_List.Count == 8)
                            cameraScript.orthographicSize = 1.75f;
                        else if (TilesToDisplay_AsSet_List.Count == 16)
                            cameraScript.orthographicSize = 2.25f;

                        Tilemap_ToGenerateOn.ClearAllTiles();
                        Vector3Int blankmapStartingCell = new Vector3Int(-20, -20, 0);
                        for (int w = -20; w < 20; w++)
                        {
                            for (int h = -20; h < 20; h++)
                            {
                                Vector3Int currentCell = new Vector3Int(w, h, 0);
                                Tilemap_ToGenerateOn.SetTile(currentCell, BlankGrid_MapTile);
                            }
                        }


                        // Display A Certain Tileset
                        if (TilesToDisplay_AsSet_List.Count > 0)
                        {
                            Vector3Int startingTileCell = new Vector3Int(0, 0, 0);
                            int tileSetHalfwayMark = TilesToDisplay_AsSet_List.Count / 2;

                            //Tilemap_ToGenerateOn.ClearAllTiles();
                            int iPos = 0;
                            int iPos2 = 0;
                            for (int i = 0; i < TilesToDisplay_AsSet_List.Count; i++)
                            {
                                if (i < tileSetHalfwayMark)
                                {
                                    Vector3Int currentCell = startingTileCell;
                                    currentCell.x = iPos;
                                    Tilemap_ToGenerateOn.SetTile(currentCell, TilesToDisplay_AsSet_List[i]);
                                    iPos++;
                                }
                                else
                                {
                                    Vector3Int currentCell = startingTileCell;
                                    currentCell.x = iPos2;
                                    currentCell.y = 1;
                                    Tilemap_ToGenerateOn.SetTile(currentCell, TilesToDisplay_AsSet_List[i]);
                                    iPos2++;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (WorldGenType == TileWorldGenerationTypes.SpecifiedTiles) // Uses Tiles In Array: Tiles_ToUseWhenGenerating
                        {
                            // Generate Tiles
                            Vector3Int currentCell = new Vector3Int(0, 0, 0);
                            Vector3Int mapStartingPos = new Vector3Int(-(IsoMapSize_Width / 2), -(IsoMapSize_Height / 2), 0);
                            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
                            {
                                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                                {
                                    // Set Map Tile
                                    currentCell = new Vector3Int(w, h, 0);
                                    if (Tilemap_ToGenerateOn.GetTile(currentCell) == null)
                                    {
                                        bool spawnATile = true;
                                        if (RandomlySpawn_TilesIn)
                                        {
                                            if (Random.Range(0, 100) < 45)
                                            {
                                                if (Tiles_ToUseWhenGenerating_Random.Length > 0)
                                                {
                                                    if (Random.Range(0, 100) < RandomChanceToSpawn_BelowGenArray)
                                                    {
                                                        int randomTileIndex = Random.Range(0, Tiles_ToUseWhenGenerating_Random.Length);
                                                        Tilemap_ToGenerateOn.SetTile(currentCell, Tiles_ToUseWhenGenerating_Random[randomTileIndex]);
                                                    }
                                                    else
                                                    {
                                                        int randomTileIndex = Random.Range(0, Tiles_ToUseWhenGenerating.Length);
                                                        Tilemap_ToGenerateOn.SetTile(currentCell, Tiles_ToUseWhenGenerating[randomTileIndex]);
                                                    }
                                                }
                                                else
                                                {
                                                    int randomTileIndex = Random.Range(0, Tiles_ToUseWhenGenerating.Length);
                                                    Tilemap_ToGenerateOn.SetTile(currentCell, Tiles_ToUseWhenGenerating[randomTileIndex]);
                                                }
                                            }
                                            spawnATile = false;
                                        }
                                        if (spawnATile)
                                        {
                                            if (Tiles_ToUseWhenGenerating_Random.Length > 0)
                                            {
                                                if (Random.Range(0, 100) < RandomChanceToSpawn_BelowGenArray)
                                                {
                                                    int randomTileIndex = Random.Range(0, Tiles_ToUseWhenGenerating_Random.Length);
                                                    Tilemap_ToGenerateOn.SetTile(currentCell, Tiles_ToUseWhenGenerating_Random[randomTileIndex]);
                                                }
                                                else
                                                {
                                                    int randomTileIndex = Random.Range(0, Tiles_ToUseWhenGenerating.Length);
                                                    Tilemap_ToGenerateOn.SetTile(currentCell, Tiles_ToUseWhenGenerating[randomTileIndex]);
                                                }
                                            }
                                            else
                                            {
                                                int randomTileIndex = Random.Range(0, Tiles_ToUseWhenGenerating.Length);
                                                Tilemap_ToGenerateOn.SetTile(currentCell, Tiles_ToUseWhenGenerating[randomTileIndex]);
                                            }
                                        }
                                    }
                                }
                            }

                            // Process Adj Tiles
                            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
                            {
                                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                                {
                                    // Set Map Tile
                                    currentCell = new Vector3Int(w, h, 0);
                                    ProcessAdjacentTiles(Tilemap_ToGenerateOn, currentCell);
                                }
                            }
                        }
                        else if (WorldGenType == TileWorldGenerationTypes.BiomeWorld_Generation) // Uses Tiles In Tile Arrays
                        {
                            Generate_Biome_PerlinWorld_FromTileArrays();

                            // Place Resources In Rocks
                            PlaceResourcesOnMap();

                            //GenerateCityCenters_InWorld();
                        }
                    }
                }
            }
        }

        private void PlaceResourcesOnMap()
        {
            // Generate Tiles
            Vector3Int currentCell = new Vector3Int(0, 0, 0);
            Vector3Int mapStartingPos = new Vector3Int(-(IsoMapSize_Width / 2), -(IsoMapSize_Height / 2), 0);
            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
            {
                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                {
                    // Set Map Tile
                    currentCell = new Vector3Int(w, h, 0);
                    if (Tilemap_ToGenerateOn.GetTile(currentCell) != null)
                    {
                        TileBase tileOnCell = Tilemap_ToGenerateOn.GetTile(currentCell);
                        if (TileNameIsRock(tileOnCell.name))
                        {
                            bool hasSpawnedResources = false;

                            if (!hasSpawnedResources)
                            {
                                if (Spawn_Copper_OnMap)
                                {
                                    // Spawn Copper
                                    if (Random.Range(0, 100) < CopperSpawnChance)
                                    {
                                        Tilemap_ToGenerateOn.SetTile(currentCell, GetRandomTileFromArray(Tiles_Resource_Copper_Set01));
                                        hasSpawnedResources = true;
                                    }
                                }
                            }

                            if (!hasSpawnedResources)
                            {
                                if (Spawn_Iron_OnMap)
                                {
                                    // Spawn Copper
                                    if (Random.Range(0, 100) < IronSpawnChance)
                                    {
                                        Tilemap_ToGenerateOn.SetTile(currentCell, GetRandomTileFromArray(Tiles_Resource_Iron_Set01));
                                        hasSpawnedResources = true;
                                    }
                                }
                            }

                            if (!hasSpawnedResources)
                            {
                                if (Spawn_Silver_OnMap)
                                {
                                    // Spawn Copper
                                    if (Random.Range(0, 100) < SilverSpawnChance)
                                    {
                                        Tilemap_ToGenerateOn.SetTile(currentCell, GetRandomTileFromArray(Tiles_Resource_Silver_Set01));
                                        hasSpawnedResources = true;
                                    }
                                }
                            }

                            if (!hasSpawnedResources)
                            {
                                if (Spawn_Gold_OnMap)
                                {
                                    // Spawn Copper
                                    if (Random.Range(0, 100) < GoldSpawnChance)
                                    {
                                        Tilemap_ToGenerateOn.SetTile(currentCell, GetRandomTileFromArray(Tiles_Resource_Gold_Set01));
                                        hasSpawnedResources = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private List<Vector3Int> BuildableTiles_Cells_List;
        private List<Vector3Int> City_Center_Cells_List;

        private void Generate_Biome_PerlinWorld_FromTileArrays()
        {
            // Generate Tiles
            Vector3Int currentCell = new Vector3Int(0, 0, 0);
            int x = 0;
            int y = 0;

            xOrg_Layer1 = Random.Range(0, 5000);
            yOrg_Layer1 = Random.Range(0, 5000);
            WorldScale = Random.Range(0.5f, 1.5f);

            List<Vector3Int> Possible_TileCells = new List<Vector3Int>();

            // Generate Plain Map
            Vector3Int mapStartingPos = new Vector3Int(-(IsoMapSize_Width / 2), -(IsoMapSize_Height / 2), 0);
            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
            {
                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                {
                    x = w;
                    y = h;
                    // Set Map Tile
                    currentCell = new Vector3Int(w, h, 0);
                    Possible_TileCells.Add(currentCell);
                    if (Tilemap_ToGenerateOn.GetTile(currentCell) == null)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, BlankGrid_MapTile);
                    }
                }
            }

            // Generate Single Biome
            for (int i = 0; i < Single_BiomeInterations; i++)
            {
                // Place Biome 1
                Tile biomeTileToPlace = Tiles_BiomeMarkers[0];
                int randomBiomeCenterCell = Random.Range(0, Possible_TileCells.Count);
                if (Possible_TileCells.Count > 0)
                {
                    Generate_BiomeAroundCell(Possible_TileCells[randomBiomeCenterCell], biomeTileToPlace, Random.Range(2, 4), Random.Range(6, 8), Random.Range(3, 6));
                }
            }

            // Fill Remaining Empty Map Tiles With Biome 7
            Possible_TileCells.Clear();
            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
            {
                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                {
                    x = w;
                    y = h;
                    // Set Map Tile
                    currentCell = new Vector3Int(w, h, 0);
                    if (Tilemap_ToGenerateOn.GetTile(currentCell) == BlankGrid_MapTile)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, Tiles_BiomeMarkers[6]);
                    }
                }
            }

            // Place Biome Tiles
            // Single Biome Selection - Index 0
            Possible_TileCells.Clear();
            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
            {
                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                {
                    x = w;
                    y = h;
                    // Set Map Tile
                    currentCell = new Vector3Int(w, h, 0);
                    if (Tilemap_ToGenerateOn.GetTile(currentCell) == Tiles_BiomeMarkers[0])
                    {
                        Possible_TileCells.Add(currentCell);
                    }
                }
            }
            Generate_PerlinBiome_FromCellList(Possible_TileCells, Single_BiomeType);

            // Ocean - Single Ocean Biome Selection - Index 6
            Possible_TileCells.Clear();
            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
            {
                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                {
                    x = w;
                    y = h;
                    // Set Map Tile
                    currentCell = new Vector3Int(w, h, 0);
                    if (Tilemap_ToGenerateOn.GetTile(currentCell) == Tiles_BiomeMarkers[6])
                    {
                        Possible_TileCells.Add(currentCell);
                    }
                }
            }
            Generate_PerlinBiome_FromCellList(Possible_TileCells, Single_BiomeOceanType);

            // Process Adj Tiles
            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
            {
                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                {
                    x = w;
                    y = h;
                    // Set Map Tile
                    currentCell = new Vector3Int(w, h, 0);
                    ProcessAdjacentTiles(Tilemap_ToGenerateOn, currentCell);
                }
            }
        }

        private void Generate_BiomeAroundCell(Vector3Int biomeCenterCell, Tile biomeTileToPlace, int minVarDistance, int maxVarDistance, int iterations)
        {
            Tilemap_ToGenerateOn.SetTile(biomeCenterCell, biomeTileToPlace);
            Vector3Int newPointToGenerateFrom = biomeCenterCell;
            for (int i = 0; i < iterations; i++)
            {
                newPointToGenerateFrom.x += Random.Range(-minVarDistance, maxVarDistance);
                newPointToGenerateFrom.y += Random.Range(-minVarDistance, maxVarDistance);
                FillTilesRandomlyAroundPointForRange(newPointToGenerateFrom, Random.Range(2, 6), Random.Range(25, 75), biomeTileToPlace);
                newPointToGenerateFrom = biomeCenterCell;
            }
        }

        // Expand World Map Based On Tile Placement
        private void FillTilesRandomlyAroundPointForRange(Vector3Int centerPointCell, float distanceToFill, float chanceToPlaceTiles, Tile tileToPlace)
        {
            int startCellX = centerPointCell.x - (int)distanceToFill;
            int startCellY = centerPointCell.y - (int)distanceToFill;
            int width = (int)(distanceToFill * 2) + 1;
            int height = (int)(distanceToFill * 2) + 1;
            int endCellX = startCellX + width;
            int endCellY = startCellY + height;

            // Random Tile Placement On Map
            for (int w = startCellX; w < endCellX; w++)
            {
                for (int h = startCellY; h < endCellY; h++)
                {
                    //if (w > (IsoMapSize_Width / 2) && w < (IsoMapSize_Width / 2) &&
                    //    h > (IsoMapSize_Height / 2) && h < (IsoMapSize_Height / 2))
                    //{
                    // Do Terraforming
                    if (w < endCellX - 2 && h < endCellY - 2)
                    {
                        // Solid Center
                        Vector3Int cell = new Vector3Int(w, h, 0);
                        TileBase currentTile = Tilemap_ToGenerateOn.GetTile(cell);
                        if (currentTile == BlankGrid_MapTile)
                        {
                            // Check Tile Is Within Range
                            if (TileIsInRange(cell, distanceToFill, centerPointCell))
                            {
                                TileBase newMapTile = tileToPlace;
                                Tilemap_ToGenerateOn.SetTile(cell, newMapTile);
                            }
                        }
                    }
                    else
                    {
                        // Random Outside
                        if (Random.Range(0, 100) < chanceToPlaceTiles)
                        {
                            Vector3Int cell = new Vector3Int(w, h, 0);
                            TileBase currentTile = Tilemap_ToGenerateOn.GetTile(cell);
                            if (currentTile == BlankGrid_MapTile)
                            {
                                // Check Tile Is Within Range
                                if (TileIsInRange(cell, distanceToFill, centerPointCell))
                                {
                                    TileBase newMapTile = tileToPlace;
                                    Tilemap_ToGenerateOn.SetTile(cell, newMapTile);
                                }
                            }
                        }
                    }
                    //}
                }
            }
        }

        public bool TileIsInRange(Vector3Int originCellWorldCenter, float range, Vector3Int centerCellWorldCenter)
        {
            float distance = (new Vector2(originCellWorldCenter.x, originCellWorldCenter.y) - new Vector2(centerCellWorldCenter.x, centerCellWorldCenter.y)).magnitude;
            float rangeSqr = range + 0.5f;
            if (distance < rangeSqr)
            {
                // ok! within range, do stuff
                //Debug.Log(" distance: " + distance.ToString() + " < rangeSqr = " + rangeSqr.ToString());
                return true;
            }
            else
                return false;
        }

        private void Generate_PerlinBiome_FromCellList(List<Vector3Int> cellsOfBiome, BiomeTypes biomeToGenerate)
        {
            // Generate Tiles
            Vector3Int currentCell = new Vector3Int(0, 0, 0);
            int x = 0;
            int y = 0;

            xOrg_Layer1 = Random.Range(0, 5000);
            yOrg_Layer1 = Random.Range(0, 5000);
            WorldScale = Random.Range(0.5f, 1.5f);

            if (cellsOfBiome.Count > 0)
            {
                for (int i = 0; i < cellsOfBiome.Count; i++)
                {

                    // Set Map Tile
                    currentCell = cellsOfBiome[i];
                    x = currentCell.x;
                    y = currentCell.y;

                    if (biomeToGenerate == BiomeTypes.GreenForest)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, GreenForest_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.SnowForest)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, SnowForest_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.RocksAndMountains)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, RocksAndMountains_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.TundraRocksAndMountains)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, TundraRocksAndMountains_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.Swamp)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, Swamp_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.Desert)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, Desert_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.Tundra)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, Tundra_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.Volcanic)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, Volcanic_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.BlightedLands)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, BlightedLands_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.GrassOcean)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, GrassOcean_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.TundraOcean)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, TundraOcean_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.SwampOcean)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, SwampOcean_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.DesertOcean)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, DesertOcean_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.VolcanicOcean)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, VolcanicOcean_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.SandLake)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, SandLake_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.BlightedOcean)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, BlightedOcean_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.MetalWorld)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, MetalWorld_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                    else if (biomeToGenerate == BiomeTypes.MetalWorldOcean)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, MetalWorldOcean_BiomeSettings.GetRandom_Biome_MapTile_Perlin(x, y));
                    }
                }
            }
        }

        private void Generate_PerlinWorldFromTileArrays()
        {
            // Generate Tiles
            Vector3Int currentCell = new Vector3Int(0, 0, 0);
            int x = 0;
            int y = 0;

            xOrg_Layer1 = Random.Range(0, 5000);
            yOrg_Layer1 = Random.Range(0, 5000);
            WorldScale = Random.Range(0.5f, 1.5f);

            Vector3Int mapStartingPos = new Vector3Int(-(IsoMapSize_Width / 2), -(IsoMapSize_Height / 2), 0);
            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
            {
                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                {
                    x = w;
                    y = h;
                    // Set Map Tile
                    currentCell = new Vector3Int(w, h, 0);
                    if (Tilemap_ToGenerateOn.GetTile(currentCell) == null)
                    {
                        Tilemap_ToGenerateOn.SetTile(currentCell, GetRandom_Buildable_MapTile_Perlin(x, y));
                    }
                }
            }

            // Process Adj Tiles
            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
            {
                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                {
                    x = w;
                    y = h;
                    // Set Map Tile
                    currentCell = new Vector3Int(w, h, 0);
                    ProcessAdjacentTiles(Tilemap_ToGenerateOn, currentCell);
                }
            }
        }

        private TileBase GetRandomTileFromArray(TileBase[] tileArrayToSelectFrom)
        {
            int randomTileIndex = Random.Range(0, tileArrayToSelectFrom.Length);
            return tileArrayToSelectFrom[randomTileIndex];
        }

        private TileBase GetRandom_Buildable_MapTile_Perlin(int x, int y)
        {

            // Generate A World With: Grass, Forest, Rocks, And Mountains


            //int randomBuildableGroundTileIndex = Random.Range(0, Buildable_GroundTiles.Length);
            // Choose Map Tiles Based On Perlin Noise
            // Perlin Noise

            float xCoord = xOrg_Layer1 + x / (float)10 * WorldScale;
            float yCoord = yOrg_Layer1 + y / (float)10 * WorldScale;
            float sample = Mathf.PerlinNoise(xCoord, yCoord);

            TileBase tileToUse = GetRandomTileFromArray(Tiles_Grass);

            if (sample < 0.1f)
            {
                tileToUse = GetRandomTileFromArray(Tiles_Mountains);
            }
            else if (sample >= 0.1f && sample < 0.15f)
            {
                tileToUse = GetRandomTileFromArray(Tiles_Mountains);
            }
            else if (sample >= 0.15f && sample < 0.23f)
            {
                tileToUse = GetRandomTileFromArray(Tiles_Rocks);
            }
            else if (sample >= 0.25f && sample < 0.3f)
            {
                if (x < IsoMapSize_Height - Random.Range(Snow_GenerationDepth_MIN, Snow_GenerationDepth_MAX))
                    tileToUse = GetRandomTileFromArray(Tiles_Forest);
                else
                    tileToUse = GetRandomTileFromArray(Tiles_SnowyForest);
            }
            else if (sample >= 0.3f && sample < 0.35f)
            {
                if (x < IsoMapSize_Height - Random.Range(Snow_GenerationDepth_MIN, Snow_GenerationDepth_MAX))
                    tileToUse = GetRandomTileFromArray(Tiles_Dirt);
                else
                    tileToUse = GetRandomTileFromArray(Tiles_SnowyGrass);
            }
            else if (sample >= 0.40f && sample < 0.55f)
            {
                //tileToUse = GetRandomTileFromArray(Tiles_Dirt);
                tileToUse = Base_SandLake_01_Tile; //Get_ActiveTileSet().Ocean_01;
            }
            else
            {
                if (x < IsoMapSize_Height - Random.Range(Snow_GenerationDepth_MIN, Snow_GenerationDepth_MAX))
                    tileToUse = GetRandomTileFromArray(Tiles_Dirt);
                else
                    tileToUse = GetRandomTileFromArray(Tiles_SnowyGrass);
                //tileToUse = Get_ActiveTileSet().GetRandom_BaseGround_01();
            }

            return tileToUse;
        }

        //
        //  Process Adj Tiles
        //
        public void ProcessAdjacentTiles(Tilemap tilemapParent, Vector3Int cellToSet)
        {
            // Clear Building Adjacent Bonuses
            //RCS_BuildingsManager.GlobalAccess.Clear_BuildingsAdjacentBonuses();
            // Process Adjacent Tiles and Update
            int w = cellToSet.x;
            int h = cellToSet.y;

            // Do Processing
            Vector3Int cell = new Vector3Int(w, h, 0);

            TileBase currentTile = tilemapParent.GetTile(cell);

            if (currentTile != null)
            {
                if (NameIs_LightShallowRiver_01(currentTile.name))
                {
                    // Update Adj - Light Shallow Rivers 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.LightShallowRiver, LightShallowRiver_01_Tile_Array);
                }
                else if (NameIs_SandLake_01(currentTile.name))
                {
                    // Update Adj - Sand Lake 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.SandLake, SandLake_01_Tile_Array);
                }
                else if (NameIs_SandRoad_Tier1_01(currentTile.name))
                {
                    // Update Adj - Sand Road 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.SandRoad_Tier1, SandRoad_Tier1_01_Tile_Array);
                }
                else if (NameIs_CementRoad_Tier1_01(currentTile.name))
                {
                    // Update Adj - Cement Road 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.CementRoad_Tier1, CementRoad_Tier1_01_Tile_Array);
                }
                else if (NameIs_GrassRoad_Tier1_01(currentTile.name))
                {
                    // Update Adj - Grass Road 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.GrassRoad_Tier1, GrassRoad_Tier1_01_Tile_Array);
                }
                else if (NameIs_DesertRoad_Tier1_01(currentTile.name))
                {
                    // Update Adj - Desert Road 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.DesertRoad_Tier1, DesertRoad_Tier1_01_Tile_Array);
                }
                else if (NameIs_SwampRoad_Tier1_01(currentTile.name))
                {
                    // Update Adj - Grass Road 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.SwampRoad_Tier1, SwampRoad_Tier1_01_Tile_Array);
                }
                else if (NameIs_TundraRoad_Tier1_01(currentTile.name))
                {
                    // Update Adj - Tundra Road 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.TundraRoad_Tier1, TundraRoad_Tier1_01_Tile_Array);
                }
                else if (NameIs_SwampRiver_Tier1_01(currentTile.name))
                {
                    // Update Adj - Swamp River 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.SwampRiver, SwampRiver_01_Tile_Array);
                }
                else if (NameIs_LavaRiver_01(currentTile.name))
                {
                    // Update Adj - Lava River 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.LavaRiver, LavaRiver_01_Tile_Array);
                }
                else if (NameIs_BlightedRiver_01(currentTile.name))
                {
                    // Update Adj - Blighted River 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.BlightedRiver, BlightedRiver_01_Tile_Array);
                }
                else if (NameIs_SmallMagmaRiver_01(currentTile.name))
                {
                    // Update Adj - Small Magma River 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.SmallMagmaRiver, SmallMagmaRiver_01_Tile_Array);
                }
                else if (NameIs_TundraOcean_Tier1_01(currentTile.name))
                {
                    // Update Adj - Tundra Ocean 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.TundraOcean, TundraOcean_01_Tile_Array);
                }
                else if (NameIs_GrassOcean_Tier1_01(currentTile.name))
                {
                    // Update Adj - Grass Ocean 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.GrassOcean, GrassOcean_01_Tile_Array);
                }
                else if (NameIs_SwampOcean_Tier1_01(currentTile.name))
                {
                    // Update Adj - Swamp Ocean 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.SwampOcean, SwampOcean_01_Tile_Array);
                }
                else if (NameIs_DesertOcean_Tier1_01(currentTile.name))
                {
                    // Update Adj - Desert Ocean 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.DesertOcean, DesertOcean_01_Tile_Array);
                }
                else if (NameIs_VolcanicOcean_Tier1_01(currentTile.name))
                {
                    // Update Adj - Volcanic Ocean 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.VolcanicOcean, VolcanicOcean_01_Tile_Array);
                }
                else if (NameIs_BlightedOcean_Tier1_01(currentTile.name))
                {
                    // Update Adj - Blighted Ocean 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.BlightedOcean, BlightedOcean_01_Tile_Array);
                }
                else if (NameIs_MetalWorldOcean_Tier1_01(currentTile.name))
                {
                    // Update Adj - Metal World Ocean 01
                    Process_Adjacent_4Directions_01(tilemapParent, cellToSet,
                        Direction4_TilesetTypes.MetalWorldOcean, MetalWorldOcean_01_Tile_Array);
                }
            }
        }
        // Tile Is A Rock
        public bool TileNameIsRock(string tileNameToCheck)
        {
            for (int i = 0; i < Tiles_Rocks_Set02.Length; i++)
            {
                if (tileNameToCheck == Tiles_Rocks_Set02[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Tile Is Buildable Ground
        public bool NameIs_BuildableTile_01(string tileNameToCheck)
        {
            for (int i = 0; i < Tiles_Sand.Length; i++)
            {
                if (tileNameToCheck == Tiles_Sand[i].name)
                {
                    return true;
                }
            }
            for (int i = 0; i < Tiles_Dirt.Length; i++)
            {
                if (tileNameToCheck == Tiles_Dirt[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Light Shallow River 01 - Tiles
        public bool NameIs_LightShallowRiver_01(string tileNameToCheck)
        {
            for (int i = 0; i < LightShallowRiver_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == LightShallowRiver_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Lava River 01 - Tiles
        public bool NameIs_LavaRiver_01(string tileNameToCheck)
        {
            for (int i = 0; i < LavaRiver_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == LavaRiver_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Blighted River 01 - Tiles
        public bool NameIs_BlightedRiver_01(string tileNameToCheck)
        {
            for (int i = 0; i < BlightedRiver_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == BlightedRiver_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Small Magma River 01 - Tiles
        public bool NameIs_SmallMagmaRiver_01(string tileNameToCheck)
        {
            for (int i = 0; i < SmallMagmaRiver_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == SmallMagmaRiver_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Sand Lake 01 - Tiles
        public bool NameIs_SandLake_01(string tileNameToCheck)
        {
            for (int i = 0; i < SandLake_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == SandLake_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Sand Road - Tier1 - 01 - Tiles
        public bool NameIs_SandRoad_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < SandRoad_Tier1_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == SandRoad_Tier1_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Cement Road - Tier1 - 01 - Tiles
        public bool NameIs_CementRoad_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < CementRoad_Tier1_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == CementRoad_Tier1_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Grass Road - Tier1 - 01 - Tiles
        public bool NameIs_GrassRoad_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < GrassRoad_Tier1_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == GrassRoad_Tier1_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Desert Road - Tier1 - 01 - Tiles
        public bool NameIs_DesertRoad_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < DesertRoad_Tier1_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == DesertRoad_Tier1_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Swamp Road - Tier1 - 01 - Tiles
        public bool NameIs_SwampRoad_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < SwampRoad_Tier1_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == SwampRoad_Tier1_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Tundra Road - Tier1 - 01 - Tiles
        public bool NameIs_TundraRoad_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < TundraRoad_Tier1_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == TundraRoad_Tier1_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Swamp River - 01 - Tiles
        public bool NameIs_SwampRiver_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < SwampRiver_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == SwampRiver_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Tundra Ocean - 01 - Tiles
        public bool NameIs_TundraOcean_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < TundraOcean_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == TundraOcean_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Grass Ocean - 01 - Tiles
        public bool NameIs_GrassOcean_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < GrassOcean_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == GrassOcean_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Swamp Ocean - 01 - Tiles
        public bool NameIs_SwampOcean_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < SwampOcean_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == SwampOcean_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Desert Ocean - 01 - Tiles
        public bool NameIs_DesertOcean_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < DesertOcean_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == DesertOcean_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Volcanic Ocean - 01 - Tiles
        public bool NameIs_VolcanicOcean_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < VolcanicOcean_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == VolcanicOcean_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Blighted Ocean - 01 - Tiles
        public bool NameIs_BlightedOcean_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < BlightedOcean_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == BlightedOcean_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        // Metal World Ocean - 01 - Tiles
        public bool NameIs_MetalWorldOcean_Tier1_01(string tileNameToCheck)
        {
            for (int i = 0; i < MetalWorldOcean_01_Tile_Array.Length; i++)
            {
                if (tileNameToCheck == MetalWorldOcean_01_Tile_Array[i].name)
                {
                    return true;
                }
            }
            return false;
        }
        private void Process_Adjacent_4Directions_01(Tilemap tilemapParent,
            Vector3Int cellToSet,
            Direction4_TilesetTypes direction4TilesetType,
            TileBase[] tileArrayToUse)
        {
            // Process Adjacent Tiles and Update
            int w = cellToSet.x;
            int h = cellToSet.y;

            // Do Processing
            Vector3Int cell = new Vector3Int(w, h, 0);

            Vector3Int cell_Top = new Vector3Int(w, h + 1, 0);
            bool hasTop = false;
            Vector3Int cell_Bottom = new Vector3Int(w, h - 1, 0);
            bool hasBottom = false;
            Vector3Int cell_Left = new Vector3Int(w - 1, h, 0);
            bool hasLeft = false;
            Vector3Int cell_Right = new Vector3Int(w + 1, h, 0);
            bool hasRight = false;

            TileBase currentTile = tilemapParent.GetTile(cell);
            TileBase tile_Top = tilemapParent.GetTile(cell_Top);
            TileBase tile_Bottom = tilemapParent.GetTile(cell_Bottom);
            TileBase tile_Left = tilemapParent.GetTile(cell_Left);
            TileBase tile_Right = tilemapParent.GetTile(cell_Right);

            if (currentTile != null)
            {
                if (direction4TilesetType == Direction4_TilesetTypes.LightShallowRiver)
                {
                    if (tile_Top != null)
                        if (NameIs_LightShallowRiver_01(tile_Top.name))
                            hasTop = true;
                    if (tile_Bottom != null)
                        if (NameIs_LightShallowRiver_01(tile_Bottom.name))
                            hasBottom = true;
                    if (tile_Left != null)
                        if (NameIs_LightShallowRiver_01(tile_Left.name))
                            hasLeft = true;
                    if (tile_Right != null)
                        if (NameIs_LightShallowRiver_01(tile_Right.name))
                            hasRight = true;
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.SandLake)
                {
                    if (tile_Top != null)
                    {
                        if (NameIs_SandLake_01(tile_Top.name))
                            hasTop = true;
                    }
                    else
                    {
                        hasTop = true;
                    }
                    if (tile_Bottom != null)
                    {
                        if (NameIs_SandLake_01(tile_Bottom.name))
                            hasBottom = true;
                    }
                    else
                    {
                        hasBottom = true;
                    }
                    if (tile_Left != null)
                    {
                        if (NameIs_SandLake_01(tile_Left.name))
                            hasLeft = true;
                    }
                    else
                    {
                        hasLeft = true;
                    }
                    if (tile_Right != null)
                    {
                        if (NameIs_SandLake_01(tile_Right.name))
                            hasRight = true;
                    }
                    else
                    {
                        hasRight = true;
                    }
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.SandRoad_Tier1)
                {
                    if (tile_Top != null)
                        if (NameIs_SandRoad_Tier1_01(tile_Top.name))
                            hasTop = true;
                    if (tile_Bottom != null)
                        if (NameIs_SandRoad_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    if (tile_Left != null)
                        if (NameIs_SandRoad_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    if (tile_Right != null)
                        if (NameIs_SandRoad_Tier1_01(tile_Right.name))
                            hasRight = true;
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.CementRoad_Tier1)
                {
                    if (tile_Top != null)
                        if (NameIs_CementRoad_Tier1_01(tile_Top.name))
                            hasTop = true;
                    if (tile_Bottom != null)
                        if (NameIs_CementRoad_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    if (tile_Left != null)
                        if (NameIs_CementRoad_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    if (tile_Right != null)
                        if (NameIs_CementRoad_Tier1_01(tile_Right.name))
                            hasRight = true;
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.GrassRoad_Tier1)
                {
                    if (tile_Top != null)
                        if (NameIs_GrassRoad_Tier1_01(tile_Top.name))
                            hasTop = true;
                    if (tile_Bottom != null)
                        if (NameIs_GrassRoad_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    if (tile_Left != null)
                        if (NameIs_GrassRoad_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    if (tile_Right != null)
                        if (NameIs_GrassRoad_Tier1_01(tile_Right.name))
                            hasRight = true;
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.DesertRoad_Tier1)
                {
                    if (tile_Top != null)
                        if (NameIs_DesertRoad_Tier1_01(tile_Top.name))
                            hasTop = true;
                    if (tile_Bottom != null)
                        if (NameIs_DesertRoad_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    if (tile_Left != null)
                        if (NameIs_DesertRoad_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    if (tile_Right != null)
                        if (NameIs_DesertRoad_Tier1_01(tile_Right.name))
                            hasRight = true;
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.SwampRoad_Tier1)
                {
                    if (tile_Top != null)
                        if (NameIs_SwampRoad_Tier1_01(tile_Top.name))
                            hasTop = true;
                    if (tile_Bottom != null)
                        if (NameIs_SwampRoad_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    if (tile_Left != null)
                        if (NameIs_SwampRoad_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    if (tile_Right != null)
                        if (NameIs_SwampRoad_Tier1_01(tile_Right.name))
                            hasRight = true;
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.TundraRoad_Tier1)
                {
                    if (tile_Top != null)
                        if (NameIs_TundraRoad_Tier1_01(tile_Top.name))
                            hasTop = true;
                    if (tile_Bottom != null)
                        if (NameIs_TundraRoad_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    if (tile_Left != null)
                        if (NameIs_TundraRoad_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    if (tile_Right != null)
                        if (NameIs_TundraRoad_Tier1_01(tile_Right.name))
                            hasRight = true;
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.SwampRiver)
                {
                    if (tile_Top != null)
                        if (NameIs_SwampRiver_Tier1_01(tile_Top.name))
                            hasTop = true;
                    if (tile_Bottom != null)
                        if (NameIs_SwampRiver_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    if (tile_Left != null)
                        if (NameIs_SwampRiver_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    if (tile_Right != null)
                        if (NameIs_SwampRiver_Tier1_01(tile_Right.name))
                            hasRight = true;
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.LavaRiver)
                {
                    if (tile_Top != null)
                        if (NameIs_LavaRiver_01(tile_Top.name))
                            hasTop = true;
                    if (tile_Bottom != null)
                        if (NameIs_LavaRiver_01(tile_Bottom.name))
                            hasBottom = true;
                    if (tile_Left != null)
                        if (NameIs_LavaRiver_01(tile_Left.name))
                            hasLeft = true;
                    if (tile_Right != null)
                        if (NameIs_LavaRiver_01(tile_Right.name))
                            hasRight = true;
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.BlightedRiver)
                {
                    if (tile_Top != null)
                        if (NameIs_BlightedRiver_01(tile_Top.name))
                            hasTop = true;
                    if (tile_Bottom != null)
                        if (NameIs_BlightedRiver_01(tile_Bottom.name))
                            hasBottom = true;
                    if (tile_Left != null)
                        if (NameIs_BlightedRiver_01(tile_Left.name))
                            hasLeft = true;
                    if (tile_Right != null)
                        if (NameIs_BlightedRiver_01(tile_Right.name))
                            hasRight = true;
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.SmallMagmaRiver)
                {
                    if (tile_Top != null)
                        if (NameIs_SmallMagmaRiver_01(tile_Top.name))
                            hasTop = true;
                    if (tile_Bottom != null)
                        if (NameIs_SmallMagmaRiver_01(tile_Bottom.name))
                            hasBottom = true;
                    if (tile_Left != null)
                        if (NameIs_SmallMagmaRiver_01(tile_Left.name))
                            hasLeft = true;
                    if (tile_Right != null)
                        if (NameIs_SmallMagmaRiver_01(tile_Right.name))
                            hasRight = true;
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.TundraOcean)
                {
                    if (tile_Top != null)
                    {
                        if (NameIs_TundraOcean_Tier1_01(tile_Top.name))
                            hasTop = true;
                    }
                    else
                    {
                        hasTop = true;
                    }
                    if (tile_Bottom != null)
                    {
                        if (NameIs_TundraOcean_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    }
                    else
                    {
                        hasBottom = true;
                    }
                    if (tile_Left != null)
                    {
                        if (NameIs_TundraOcean_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    }
                    else
                    {
                        hasLeft = true;
                    }
                    if (tile_Right != null)
                    {
                        if (NameIs_TundraOcean_Tier1_01(tile_Right.name))
                            hasRight = true;
                    }
                    else
                    {
                        hasRight = true;
                    }
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.GrassOcean)
                {
                    if (tile_Top != null)
                    {
                        if (NameIs_GrassOcean_Tier1_01(tile_Top.name))
                            hasTop = true;
                    }
                    else
                    {
                        hasTop = true;
                    }
                    if (tile_Bottom != null)
                    {
                        if (NameIs_GrassOcean_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    }
                    else
                    {
                        hasBottom = true;
                    }
                    if (tile_Left != null)
                    {
                        if (NameIs_GrassOcean_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    }
                    else
                    {
                        hasLeft = true;
                    }
                    if (tile_Right != null)
                    {
                        if (NameIs_GrassOcean_Tier1_01(tile_Right.name))
                            hasRight = true;
                    }
                    else
                    {
                        hasRight = true;
                    }
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.SwampOcean)
                {
                    if (tile_Top != null)
                    {
                        if (NameIs_SwampOcean_Tier1_01(tile_Top.name))
                            hasTop = true;
                    }
                    else
                    {
                        hasTop = true;
                    }
                    if (tile_Bottom != null)
                    {
                        if (NameIs_SwampOcean_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    }
                    else
                    {
                        hasBottom = true;
                    }
                    if (tile_Left != null)
                    {
                        if (NameIs_SwampOcean_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    }
                    else
                    {
                        hasLeft = true;
                    }
                    if (tile_Right != null)
                    {
                        if (NameIs_SwampOcean_Tier1_01(tile_Right.name))
                            hasRight = true;
                    }
                    else
                    {
                        hasRight = true;
                    }
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.DesertOcean)
                {
                    if (tile_Top != null)
                    {
                        if (NameIs_DesertOcean_Tier1_01(tile_Top.name))
                            hasTop = true;
                    }
                    else
                    {
                        hasTop = true;
                    }
                    if (tile_Bottom != null)
                    {
                        if (NameIs_DesertOcean_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    }
                    else
                    {
                        hasBottom = true;
                    }
                    if (tile_Left != null)
                    {
                        if (NameIs_DesertOcean_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    }
                    else
                    {
                        hasLeft = true;
                    }
                    if (tile_Right != null)
                    {
                        if (NameIs_DesertOcean_Tier1_01(tile_Right.name))
                            hasRight = true;
                    }
                    else
                    {
                        hasRight = true;
                    }
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.VolcanicOcean)
                {
                    if (tile_Top != null)
                    {
                        if (NameIs_VolcanicOcean_Tier1_01(tile_Top.name))
                            hasTop = true;
                    }
                    else
                    {
                        hasTop = true;
                    }
                    if (tile_Bottom != null)
                    {
                        if (NameIs_VolcanicOcean_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    }
                    else
                    {
                        hasBottom = true;
                    }
                    if (tile_Left != null)
                    {
                        if (NameIs_VolcanicOcean_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    }
                    else
                    {
                        hasLeft = true;
                    }
                    if (tile_Right != null)
                    {
                        if (NameIs_VolcanicOcean_Tier1_01(tile_Right.name))
                            hasRight = true;
                    }
                    else
                    {
                        hasRight = true;
                    }
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.BlightedOcean)
                {
                    if (tile_Top != null)
                    {
                        if (NameIs_BlightedOcean_Tier1_01(tile_Top.name))
                            hasTop = true;
                    }
                    else
                    {
                        hasTop = true;
                    }
                    if (tile_Bottom != null)
                    {
                        if (NameIs_BlightedOcean_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    }
                    else
                    {
                        hasBottom = true;
                    }
                    if (tile_Left != null)
                    {
                        if (NameIs_BlightedOcean_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    }
                    else
                    {
                        hasLeft = true;
                    }
                    if (tile_Right != null)
                    {
                        if (NameIs_BlightedOcean_Tier1_01(tile_Right.name))
                            hasRight = true;
                    }
                    else
                    {
                        hasRight = true;
                    }
                }
                else if (direction4TilesetType == Direction4_TilesetTypes.MetalWorldOcean)
                {
                    if (tile_Top != null)
                    {
                        if (NameIs_MetalWorldOcean_Tier1_01(tile_Top.name))
                            hasTop = true;
                    }
                    else
                    {
                        hasTop = true;
                    }
                    if (tile_Bottom != null)
                    {
                        if (NameIs_MetalWorldOcean_Tier1_01(tile_Bottom.name))
                            hasBottom = true;
                    }
                    else
                    {
                        hasBottom = true;
                    }
                    if (tile_Left != null)
                    {
                        if (NameIs_MetalWorldOcean_Tier1_01(tile_Left.name))
                            hasLeft = true;
                    }
                    else
                    {
                        hasLeft = true;
                    }
                    if (tile_Right != null)
                    {
                        if (NameIs_MetalWorldOcean_Tier1_01(tile_Right.name))
                            hasRight = true;
                    }
                    else
                    {
                        hasRight = true;
                    }
                }

                if (!hasTop && !hasLeft && !hasRight && !hasBottom)
                {
                    // None
                    tilemapParent.SetTile(cell, tileArrayToUse[0]);
                }
                else if (hasTop && hasLeft && hasRight && hasBottom)
                {
                    // TLRB
                    tilemapParent.SetTile(cell, tileArrayToUse[8]);
                }
                else if (hasTop && hasLeft && hasRight && !hasBottom)
                {
                    // TLR
                    tilemapParent.SetTile(cell, tileArrayToUse[12]);
                }
                else if (!hasTop && hasLeft && hasRight && hasBottom)
                {
                    // LRB
                    tilemapParent.SetTile(cell, tileArrayToUse[10]);
                }
                else if (hasTop && hasLeft && !hasRight && hasBottom)
                {
                    // TLB
                    tilemapParent.SetTile(cell, tileArrayToUse[9]);
                }
                else if (hasTop && !hasLeft && hasRight && hasBottom)
                {
                    // TRB
                    tilemapParent.SetTile(cell, tileArrayToUse[11]);
                }
                else if (!hasTop && hasLeft && !hasRight && hasBottom)
                {
                    // LB
                    tilemapParent.SetTile(cell, tileArrayToUse[13]);
                }
                else if (!hasTop && hasLeft && hasRight && !hasBottom)
                {
                    // LR
                    tilemapParent.SetTile(cell, tileArrayToUse[5]);
                }
                else if (!hasTop && !hasLeft && hasRight && hasBottom)
                {
                    // RB
                    tilemapParent.SetTile(cell, tileArrayToUse[14]);
                }
                else if (hasTop && !hasLeft && !hasRight && hasBottom)
                {
                    // TB
                    tilemapParent.SetTile(cell, tileArrayToUse[6]);
                }
                else if (hasTop && hasLeft && !hasRight && !hasBottom)
                {
                    // TL
                    tilemapParent.SetTile(cell, tileArrayToUse[15]);
                }
                else if (hasTop && !hasLeft && hasRight && !hasBottom)
                {
                    // TR
                    tilemapParent.SetTile(cell, tileArrayToUse[7]);
                }
                else if (!hasTop && !hasLeft && !hasRight && hasBottom)
                {
                    // B
                    tilemapParent.SetTile(cell, tileArrayToUse[2]);
                }
                else if (!hasTop && hasLeft && !hasRight && !hasBottom)
                {
                    // L
                    tilemapParent.SetTile(cell, tileArrayToUse[1]);
                }
                else if (!hasTop && !hasLeft && hasRight && !hasBottom)
                {
                    // R
                    tilemapParent.SetTile(cell, tileArrayToUse[4]);
                }
                else if (hasTop && !hasLeft && !hasRight && !hasBottom)
                {
                    // T
                    tilemapParent.SetTile(cell, tileArrayToUse[3]);
                }
            }
        }

        public bool WorldGenComplete = false;
        public List<isoTileUniverse_RiverAndRoadBot> RiverAndRoad_Bots;
        public bool Process_RiverAndRoadBot_Activities = false;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
                Regenerate_IsoMap = true;
            if (Regenerate_IsoMap)
            {
                Generate_TileSet_Sample();
                Regenerate_IsoMap = false;
            }

            // Process Biome Rivers After All Complete
            if (!Display_Tileset)
            {
                if (WorldGenComplete)
                {
                    if (Process_RiverAndRoadBot_Activities)
                    {
                        bool hasAliveBot = false;
                        if (RiverAndRoad_Bots.Count > 0)
                        {
                            for (int i = 0; i < RiverAndRoad_Bots.Count; i++)
                            {
                                if (!RiverAndRoad_Bots[i].BotDead)
                                    hasAliveBot = true;
                            }
                        }
                        if (!hasAliveBot)
                        {
                            Vector3Int currentCell = new Vector3Int(0, 0, 0);
                            Vector3Int mapStartingPos = new Vector3Int(-(IsoMapSize_Width / 2), -(IsoMapSize_Height / 2), 0);
                            // Process Adj Tiles
                            for (int w = mapStartingPos.x; w < mapStartingPos.x + IsoMapSize_Width; w++)
                            {
                                for (int h = mapStartingPos.x; h < mapStartingPos.y + IsoMapSize_Height; h++)
                                {
                                    // Set Map Tile
                                    currentCell = new Vector3Int(w, h, 0);
                                    ProcessAdjacentTiles(Tilemap_ToGenerateOn, currentCell);
                                }
                            }
                            Process_RiverAndRoadBot_Activities = false;
                        }
                    }
                }
            }
        }
    }

}