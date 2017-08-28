using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable {

    void PickedUp(IHolder grabber);
    void PutDown();
    void SetPosition(Vector3 position);
    void RotateObject(Vector3 eulerRotation);
    bool CheckForValidDrop(Vector3 position);
}
