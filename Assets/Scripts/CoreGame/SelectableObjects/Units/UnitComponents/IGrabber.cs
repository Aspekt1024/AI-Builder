using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabber {

    bool GrabObjectAtPosition(Vector3 position);
    void ReleaseObjectToPosition(Vector3 position);
    void SetObjectHoldPosition(Transform objectHoldTransform);
    void RotateHeldObject(Vector3 eulerRotation);
    Collectible GetHeldCollectible();
}
