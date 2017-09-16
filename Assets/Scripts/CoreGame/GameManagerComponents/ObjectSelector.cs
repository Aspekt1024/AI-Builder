using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour {

    public SelectableObject SelectedObject;

    private SelectionIndicator selectionIndicator;

    private static ObjectSelector objectSelector;
    public static ObjectSelector Instance
    {
        get
        {
            if (objectSelector == null)
            {
                objectSelector = FindObjectOfType<ObjectSelector>();
                if (objectSelector == null)
                {
                    Debug.LogError("Unable to find SelectionProperties class - add one to the scene");
                }
            }
            return objectSelector;
        }
    }

    private void Start()
    {
        selectionIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/SelectionIndicator")).GetComponent<SelectionIndicator>();
    }

    public static SelectableObject GetSelectedObject()
    {
        return Instance.SelectedObject;
    }

    public static void SelectSingleObject(Vector2 position)
    {
        Instance.SelectedObject = GetObjectByRaycast(position);

        if (Instance.SelectedObject == null)
        {
            Instance.selectionIndicator.DeselectObject();
            GameUI.DeselectObject();
        }
        else
        {
            Instance.selectionIndicator.SelectObject(Instance.SelectedObject);
            GameUI.SetSelectedObject(Instance.SelectedObject);
        }
    }

    public static void GetMouseOver(Vector2 position)
    {
        SelectableObject obj = GetObjectByRaycast(position);
        if (obj == null || obj == Instance.SelectedObject)
        {
            GameUI.NoMouseOver();
        }
        else
        {
            GameUI.MouseOver(obj);
        }
    }

    private static SelectableObject GetObjectByRaycast(Vector2 position)
    {
        Camera camera = GameController.Instance.MainCamera;
        Ray ray = camera.ScreenPointToRay(position);
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
