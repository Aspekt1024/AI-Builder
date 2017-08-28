using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabber : IHolder {

    bool GrabObject(IGrabbable obj);
    bool GrabObjectAtPosition(Vector3 position);
    bool ReleaseObjectToPosition(Vector3 position);
    void SetObjectHoldPosition(Transform objectHoldTransform);
    Collectible GetHeldCollectible();
}
