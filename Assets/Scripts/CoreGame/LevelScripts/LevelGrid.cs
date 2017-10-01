using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelGrid {

    public const int LEVEL_ROWS = 7;
    public const int LEVEL_COLS = 7;
    public const int ROOM_ROWS = 11;
    public const int ROOM_COLS = 11;
    public const float TILE_SIZE = 2f;
    
    public static Vector3 GetSnappedPosition(Vector3 pos)
    {
        float snappedXPos = Mathf.Clamp(pos.x, -TILE_SIZE * (ROOM_COLS - 1) / 2, TILE_SIZE * (ROOM_COLS - 1) / 2);
        float snappedZPos = Mathf.Clamp(pos.z, -TILE_SIZE * (ROOM_ROWS - 1) / 2, TILE_SIZE * (ROOM_ROWS - 1) / 2);
        float snappedYPos = pos.y;

        snappedXPos = Mathf.Round(snappedXPos / TILE_SIZE) * TILE_SIZE;
        snappedZPos = Mathf.Round(snappedZPos / TILE_SIZE) * TILE_SIZE;
    
        return new Vector3(snappedXPos, snappedYPos, snappedZPos);
    }

}