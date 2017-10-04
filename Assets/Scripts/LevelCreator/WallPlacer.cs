using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacer : MonoBehaviour {

    private GameObject Wall0;
    private GameObject Wall1;
    private GameObject Wall2Corner;
    private GameObject Wall2Straight;
    private GameObject Wall3;
    private GameObject Wall4;

    private LevelGrid floor;

    private void Awake()
    {
        floor = FindObjectOfType<Level>().Grid;
        Wall0 = Resources.Load<GameObject>("Prefabs/Walls/Wall_0");
        Wall1 = Resources.Load<GameObject>("Prefabs/Walls/Wall_1");
        Wall2Corner = Resources.Load<GameObject>("Prefabs/Walls/Wall_2_corner");
        Wall2Straight = Resources.Load<GameObject>("Prefabs/Walls/Wall_2_straight");
        Wall3 = Resources.Load<GameObject>("Prefabs/Walls/Wall_3");
        Wall4 = Resources.Load<GameObject>("Prefabs/Walls/Wall_4");
    }

    public void PlaceWall(Wall obj)
    {
        CellIndex tile = floor.GetCellIndex(obj.transform.position);

        bool northWall = floor.CellHasWall(new CellIndex(tile.Row + 1, tile.Col));
        bool southWall = floor.CellHasWall(new CellIndex(tile.Row - 1, tile.Col));
        bool eastWall = floor.CellHasWall(new CellIndex(tile.Row, tile.Col + 1));
        bool westWall = floor.CellHasWall(new CellIndex(tile.Row, tile.Col - 1));
        
        int numConnections = 0;
        if (northWall) numConnections++;
        if (southWall) numConnections++;
        if (eastWall) numConnections++;
        if (westWall) numConnections++;
        
        if (numConnections > 0)
        {
            CreateWall(obj, numConnections, northWall, southWall, eastWall, westWall);
            Destroy(obj.gameObject);
        }
        else
        {
            floor.AddObjectToTile(obj, tile);
        }

        if (northWall)
        {
            UpdateWall(new CellIndex(tile.Row + 1, tile.Col));
        }
        if (southWall)
        {
            UpdateWall(new CellIndex(tile.Row - 1, tile.Col));
        }

        if (eastWall)
        {
            UpdateWall(new CellIndex(tile.Row, tile.Col + 1));
        }

        if (westWall)
        {
            UpdateWall(new CellIndex(tile.Row, tile.Col - 1));
        }
    }

    private void UpdateWall(CellIndex tile)
    {
        Wall wall = floor.GetWallFromCell(tile);
        
        bool northWall = floor.CellHasWall(new CellIndex(tile.Row + 1, tile.Col));
        bool southWall = floor.CellHasWall(new CellIndex(tile.Row - 1, tile.Col));
        bool eastWall = floor.CellHasWall(new CellIndex(tile.Row, tile.Col + 1));
        bool westWall = floor.CellHasWall(new CellIndex(tile.Row, tile.Col - 1));

        int numConnections = 0;
        if (northWall) numConnections++;
        if (southWall) numConnections++;
        if (eastWall) numConnections++;
        if (westWall) numConnections++;

        CreateWall(wall, numConnections, northWall, southWall, eastWall, westWall);
        floor.RemoveObjectFromCell(wall, tile);
        Destroy(wall.gameObject);
    }

    private void CreateWall(Wall original, int numConnections, bool north, bool south, bool east, bool west)
    {
        Wall wall = null;
        switch(numConnections)
        {
            case 1:
                wall = Instantiate(Wall1).GetComponent<Wall>();
                RotateEndWall(wall, north, south, east, west);
                break;
            case 2:
                if ((north && south) || (east && west))
                {
                    wall = Instantiate(Wall2Straight).GetComponent<Wall>();
                    RotateStraightWall(wall, north, south, east, west);
                }
                else
                {
                    wall = Instantiate(Wall2Corner).GetComponent<Wall>();
                    RotateCornerWall(wall, north, south, east, west);
                }
                break;
            case 3:
                wall = Instantiate(Wall3).GetComponent<Wall>();
                RotateTWall(wall, north, south, east, west);
                break;
            case 4:
                wall = Instantiate(Wall4).GetComponent<Wall>();
                break;
        }

        wall.transform.position = original.transform.position;
        wall.transform.SetParent(original.transform.parent);

        floor.AddObjectToCell(wall, wall.transform.position);
    }

    private void RotateTWall(Wall wall, bool north, bool south, bool east, bool west)
    {
        // T walls are initially aligned to connect with south, east and west walls
        if (!south)
        {
            wall.transform.Rotate(new Vector3(0f, 180f, 0f));
        }
        else if (!east)
        {
            wall.transform.Rotate(new Vector3(0f, 90f, 0f));
        }
        else if (!west)
        {
            wall.transform.Rotate(new Vector3(0f, -90f, 0f));
        }
    }

    private void RotateCornerWall(Wall wall, bool north, bool south, bool east, bool west)
    {
        // Corner walls are initially aligned to connect with north and west
        if (north && east)
        {
            wall.transform.Rotate(new Vector3(0f, 90f, 0f));
        }
        else if (south && west)
        {
            wall.transform.Rotate(new Vector3(0f, -90f, 0f));
        }
        else if (south && east)
        {
            wall.transform.Rotate(new Vector3(0f, 180f, 0f));
        }
    }

    private void RotateStraightWall(Wall wall, bool north, bool south, bool east, bool west)
    {
        // Straight walls are initially aligned east to west
        if (north && south)
        {
            wall.transform.Rotate(new Vector3(0f, 90f, 0f));
        }
    }

    private void RotateEndWall(Wall wall, bool north, bool south, bool east, bool west)
    {
        // End walls are initially aligned to connect with west
        if (north)
        {
            wall.transform.Rotate(new Vector3(0f, 90f, 0f));
        }
        else if (south)
        {
            wall.transform.Rotate(new Vector3(0f, -90f, 0f));
        }
        else if (east)
        {
            wall.transform.Rotate(new Vector3(0f, 180f, 0f));
        }
    }
}
