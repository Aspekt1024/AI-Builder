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


    public void SetObjectHoldPosition(Transform holdTransform)
    {
        Debug.Log(holdTransform);
        objectHoldTransform = holdTransform;
    }
    
    public void GrabObject(IGrabbable grabbableObject)
    {
        grabbableObject.PickedUp(this);
        grabbedObject = grabbableObject;
    }

    public void ReleaseObject()
    {
        grabbedObject.PutDown();
    }

    public Vector3 GetObjectHoldPosition()
    {
        return objectHoldTransform.position;
    }
}
