using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabber {

    bool GrabObject(IGrabbable obj);
    bool GrabObjectAtPosition(Vector3 position);
    bool DetachGrabbedObject();
    bool ReleaseObjectToPosition(Vector3 position);
    void SetObjectHoldPosition(Transform objectHoldTransform);
    void RotateHeldObject(Vector3 eulerRotation);
    Collectible GetHeldCollectible();
}
