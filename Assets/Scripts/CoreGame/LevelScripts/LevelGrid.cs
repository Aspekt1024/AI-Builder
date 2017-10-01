using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelGrid {

    public const int LEVEL_ROWS = 7;
    public const int LEVEL_COLS = 7;
    public const int ROOM_ROWS = 11;
    public const int ROOM_COLS = 11;
    public const float TILE_SIZE = 2f;
    
    public static Vector3 GetLevelPos(int level)
    {
        int row = Mathf.FloorToInt((level - 1) / LEVEL_COLS);
        int col = (level - 1 - row * LEVEL_COLS);

        return new Vector3(row * ROOM_ROWS * TILE_SIZE, 0f, col * ROOM_COLS * TILE_SIZE);
    }

    public static Vector3 GetSnappedPosition(Vector3 pos)
    {
        float snappedXPos = Mathf.Round(pos.x / TILE_SIZE) * TILE_SIZE;
        float snappedYPos = pos.y;
        float snappedZPos = Mathf.Round(pos.z / TILE_SIZE) * TILE_SIZE;

        return new Vector3(snappedXPos, snappedYPos, snappedZPos);
    }

}