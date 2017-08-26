using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabber {

    void GrabObject(IGrabbable grabbableObject);
    void ReleaseObject();
    void SetObjectHoldPosition(Transform objectHoldTransform);
    Vector3 GetObjectHoldPosition();
}
