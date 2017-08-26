using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : SelectableObject, IGrabbable {

    protected enum GrabState
    {
        None, PickedUp, PutDown, Consumed
    }
    protected GrabState grabState;
    protected IGrabber grabber;

    private void Start ()
    {
        GetComponents();
    }

    private void Update()
    {
        switch (grabState)
        {
            case GrabState.None:
                break;
            case GrabState.PickedUp:
                transform.position = grabber.GetObjectHoldPosition();
                break;
            case GrabState.PutDown:
                break;
            case GrabState.Consumed:
                // TODO destroy
                break;
        }
    }

    public void PickedUp(IGrabber grabbedBy)
    {
        grabState = GrabState.PickedUp;
        grabber = grabbedBy;
    }

    public void PutDown()
    {
        grabState = GrabState.PutDown;
    }

    public void Consumed()
    {
        grabState = GrabState.Consumed;
    }
}
