using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public LevelGrid Grid
    {
        get { return grid; }
    }

    private LevelGrid grid;
    //private int levelNum;
    
	private void Awake ()
    {
        //levelNum = 1;
        LoadGrid();
    }
    
    private void LoadGrid()
    {
        grid = new LevelGrid();
        grid.Init(levelCenterPosition:Vector3.zero);
    }

    public void RemoveObjectFromTile(SelectableObject obj)
    {
        CellIndex tile = GetTileIndex(obj.transform.position);
        grid.RemoveObjectFromCell(obj, tile);
    }

    public void AddObjectToTile(SelectableObject obj)
    {
        CellIndex tile = GetTileIndex(obj.transform.position);
        grid.AddObjectToTile(obj, tile);
    }

    public void ShowAllTiles()
    {
        grid.ShowAllCells();
    }

    public void ShowTiles(List<CellIndex> tiles)
    {
        foreach(CellIndex tile in tiles)
        {
            grid.ShowCell(tile);
        }
    }

    public void HideTiles(List<CellIndex> tiles)
    {
        foreach(CellIndex tile in tiles)
        {
            grid.HideCell(tile);
        }
    }

    private CellIndex GetTileIndex(Vector3 position)
    {
        return GetTileIndex(new Vector2(position.x, position.z));
    }

    private CellIndex GetTileIndex(Vector2 position)
    {
        return grid.GetCellIndex(position);
    }
}
