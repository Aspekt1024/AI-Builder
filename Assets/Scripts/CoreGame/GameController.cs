using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Camera MainCamera;

    private enum States
    {
        None, Menu, Normal, Dialogue
    }
    private States state;

    #region initialisation
    private static GameController gameManager;
    public static GameController Instance
    {
        get
        {
            if (gameManager == null)
            {
                Debug.LogError("Please add GameManager to the scene");
                return null;
            }
            return gameManager;
        }
    }

    private void Awake()
    {
        if (gameManager != null)
        {
            Debug.LogError("Multiple GameManager instances detected!");
        }
        else
        {
            gameManager = this;
        }
        state = States.Normal;
    }

    private void Update()
    {

    }
    #endregion

    public static void LeftMouseDownReceived(Vector2 position)
    {
        if (Instance.state == States.Normal)
        {
            ObjectSelector.SelectSingleObject(position);
        }
    }

    public static void LeftMouseUpReceived(Vector2 position)
    {

    }

    public static void CheckMouseover(Vector2 position)
    {
        if (Instance.state == States.Normal)
        {
            ObjectSelector.GetMouseOver(position);
        }
    }
}
