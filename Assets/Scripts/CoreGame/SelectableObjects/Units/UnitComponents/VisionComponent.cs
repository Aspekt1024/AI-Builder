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

    private void Awake()
    {
        visibleTiles = new List<TileIndex>();

        hasSelfVision = true;
        hasForwardVision = true;
        hasVisionMemory = false;
    }

    public bool Look()
    {
        GetVisibleTiles();
        Level.ShowTiles(tilesToShow);
        Level.HideTiles(tilesToHide);
        visibleTiles = tilesToShow;
        return true;
    }

    private void GetVisibleTiles()
    {
        TileIndex currentTile = Floor.GetTileIndex(transform.position);
        tilesToShow = new List<TileIndex>();

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
}
