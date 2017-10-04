using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer {

    private PlaceableObject currentObject;
    private Level levelScript;

    public ObjectPlacer()
    {
        levelScript = Object.FindObjectOfType<Level>();
    }

    public void Update()
    {
        if (currentObject == null) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Terrain"));

        if (hit.collider != null)
        {
            Vector3 snappedPosition = GridProperties.GetSnappedPosition(hit.point);
            currentObject.transform.position = snappedPosition;
        }
    }

    public void SetCurrentObject(PlaceableObject obj)
    {
        currentObject = obj;
    }
    
    public void ClearCurrentObject()
    {
        currentObject = null;
    }


    public void PlaceObjectAndRetainSelection(Vector2 position)
    {
        if (currentObject == null) return;

        PlaceableObject originalObject = currentObject;
        bool success = PlaceObject(position);
        
        if (success)
        {
            currentObject = Object.Instantiate(originalObject);
            currentObject.name = originalObject.name;
            currentObject.transform.SetParent(originalObject.transform.parent);
        }
    }
    
    public bool PlaceObject(Vector2 position)
    {
        if (currentObject == null) return false;

        CellIndex tile = levelScript.Grid.GetCellIndex(currentObject.transform.position);
        if (levelScript.Grid.CellIsEmpty(tile))
        {
            if (currentObject.IsType<Wall>())
            {
                WallPlacer wallPlacer = new GameObject().AddComponent<WallPlacer>();
                wallPlacer.PlaceWall((Wall)currentObject);
            }
            else
            {
                levelScript.Grid.AddObjectToCell(currentObject, currentObject.transform.position);
            }

            currentObject = null;
            return true;
        }
        else
        {
            Debug.Log("Cannot place there!");
            return false;
        }
    }
}
