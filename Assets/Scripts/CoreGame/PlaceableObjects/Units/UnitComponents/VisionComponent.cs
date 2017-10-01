using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionComponent : MonoBehaviour, IVision {
    
    private List<TileIndex> visibleTiles;
    private List<TileIndex> tilesToShow;
    private List<TileIndex> tilesToHide;

    private bool hasSelfVision;
    private bool hasForwardVision;
    private bool hasCrossVision;
    private bool hasForwardLineOfSightVision;
    private bool hasLineOfSightVision;
    private bool hasTotalVision;

    private bool hasVisionMemory;
    private bool levelScanned;

    private Level levelScript;

    private void Awake()
    {
        visibleTiles = new List<TileIndex>();

        hasSelfVision = true;
        hasForwardVision = true;
        hasVisionMemory = false;
        levelScanned = false;
        hasTotalVision = true;

        levelScript = FindObjectOfType<Level>();
    }

    public bool Look()
    {
        GetVisibleTiles();
        levelScript.ShowTiles(tilesToShow);
        levelScript.HideTiles(tilesToHide);
        visibleTiles = tilesToShow;
        return true;
    }

    private void GetVisibleTiles()
    {
        TileIndex currentTile = Floor.GetTileIndex(transform.position);
        tilesToShow = new List<TileIndex>();

        if (hasTotalVision)
        {
            if (levelScanned) return;

            ShowAllTiles();
            levelScanned = true;
            return;
        }

        if (hasSelfVision)
        {
            tilesToShow.Add(currentTile);
        }
        if (hasForwardVision)
        {
            tilesToShow.Add(Floor.GetTileIndex(transform.position + transform.forward * LevelGrid.TILE_SIZE));
        }
        if (hasCrossVision)
        {
            tilesToShow.Add(new TileIndex(currentTile.Row - 1, currentTile.Col));
            tilesToShow.Add(new TileIndex(currentTile.Row + 1, currentTile.Col));
            tilesToShow.Add(new TileIndex(currentTile.Row, currentTile.Col - 1));
            tilesToShow.Add(new TileIndex(currentTile.Row, currentTile.Col + 1));
        }

        tilesToHide = new List<TileIndex>();
        if (hasVisionMemory) return;

        foreach(TileIndex tile in visibleTiles)
        {
            if (!tilesToShow.Contains(tile))
            {
                tilesToHide.Add(tile);
            }
        }
    }

    private void ShowAllTiles()
    {
        for (int col = 0; col < LevelGrid.ROOM_COLS; col++)
        {
            for (int row = 0; row < LevelGrid.ROOM_ROWS; row++)
            {
                tilesToShow.Add(new TileIndex(row, col));
            }
        }
        tilesToHide = new List<TileIndex>();
    }
}
