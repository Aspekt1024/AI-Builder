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
    }

    private void Start()
    {
        levelNum = 1;
        levelCenter = LevelGrid.GetLevelPos(levelNum);
        LoadFloor();
    }
    
    private void LoadFloor()
    {
        floor = new Floor();
        floor.SetupFloorAtPosition(Vector3.zero);
    }

    public static void ShowTilesAtPositions(Vector3[] positions)
    {
        foreach(Vector2 pos in positions)
        {
            TileIndex index = GetTileIndex(pos);
            levelScript.floor.ShowTile(index);
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
