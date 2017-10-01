using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanGrab : IUnitAttribute {

    bool GrabObject();
    bool ReleaseObject();
}
