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
        Transform tilesParent = new GameObject("Tiles").transform;
        for (int r = 0; r < LevelGrid.ROOM_ROWS; r++)
        {
            for (int c = 0; c < LevelGrid.ROOM_COLS; c++)
            {
                tiles[r, c] = Object.Instantiate(tilePrefab).GetComponent<Tile>();
                tiles[r, c].transform.SetParent(tilesParent);
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

    public void ShowAll()
    {
        foreach (Tile tile in tiles)
        {
            tile.Show();
        }
    }

    public void RemoveObjectFromTile(PlaceableObject obj, TileIndex tile)
    {
        tiles[tile.Row, tile.Col].RemoveInhabitingObject(obj);
    }

    public void AddObjectToTile(PlaceableObject obj, Vector3 position)
    {
        AddObjectToTile(obj, GetTileIndex(position));
    }

    public void AddObjectToTile(PlaceableObject obj, TileIndex tile)
    {
        tiles[tile.Row, tile.Col].AddInhabitingObject(obj);
    }

    public bool TileIsEmpty(TileIndex tile)
    {
        return tiles[tile.Row, tile.Col].IsEmpty();
    }

    public bool TileHasWall(TileIndex tile)
    {
        bool hasWall = false;
        if ((tile.Row >= 0 && tile.Row < LevelGrid.ROOM_ROWS) && (tile.Col >= 0 && tile.Col < LevelGrid.ROOM_COLS))
        {
            hasWall = (tiles[tile.Row, tile.Col].HasWall());
        }
        return hasWall;
    }

    public Wall GetWallFromTile(TileIndex tile)
    {
        return tiles[tile.Row, tile.Col].GetWall();
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
                    tiles[r, c].AddInhabitingObject(go.GetComponent<PlaceableObject>());
                }
                if (c == 8 && r == 5)
                {
                    GameObject go = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Selectables/Collectibles/ResourceCube"));
                    go.transform.position = tiles[r, c].transform.position;
                    tiles[r, c].AddInhabitingObject(go.GetComponent<PlaceableObject>());
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
