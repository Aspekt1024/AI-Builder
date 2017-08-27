using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberComponent : MonoBehaviour, IGrabber {

    private enum State
    {
        None, HoldingObject, GrabbingObject, DroppingObject
    }
    private State state;

    private Transform objectHoldTransform;
    private IGrabbable grabbedObject;

    private void Update()
    {
        switch(state)
        {
            case State.None:
                break;
            case State.HoldingObject:
                grabbedObject.SetPosition(objectHoldTransform.position);
                break;
            case State.GrabbingObject:
                state = State.HoldingObject;
                break;
            case State.DroppingObject:
                state = State.None;
                break;

        }
    }

    public void SetObjectHoldPosition(Transform holdTransform)
    {
        objectHoldTransform = holdTransform;
    }
    
    public bool GrabObject(IGrabbable obj)
    {
        state = State.HoldingObject;
        grabbedObject = obj;
        obj.PickedUp(this);
        return true;
    }

    public bool GrabObjectAtPosition(Vector3 position)
    {
        if (state == State.HoldingObject) return false;

        Ray ray = new Ray()
        {
            origin = position,
            direction = Vector3.down
        };
        RaycastHit hit = new RaycastHit();
        LayerMask layers = 1 << LayerMask.NameToLayer("Collectible");

        if (Physics.Raycast(ray, out hit, 10f, layers))
        {
            SelectableObject obj = hit.collider.gameObject.GetComponent<SelectableObject>();
            if (obj == null) return false;

            if (obj.IsType<Collectible>())
            {
                grabbedObject = (IGrabbable)obj;
                ((IGrabbable)obj).PickedUp(this);
                state = State.HoldingObject;
                return true;
            }
        }
        return false;
    }

    public bool DetachGrabbedObject()
    {
        state = State.None;
        grabbedObject = null;
        return true;
    }

    public void ReleaseObjectToPosition(Vector3 position)
    {
        state = State.None;
        grabbedObject.SetPosition(position);
        grabbedObject.PutDown();
    }

    public void RotateHeldObject(Vector3 eulerRotation)
    {
        grabbedObject.RotateObject(eulerRotation);
    }

    public Collectible GetHeldCollectible()
    {
        if (grabbedObject.GetType().Equals(typeof(Collectible)) || grabbedObject.GetType().IsSubclassOf(typeof(Collectible)))
            return (Collectible)grabbedObject;

        return null;
    }

    public bool IsHoldingObject() { return state == State.HoldingObject; }
}
