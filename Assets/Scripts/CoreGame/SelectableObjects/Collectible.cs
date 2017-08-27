using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : SelectableObject, IGrabbable {

    protected enum GrabState
    {
        None, PickedUp, PutDown, Consumed
    }
    protected GrabState grabState;

    private void Start ()
    {
        GetComponents();
    }
    
    public void PickedUp()
    {
        grabState = GrabState.PickedUp;
    }

    public void PutDown()
    {
        grabState = GrabState.PutDown;
    }

    public void Consumed()
    {
        grabState = GrabState.Consumed;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
