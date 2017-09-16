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
                tiles[r, c].gameObject.SetActive(false);
            }
        }
    }

    public void HideTile(int row, int col)
    {
        tiles[row, col].Hide();
    }

    public void ShowTile(TileIndex index)
    {
        tiles[index.Row, index.Col].Show();
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
                //tiles[r, c].Show();
            }
        }
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
}
