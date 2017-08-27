using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable {

    void PickedUp();
    void PutDown();
    void Consumed();
    void SetPosition(Vector3 position);
}
