using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionProperties : MonoBehaviour {

    public SelectableObject SelectedObject;

    private void Start()
    {
        SelectedObject = FindObjectOfType<Drone>();
    }

    private static SelectionProperties selectionProperties;
    public static SelectionProperties Instance
    {
        get
        {
            if (selectionProperties == null)
            {
                selectionProperties = FindObjectOfType<SelectionProperties>();
                if (selectionProperties == null)
                {
                    Debug.LogError("Unable to find SelectionProperties class - add one to the scene");
                }
            }
            return selectionProperties;
        }
    }

    public static SelectableObject GetSelectedObject()
    {
        return Instance.SelectedObject;
    }
}
