using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPlacer {

    private PlaceableBehaviour currentObj;
    private Level levelScript;

    public ObjectPlacer()
    {
        levelScript = UnityEngine.Object.FindObjectOfType<Level>();
    }

    public void Update()
    {
        if (currentObj == null) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Terrain"));

        if (hit.collider != null)
        {
            Vector3 snappedPosition = GridProperties.GetSnappedPosition(hit.point);
            currentObj.Object.transform.position = snappedPosition;
        }
    }

    public void SetCurrentObject(PlaceableBehaviour obj)
    {
        currentObj = obj;
    }
    
    public void ClearCurrentObject()
    {
        currentObj = null;
    }


    public void PlaceObjectAndRetainSelection(Vector2 position)
    {
        if (currentObj == null) return;

        PlaceableBehaviour originalObject = currentObj;
        bool success = PlaceObject(position);
        
        if (success)
        {
            currentObj = (PlaceableBehaviour)originalObject.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
            currentObj.Object.name = originalObject.Object.name;
            currentObj.Object.transform.SetParent(originalObject.Object.transform.parent);
        }
    }
    
    public bool PlaceObject(Vector2 position)
    {
        if (currentObj == null) return false;

        CellIndex tile = levelScript.Grid.GetCellIndex(currentObj.Object.transform.position);
        if (levelScript.Grid.CellIsEmpty(tile))
        {
            if (currentObj.IsType<Wall>())
            {
                WallPlacer wallPlacer = new GameObject().AddComponent<WallPlacer>();
                wallPlacer.PlaceWall((Wall)currentObj);
            }
            else
            {
                levelScript.Grid.AddObjectToCell(currentObj, currentObj.Object.transform.position);
            }

            currentObj = null;
            return true;
        }
        else
        {
            Debug.Log("Cannot place there!");
            return false;
        }
    }
}
