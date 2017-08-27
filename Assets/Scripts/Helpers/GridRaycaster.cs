using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridRaycaster {

    public static bool CheckForObject(Vector3 position, LayerMask layers)
    {
        return GetObject(position, layers) != null;
    }

    public static GameObject GetObject(Vector3 position, LayerMask layers)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray()
        {
            origin = new Vector3(position.x, 5f, position.z),
            direction = Vector3.down
        };

        if (Physics.Raycast(ray, out hit, 10f, layers))
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
