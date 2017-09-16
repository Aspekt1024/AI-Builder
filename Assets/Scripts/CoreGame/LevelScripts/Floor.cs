using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor {

    private Tile[,] tiles;
    
    public Floor()
    {
        LoadTiles();
    }

    private void LoadTiles()
    {
        GameObject tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        tiles = new Tile[LevelGrid.ROOM_ROWS, LevelGrid.ROOM_COLS];
        for (int r = 0; r < LevelGrid.ROOM_ROWS; r++)
        {
            for (int c = 0; c < LevelGrid.ROOM_COLS; c++)
            {
                tiles[r, c] = Object.Instantiate(tilePrefab).GetComponent<Tile>();
            }
        }
    }

    public void HideTile(TileIndex index)
    {
        if (index.Row < 0 || index.Col < 0 || index.Row >= LevelGrid.ROOM_ROWS || index.Col >= LevelGrid.ROOM_COLS) return;
        tiles[index.Row, index.Col].Hide();
    }

    public void ShowTile(TileIndex index)
    {
        if (index.Row < 0 || index.Col < 0 || index.Row >= LevelGrid.ROOM_ROWS || index.Col >= LevelGrid.ROOM_COLS) return;
        tiles[index.Row, index.Col].Show();
    }

    public void RemoveObjectFromTile(SelectableObject obj, TileIndex tile)
    {
        tiles[tile.Row, tile.Col].RemoveInhabitingObject(obj);
    }

    public void AddObjectToTile(SelectableObject obj, TileIndex tile)
    {
        tiles[tile.Row, tile.Col].AddInhabitingObject(obj);
    }

    public void SetupFloorAtPosition(Vector3 position)
    {
        float xDiff = (LevelGrid.ROOM_COLS / 2f - 0.5f) * LevelGrid.TILE_SIZE;
        float zDiff = (LevelGrid.ROOM_ROWS / 2f - 0.5f) * LevelGrid.TILE_SIZE;
        position -= new Vector3(xDiff, 0f, zDiff);
        for (int r = 0; r < LevelGrid.ROOM_ROWS; r++)
        {
            for (int c = 0; c < LevelGrid.ROOM_COLS; c++)
            {
                tiles[r, c].transform.position = position + new Vector3(c * LevelGrid.TILE_SIZE, 0f, r * LevelGrid.TILE_SIZE);
                if (c == 7 && r == 8)
                {
                    GameObject go = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Selectables/Buildings/PowerPad"));
                    go.transform.position = tiles[r, c].transform.position;
                    tiles[r, c].AddInhabitingObject(go.GetComponent<SelectableObject>());
                }
                if (c == 8 && r == 5)
                {
                    GameObject go = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Selectables/Collectibles/ResourceCube"));
                    go.transform.position = tiles[r, c].transform.position;
                    tiles[r, c].AddInhabitingObject(go.GetComponent<SelectableObject>());
                }
            }
        }
    }

    public static TileIndex GetTileIndex(Vector3 position)
    {
        return GetTileIndex(new Vector2(position.x, position.z));
    }

    public static TileIndex GetTileIndex(Vector2 position)
    {
        // Position is relative to the center of the room. Convert to bottom left
        float xDiff = (LevelGrid.ROOM_COLS / 2f - 0.5f) * LevelGrid.TILE_SIZE;
        float zDiff = (LevelGrid.ROOM_ROWS / 2f - 0.5f) * LevelGrid.TILE_SIZE;
        position += new Vector2(xDiff, zDiff);

        int col = Mathf.RoundToInt(position.x / LevelGrid.TILE_SIZE);
        int row = Mathf.RoundToInt(position.y / LevelGrid.TILE_SIZE);

        if (col > LevelGrid.ROOM_COLS || col < 0) col = -1;
        if (row > LevelGrid.ROOM_ROWS || row < 0) row = -1;
        
        return new TileIndex()
        {
            Row = row,
            Col = col
        };
    }
}

public struct TileIndex
{
    public int Row;
    public int Col;

    public TileIndex(int row, int col)
    {
        Row = row;
        Col = col;
    }
}
