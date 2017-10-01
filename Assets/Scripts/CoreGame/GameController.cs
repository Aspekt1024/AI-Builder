using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameController : MonoBehaviour {

    public Camera MainCamera;

    private InputManager inputManager;

    protected enum States
    {
        None, Menu, Normal, Dialogue
    }
    protected States state;
    
    private void Awake()
    {
        state = States.Normal;
        inputManager = gameObject.AddComponent<InputManager>();
        inputManager.Init(this);
    }

    public abstract void LeftMouseDownReceived(Vector2 position);
    public abstract void LeftMouseUpReceived(Vector2 position);
    public abstract void LeftMouseUpWithShiftReceived(Vector2 position);

    public virtual void CheckMouseover(Vector2 position) { }
}
