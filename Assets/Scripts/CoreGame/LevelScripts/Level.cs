using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    private static Level levelScript;
    private Floor floor;

    private int levelNum;
    private Vector3 levelCenter;
    


	private void Awake () {

        if (levelScript == null)
        {
            levelScript = this;
        }
        else
        {
            Debug.LogError("Multiple instance of Level (script) found in scene.");
        }

        levelNum = 1;
        levelCenter = LevelGrid.GetLevelPos(levelNum);
        LoadFloor();
    }
    
    private void LoadFloor()
    {
        floor = new Floor();
        floor.SetupFloorAtPosition(Vector3.zero);
    }

    public static void RemoveObjectFromTile(SelectableObject obj)
    {
        TileIndex tile = GetTileIndex(obj.transform.position);
        levelScript.floor.RemoveObjectFromTile(obj, tile);
    }

    public static void AddObjectToTile(SelectableObject obj)
    {
        TileIndex tile = GetTileIndex(obj.transform.position);
        levelScript.floor.AddObjectToTile(obj, tile);
    }

    public static void ShowTiles(List<TileIndex> tiles)
    {
        foreach(TileIndex tile in tiles)
        {
            levelScript.floor.ShowTile(tile);
        }
    }

    public static void HideTiles(List<TileIndex> tiles)
    {
        foreach(TileIndex tile in tiles)
        {
            levelScript.floor.HideTile(tile);
        }
    }

    private static TileIndex GetTileIndex(Vector3 position)
    {
        return GetTileIndex(new Vector2(position.x, position.z));
    }

    private static TileIndex GetTileIndex(Vector2 position)
    {
        position -= new Vector2(levelScript.levelCenter.x, levelScript.levelCenter.z);
        return Floor.GetTileIndex(position);
    }
}
