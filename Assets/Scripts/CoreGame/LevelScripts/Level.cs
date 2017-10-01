using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    
    private Floor floor;

    private int levelNum;
    
	private void Awake ()
    {
        levelNum = 1;
        LoadFloor();
    }
    
    private void LoadFloor()
    {
        floor = new Floor();
        floor.SetupFloorAtPosition(Vector3.zero);
    }

    public void RemoveObjectFromTile(SelectableObject obj)
    {
        TileIndex tile = GetTileIndex(obj.transform.position);
        floor.RemoveObjectFromTile(obj, tile);
    }

    public Floor GetFloor()
    {
        return floor;
    }

    public void AddObjectToTile(SelectableObject obj)
    {
        TileIndex tile = GetTileIndex(obj.transform.position);
        floor.AddObjectToTile(obj, tile);
    }

    public void ShowAllTiles()
    {
        floor.ShowAll();
    }

    public void ShowTiles(List<TileIndex> tiles)
    {
        foreach(TileIndex tile in tiles)
        {
            floor.ShowTile(tile);
        }
    }

    public void HideTiles(List<TileIndex> tiles)
    {
        foreach(TileIndex tile in tiles)
        {
            floor.HideTile(tile);
        }
    }

    private TileIndex GetTileIndex(Vector3 position)
    {
        return GetTileIndex(new Vector2(position.x, position.z));
    }

    private TileIndex GetTileIndex(Vector2 position)
    {
        return Floor.GetTileIndex(position);
    }
}
