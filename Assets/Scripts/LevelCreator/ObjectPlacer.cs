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
        Debug.Log(ray.origin + " " + ray.direction);

        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray, out hit, 1000000f, 1 << LayerMask.NameToLayer("Terrain"));


        Debug.Log(currentObject + " " + hit.point);
        currentObject.transform.position = hit.point;
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
