using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace isoTile_Universe
{
    public class isoTileUniverse_RiverAndRoadBot : MonoBehaviour
    {

        /// <summary>
        /// Generates A River Or A Road randomly across the tilemap
        /// </summary>

        public bool BotActive = false;
        public bool BotDead = false;
        public Tilemap TilemapToRunOn;
        public Tile[] BaseTiles_ToTravelOn;
        public List<TileBase> BaseTiles_ToTravelOn_List;
        public Tile TileToPlace;
        public Vector3Int CurrentCell;
        public int CurrentLifePoint = 0;
        public int LifeCycles = 12;

        // Start is called before the first frame update
        public void Initialize_Bot(Tilemap tilemapToEffectIn, Tile[] baseTileToEndOnIn, Tile tileToPlaceAlongTheWayIn, Vector3Int startingCellIn)
        {
            CurrentCell = startingCellIn;
            TilemapToRunOn = tilemapToEffectIn;
            TileToPlace = tileToPlaceAlongTheWayIn;
            BaseTiles_ToTravelOn = baseTileToEndOnIn;
            BaseTiles_ToTravelOn_List = new List<TileBase>();
            for (int i = 0; i < BaseTiles_ToTravelOn.Length; i++)
            {
                BaseTiles_ToTravelOn_List.Add(BaseTiles_ToTravelOn[i]);
            }
            CurrentLifePoint = 0;
            LifeCycles = Random.Range(6, 14);
            BotActive = true;
            BotDead = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (CurrentLifePoint < LifeCycles)
            {
                // Do Bot Functionality
                if (!BaseTiles_ToTravelOn_List.Contains(TilemapToRunOn.GetTile(CurrentCell)))
                {
                    // Last Tile Found
                    CurrentLifePoint = LifeCycles;
                }
                else
                {
                    TilemapToRunOn.SetTile(CurrentCell, TileToPlace);
                    int randomDirectionToTravel = Random.Range(0, 4);
                    if (randomDirectionToTravel == 0)
                        CurrentCell.x += 1;
                    else if (randomDirectionToTravel == 1)
                        CurrentCell.x -= 1;
                    else if (randomDirectionToTravel == 2)
                        CurrentCell.y += 1;
                    else if (randomDirectionToTravel == 3)
                        CurrentCell.y -= 1;
                    else
                        CurrentCell.x += 1;
                    CurrentLifePoint++;
                }
            }
            else
            {
                // We are Dead
                BotDead = true;
            }
        }
    }
}
