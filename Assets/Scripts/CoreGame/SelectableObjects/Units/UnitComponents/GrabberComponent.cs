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
        if (state == State.HoldingObject)
        {
            grabbedObject.SetPosition(objectHoldTransform.position);
        }
    }

    public void SetObjectHoldPosition(Transform holdTransform)
    {
        Debug.Log(holdTransform);
        objectHoldTransform = holdTransform;
    }
    
    public void GrabObject(IGrabbable grabbableObject)
    {
        state = State.HoldingObject;
        grabbableObject.PickedUp();
        grabbedObject = grabbableObject;
    }

    public void ReleaseObject()
    {
        state = State.None;
        grabbedObject.PutDown();
    }

    public Vector3 GetObjectHoldPosition()
    {
        return objectHoldTransform.position;
    }
}
