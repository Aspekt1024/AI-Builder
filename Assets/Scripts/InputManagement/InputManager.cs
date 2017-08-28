using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the raw input into the game
/// </summary>
public class InputManager : MonoBehaviour {

    private enum States
    {
        None, Enabled, Disabled
    }
    private States state;

    public enum MouseEvents
    {
        LeftMouseUp, LeftMouseDown,
        RightMouseUp, RightMouseDown
    }

    //private bool mouseDownHeld;

    private const int LEFT_MOUSE_BUTTON = 0;
    private const int RIGHT_MOUSE_BUTTON = 1;
    private const int MIDDLE_MOUSE_BUTTON = 2;

    private void Start()
    {
        state = States.Enabled;
    }

    private void Update()
    {
        switch(state)
        {
            case States.None:
                break;
            case States.Enabled:
                ProcessInput();
                break;
            case States.Disabled:
                break;
        }
    }

    private void ProcessInput()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonUp(LEFT_MOUSE_BUTTON))
        {
            //mouseDownHeld = false;
            GameManager.LeftMouseUpReceived(Input.mousePosition);
        }
        else if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
        {
            //mouseDownHeld = true;
            GameManager.LeftMouseDownReceived(Input.mousePosition);
        }
        else
        {
            GameManager.CheckMouseover(Input.mousePosition);
        }
    }
}
