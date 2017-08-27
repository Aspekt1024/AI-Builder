using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtons : MonoBehaviour {

    public GameObject Buttons;

    public void ObjectSelected(SelectableObject selectedObject)
    {
        if (selectedObject.IsType<Drone>())
        {
            Buttons.SetActive(true);
        }
        else
        {
            NoSelection();  // TODO set up for other objects!
        }
    }

    public void NoSelection()
    {
        // TODO hide entire GameUI panel?
        Buttons.SetActive(false);
    }

}
