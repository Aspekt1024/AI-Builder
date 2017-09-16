using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionComponent : MonoBehaviour, IVision {
    
    private List<TileIndex> visibleTiles;
    private List<TileIndex> tilesToShow;
    private List<TileIndex> tilesToHide;

    private bool hasSelfVision;
    private bool hasCrossVision;
    private bool hasForwardVision;
    private bool hasLineOfSightVision;
    private bool hasTotalVision;

    private bool hasVisionMemory;

    private void Start()
    {
        visibleTiles = new List<TileIndex>();

        hasSelfVision = true;
        hasCrossVision = true;
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
        if (hasCrossVision)
        {
            tilesToShow.Add(new TileIndex(currentTile.Row - 1, currentTile.Col));
            tilesToShow.Add(new TileIndex(currentTile.Row + 1, currentTile.Col));
            tilesToShow.Add(new TileIndex(currentTile.Row, currentTile.Col - 1));
            tilesToShow.Add(new TileIndex(currentTile.Row, currentTile.Col + 1));
        }

        tilesToHide = new List<TileIndex>();
        foreach(TileIndex tile in visibleTiles)
        {
            if (!tilesToShow.Contains(tile))
            {
                tilesToHide.Add(tile);
            }
        }
    }
}
