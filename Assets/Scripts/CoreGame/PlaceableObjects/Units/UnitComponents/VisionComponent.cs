using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionComponent : MonoBehaviour, IVision {
    
    private List<CellIndex> visibleTiles;
    private List<CellIndex> tilesToShow;
    private List<CellIndex> tilesToHide;

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
        visibleTiles = new List<CellIndex>();

        hasSelfVision = true;
        hasForwardVision = true;
        hasVisionMemory = false;
        hasCrossVision = false;
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
        CellIndex currentTile = levelScript.Grid.GetCellIndex(transform.position);
        tilesToShow = new List<CellIndex>();

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
            tilesToShow.Add(levelScript.Grid.GetCellIndex(transform.position + transform.forward * GridProperties.TILE_SIZE));
        }
        if (hasCrossVision)
        {
            tilesToShow.Add(new CellIndex(currentTile.Row - 1, currentTile.Col));
            tilesToShow.Add(new CellIndex(currentTile.Row + 1, currentTile.Col));
            tilesToShow.Add(new CellIndex(currentTile.Row, currentTile.Col - 1));
            tilesToShow.Add(new CellIndex(currentTile.Row, currentTile.Col + 1));
        }

        tilesToHide = new List<CellIndex>();
        if (hasVisionMemory) return;

        foreach(CellIndex tile in visibleTiles)
        {
            if (!tilesToShow.Contains(tile))
            {
                tilesToHide.Add(tile);
            }
        }
    }

    private void ShowAllTiles()
    {
        for (int col = 0; col < GridProperties.ROOM_COLS; col++)
        {
            for (int row = 0; row < GridProperties.ROOM_ROWS; row++)
            {
                tilesToShow.Add(new CellIndex(row, col));
            }
        }
        tilesToHide = new List<CellIndex>();
    }
}
