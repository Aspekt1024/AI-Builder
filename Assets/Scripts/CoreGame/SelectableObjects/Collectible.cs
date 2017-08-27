using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : SelectableObject, IGrabbable {

    private IGrabber currentHolder;

    protected enum GrabState
    {
        None, PickedUp, PutDown, Consumed
    }
    protected GrabState grabState;

    private void Start ()
    {
        GetComponents();
    }
    
    public void PickedUp(IGrabber grabber)
    {
        if (currentHolder != null)
        {
            currentHolder.DetachGrabbedObject();
        }
        currentHolder = grabber;
        grabState = GrabState.PickedUp;
    }

    public void PutDown()
    {
        grabState = GrabState.PutDown;
        currentHolder = null;
        OnPlaced();
    }

    public void Consumed()
    {
        grabState = GrabState.Consumed;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void RotateObject(Vector3 eulerRotation)
    {
        transform.Rotate(eulerRotation);
    }

    protected virtual void OnPlaced() { }
}
