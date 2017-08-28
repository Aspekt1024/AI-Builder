using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceptor : IHolder {

    bool ReceiveObject(IGrabbable grabbableObject);
    bool ReleaseObject();
    void SetReceptorPoint();
}
