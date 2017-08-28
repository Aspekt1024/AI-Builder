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
        state = States.HoldingObject;
        grabbedObject = obj;
        obj.PickedUp(this);
        gameObject.GetComponent<ICanGrab>().FinishedAction();
        return true;
    }

    public bool GrabObjectAtPosition(Vector3 position)
    {
        Debug.Log("grabbing");
        gameObject.GetComponent<ICanGrab>().FinishedAction();

        if (state == States.HoldingObject) return false;

        Ray ray = new Ray()
        {
            origin = position,
            direction = Vector3.down
        };
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 10f, Layers.COLLECTIBLE))
        {
            SelectableObject obj = hit.collider.gameObject.GetComponent<SelectableObject>();
            if (obj == null) return false;

            if (obj.IsType<Collectible>())
            {
                grabbedObject = (IGrabbable)obj;
                ((IGrabbable)obj).PickedUp(this);
                state = States.HoldingObject;
                return true;
            }
        }
        return false;
    }

    public bool DetachGrabbedObject()
    {
        state = States.None;
        grabbedObject = null;
        return true;
    }

    public bool ReleaseObjectToPosition(Vector3 position)
    {
        gameObject.GetComponent<ICanGrab>().FinishedAction();

        if (grabbedObject.CheckForValidDrop(position))
        {
            state = States.None;
            grabbedObject.SetPosition(position);
            grabbedObject.PutDown();
            grabbedObject = null;
            return true;
        }
        else
        {
            return false;
        }
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
