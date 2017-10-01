using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer {

    private PlaceableObject currentObject;

    public ObjectPlacer() { }

    public void Update()
    {
        if (currentObject == null) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Terrain"));

        if (hit.collider != null)
        {
            Vector3 snappedPosition = LevelGrid.GetSnappedPosition(hit.point);
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

}
