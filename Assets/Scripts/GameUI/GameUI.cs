using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    // To be set in the editor/inspector
    public Text SelectedObjectText;
    public TooltipHandler TooltipHandler;
    public ActionButtons ActionButtons;

    private SelectableObject selectedObject;

    private enum States
    {
        None, SelectingObject, DeselectingObject
    }
    private States state;

    #region initialisation
    private static GameUI gameUI;
    private static GameUI Instance
    {
        get
        {
            if (gameUI == null)
            {
                Debug.LogError("Please add GameUI to the scene");
                return null;
            }
            return gameUI;
        }
    }

    private void Awake()
    {
        if (gameUI != null)
        {
            Debug.LogError("Multiple GameUI instances detected!");
        }
        else
        {
            gameUI = this;
        }
        state = States.DeselectingObject;
    }

    private void Update()
    {
        switch (state)
        {
            case States.None:
                break;
            case States.DeselectingObject:
                SetDefaultUI();
                break;
            case States.SelectingObject:
                SetObjectUI();
                break;
        }
    }
    #endregion

    private void SetObjectUI()
    {
        string objName = selectedObject.name;

        if (selectedObject.IsType<Unit>())
        {
            objName += " : Unit";
        }
        else if (selectedObject.IsType<Building>())
        {
            objName += " : Building";
        }
        else if (selectedObject.IsType<Collectible>())
        {
            objName += " : Collectible";
        }

        Instance.SelectedObjectText.text = objName;
    }

    private void SetDefaultUI()
    {
        Instance.SelectedObjectText.text = "";
        ActionButtons.NoSelection();
    }

    public static void SetSelectedObject(SelectableObject obj)
    {
        Instance.selectedObject = obj;
        Instance.state = States.SelectingObject;
        Instance.ActionButtons.ObjectSelected(obj);
    }

    public static void MouseOver(SelectableObject obj)
    {
        Instance.TooltipHandler.SetTextFromObject(obj);
    }

    public static void NoMouseOver()
    {
        Instance.TooltipHandler.RemoveTooltip();
    }

    public static void DeselectObject()
    {
        Instance.state = States.DeselectingObject;
        Instance.ActionButtons.NoSelection();
    }
}
