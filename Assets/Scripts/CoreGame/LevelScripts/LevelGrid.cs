using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelGrid {
    
    [SerializeField]
    private Cell[,] cells;

    private Vector3 floorPositionBottomLeft;
    
    public LevelGrid() { }

    public void Init(Vector3 levelCenterPosition)
    {
        float xDiff = (GridProperties.ROOM_COLS / 2f - 0.5f) * GridProperties.TILE_SIZE;
        float zDiff = (GridProperties.ROOM_ROWS / 2f - 0.5f) * GridProperties.TILE_SIZE;
        floorPositionBottomLeft = levelCenterPosition -new Vector3(xDiff, 0f, zDiff);

        LoadCells();
    }

    private void LoadCells()
    {
        cells = new Cell[GridProperties.ROOM_ROWS, GridProperties.ROOM_COLS];
        
        for (int r = 0; r < GridProperties.ROOM_ROWS; r++)
        {
            for (int c = 0; c < GridProperties.ROOM_COLS; c++)
            {
                cells[r, c] = new Cell();
                cells[r, c].Init(r, c, this);
            }
        }
    }

    public Vector3 GetFloorPosition()
    {
        return floorPositionBottomLeft;
    }

    public void HideCell(CellIndex index)
    {
        if (index.Row < 0 || index.Col < 0 || index.Row >= GridProperties.ROOM_ROWS || index.Col >= GridProperties.ROOM_COLS) return;
        cells[index.Row, index.Col].Hide();
    }

    public void ShowCell(CellIndex index)
    {
        if (index.Row < 0 || index.Col < 0 || index.Row >= GridProperties.ROOM_ROWS || index.Col >= GridProperties.ROOM_COLS) return;
        cells[index.Row, index.Col].Show();
    }

    public Cell GetCell(int row, int col)
    {
        return cells[row, col];
    }

    public void ShowAllCells()
    {
        foreach (Cell tile in cells)
        {
            tile.Show();
        }
    }

    public void RemoveObjectFromCell(PlaceableObject obj, CellIndex tile)
    {
        cells[tile.Row, tile.Col].RemoveInhabitingObject(obj);
    }

    public void AddObjectToCell(PlaceableObject obj, Vector3 position)
    {
        AddObjectToTile(obj, GetCellIndex(position));
    }

    public void AddObjectToTile(PlaceableObject obj, CellIndex tile)
    {
        cells[tile.Row, tile.Col].AddInhabitingObject(obj);
    }

    public bool CellIsEmpty(CellIndex tile)
    {
        return cells[tile.Row, tile.Col].IsEmpty();
    }

    public bool CellHasWall(CellIndex tile)
    {
        bool hasWall = false;
        if ((tile.Row >= 0 && tile.Row < GridProperties.ROOM_ROWS) && (tile.Col >= 0 && tile.Col < GridProperties.ROOM_COLS))
        {
            hasWall = (cells[tile.Row, tile.Col].HasWall());
        }
        return hasWall;
    }

    public Wall GetWallFromCell(CellIndex tile)
    {
        return cells[tile.Row, tile.Col].GetWall();
    }

    public CellIndex GetCellIndex(Vector3 position)
    {
        return GetCellIndex(new Vector2(position.x, position.z));
    }

    public CellIndex GetCellIndex(Vector2 position)
    {
        // Position is relative to the center of the room. Convert to bottom left
        float xDiff = (GridProperties.ROOM_COLS / 2f - 0.5f) * GridProperties.TILE_SIZE;
        float zDiff = (GridProperties.ROOM_ROWS / 2f - 0.5f) * GridProperties.TILE_SIZE;
        position += new Vector2(xDiff, zDiff);

        int col = Mathf.RoundToInt(position.x / GridProperties.TILE_SIZE);
        int row = Mathf.RoundToInt(position.y / GridProperties.TILE_SIZE);

        if (col > GridProperties.ROOM_COLS || col < 0) col = -1;
        if (row > GridProperties.ROOM_ROWS || row < 0) row = -1;
        
        return new CellIndex()
        {
            Row = row,
            Col = col
        };
    }
}

public struct CellIndex
{
    public int Row;
    public int Col;

    public CellIndex(int row, int col)
    {
        Row = row;
        Col = col;
    }
}
