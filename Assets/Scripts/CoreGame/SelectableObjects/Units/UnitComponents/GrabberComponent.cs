using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberComponent : MonoBehaviour, IGrabber {

    private enum States
    {
        None, HoldingObject, GrabbingObject, DroppingObject
    }
    private States state;

    private Transform objectHoldTransform;
    private IGrabbable grabbedObject;

    private void Update()
    {
        switch(state)
        {
            case States.None:
                break;
            case States.HoldingObject:
                grabbedObject.SetPosition(objectHoldTransform.position);
                break;
            case States.GrabbingObject:
                state = States.HoldingObject;
                break;
            case States.DroppingObject:
                state = States.None;
                break;

        }
    }

    public void SetObjectHoldPosition(Transform holdTransform)
    {
        objectHoldTransform = holdTransform;
    }
    
    public bool GrabObject(IGrabbable obj)
    {
        bool grabSuccessful = true;
        if (obj == null)
        {
            // TODO failure reason, no object to grab
            grabSuccessful = false;
        }
        else
        {
            grabbedObject = obj;
            obj.PickedUp(this);
            state = States.HoldingObject;
        }
        gameObject.GetComponent<ICanGrab>().FinishedAction(grabSuccessful);
        return grabSuccessful;
    }

    public bool GrabObjectAtPosition(Vector3 position)
    {
        bool grabSuccessful = true;

        if (state == States.HoldingObject)
        {
            gameObject.GetComponent<ICanGrab>().FinishedAction(false);
            // TODO failure reason: already holding object
            return false;
        }

        grabbedObject = GetGrabbableObjectAtPoint(position);
        
        if (grabbedObject == null)
        {
            // TODO failure reason, no object to grab
            grabSuccessful = false;
        }
        else
        {
            grabbedObject.PickedUp(this);
            state = States.HoldingObject;
        }

        gameObject.GetComponent<ICanGrab>().FinishedAction(grabSuccessful);
        return grabSuccessful;
    }

    private IGrabbable GetGrabbableObjectAtPoint(Vector3 position)
    {
        Ray ray = new Ray()
        {
            origin = position,
            direction = Vector3.down
        };
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 10f, Layers.COLLECTIBLE))
        {
            SelectableObject obj = hit.collider.gameObject.GetComponent<SelectableObject>();
            if (obj == null) return null;

            if (obj.IsType<IGrabbable>())
            {
                return (IGrabbable)obj;
            }
        }
        return null;
    }

    public bool DetachGrabbedObject()
    {
        state = States.None;
        grabbedObject = null;
        return true;
    }

    public bool ReleaseObjectToPosition(Vector3 position)
    {
        bool releaseSuccessful = true;

        if (grabbedObject.CheckForValidDrop(position))
        {
            state = States.None;
            grabbedObject.SetPosition(position);
            grabbedObject.PutDown();
            grabbedObject = null;
        }
        else
        {
            // TODO failure reason, could not place at specified location
            releaseSuccessful = false;
        }
        gameObject.GetComponent<ICanGrab>().FinishedAction(releaseSuccessful);
        return releaseSuccessful;
    }

    public Collectible GetHeldCollectible()
    {
        if (grabbedObject == null) return null;
        if (grabbedObject.GetType().Equals(typeof(Collectible)) || grabbedObject.GetType().IsSubclassOf(typeof(Collectible)))
            return (Collectible)grabbedObject;

        return null;
    }


    public bool IsHoldingObject() { return state == States.HoldingObject; }
}
