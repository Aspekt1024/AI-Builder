using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable {

    void PickedUp(IGrabber grabber);
    void PutDown();
    void Consumed();
}
