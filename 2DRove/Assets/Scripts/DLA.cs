using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLAMapGen : MonoBehaviour
{
    enum State{
        wall,
        floor,
        startingRoom
    }
    [SerializeField] private Vector2 mapSize;
    [SerializeField] private Vector2 startingRoomSize;
    [SerializeField] private Vector2 tileSize;
    [SerializeField] private GameObject startingRoomPrefab;
    [SerializeField] private GameObject wallTilePrefab;
    [SerializeField] private GameObject floorTilePrefab;
    [SerializeField] private float percentToFillMap  = 40f;
    private int mapSizeX;
    private int mapSizeY;
    private int currentTilesFilled;
    private void Start(){
        mapSizeX = (int) mapSize.x;
        mapSizeY = (int) mapSize.y;
        State[] mapData = new State[mapSizeX * mapSizeY];
        int tilesToFill = (int) (mapData.Length * (percentToFillMap / 100));

        spawnStartingRoom(mapData);

        while(currentTilesFilled < tilesToFill){
            spawnWalker(mapSizeX, mapSizeY, mapData);
        }

        generateMap(mapData);
    }

    private void generateMap(State[] data){
        for(int x = 0; x < mapSizeX; x++){
            for(int y = 0; y < mapSizeY; y++){
                if(data[coordinatesToArrayIndex(x, y)] == State.wall){
                    spawnTile(wallTilePrefab, x, y);
                }else if(data[coordinatesToArrayIndex(x, y)] == State.floor){
                    spawnTile(floorTilePrefab, x, y);
                }
            }
        }
    }

    private void spawnStartingRoom(State[] data){
        int xMid = mapSizeX / 2;
        int yMid = mapSizeY / 2;
        int startingXMid = ((int) startingRoomSize.x / 2);
        int startingYMid = ((int) startingRoomSize.y / 2);
        
        GameObject.Instantiate(startingRoomPrefab, (Vector2)transform.position + alignStartingRoom((float)xMid, (float)yMid) * tileSize, Quaternion.identity);

        int tempX = 0;
        int tempY = 0;

        if(isOdd(startingRoomSize.x)){
            tempX ++;
        }
        if(isOdd(startingRoomSize.y)){
            tempY ++;
        }

        for(int x = xMid - startingXMid; x < xMid + startingXMid + tempX; x++){
            for(int y = yMid - startingYMid; y < yMid + startingYMid + tempY; y++){
                data[coordinatesToArrayIndex(x, y)] = State.startingRoom;
                currentTilesFilled ++;
            }
        }
    }

    private void spawnTile(GameObject tilePrefab, int xId, int yId){
        GameObject.Instantiate(tilePrefab, (Vector2)transform.position + getRoomPosition(xId, yId), Quaternion.identity);
    }

    private Vector2 getRoomPosition(int xId, int yId){
        return new Vector2(xId, yId) * tileSize;
    }

    private Vector2 alignStartingRoom(float x, float y){
        if(isEven(startingRoomSize.x)){
            x -= 0.5f;
        }
        if(isEven(startingRoomSize.y)){
            y -= 0.5f;
        }
        return new Vector2(x, y);
    }

    private bool isEven(float a){
        if(a % 2 == 0){
            return true;
        }
        return false;
    }

    private bool isOdd(float a){
        return !isEven(a);
    }

    private int coordinatesToArrayIndex(int x, int y){
        return x * mapSizeX + y;
    }

    private void spawnWalker(int xSize, int ySize, State[] map){
        Vector2 currentPos = getWalkerStartingPos(xSize, ySize, map);
        int x = (int)currentPos.x;
        int y = (int)currentPos.y;
        while(!doesWalkerStop(x, y, map)){
            int currentDirection = Random.Range(0,4);
            while(!isInBounds(getIndexOfNeighbor(x, y, currentDirection), map)){
                currentDirection = Random.Range(0,4);
            }
            currentPos = moveWalker(x, y, currentDirection);
            x = (int)currentPos.x;
            y = (int)currentPos.y;
        }
    }

    private Vector2 getWalkerStartingPos(int xSize, int ySize, State[] map){
        int currentX = Random.Range(0, xSize);
        int currentY = Random.Range(0, ySize);

        if(map[coordinatesToArrayIndex(currentX, currentY)] == State.wall){
            return new Vector2(currentX, currentY);
        }
        return getWalkerStartingPos(xSize, ySize, map);
    }

    private bool doesWalkerStop(int x, int y, State[] map){
        for(int direction = 0; direction < 4; direction ++){
            if(!isTileWall(getIndexOfNeighbor(x, y, direction), map)){
                map[coordinatesToArrayIndex(x, y)] = State.floor;
                currentTilesFilled ++;
                return true;
            }
        }
        return false;
    }

    private bool isTileWall(int index, State[] map){
        if(isInBounds(index, map) && map[index] == State.wall){
            return true;
        }
        if(!isInBounds(index, map)){
            return true;
        }
        return false;
    }

    private bool isInBounds(int indexToCheck, State[] map){
        if(0 <= indexToCheck && indexToCheck < map.Length){
            return true;
        }
        return false;
    }

    private Vector2 moveWalker(int x, int y, int direction){
        switch(direction){
            case 0:
                return new Vector2(x, y + 1);

            case 1:
                return new Vector2(x, y - 1);

            case 2:
                return new Vector2(x + 1, y);

            case 3:
                return new Vector2(x - 1, y);
        }
        return new Vector2(x, y);
    }

    private int getIndexOfNeighbor(int x, int y, int direction){ //For direction, 0-3 NSEW
        switch(direction){
            case 0:
                return coordinatesToArrayIndex(x, y + 1);

            case 1:
                return coordinatesToArrayIndex(x, y - 1);

            case 2:
                return coordinatesToArrayIndex(x + 1, y);

            case 3:
                return coordinatesToArrayIndex(x - 1, y);
        }
        return -1;
    }
}