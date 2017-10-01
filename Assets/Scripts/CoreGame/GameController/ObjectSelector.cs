using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour {

    private SelectableObject selectedObject;

    private SelectionIndicator selectionIndicator;
    
    private void Start()
    {
        selectionIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/SelectionIndicator")).GetComponent<SelectionIndicator>();
    }

    public SelectableObject GetSelectedObject()
    {
        return selectedObject;
    }

    public void SelectSingleObject(Vector2 position)
    {
        selectedObject = GetObjectByRaycast(position);

        if (selectedObject == null)
        {
            selectionIndicator.DeselectObject();
            GameUI.DeselectObject();
        }
        else
        {
            selectionIndicator.SelectObject(selectedObject);
            GameUI.SetSelectedObject(selectedObject);
        }
    }

    public void GetMouseOver(Vector2 position)
    {
        SelectableObject obj = GetObjectByRaycast(position);
        if (obj == null || obj == selectedObject)
        {
            GameUI.NoMouseOver();
        }
        else
        {
            GameUI.MouseOver(obj);
        }
    }

    private SelectableObject GetObjectByRaycast(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, Layers.SELECTABLE_OBJECT))
        {
            return hit.collider.GetComponentInParent<SelectableObject>();
        }
        else
        {
            return null;
        }
    }
}
