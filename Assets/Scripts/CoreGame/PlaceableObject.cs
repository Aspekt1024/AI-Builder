using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour {
    
    private enum States
    {
        None, Placed, Held
    }

    private States state;

    public void SetHeld()
    {
        state = States.Held;
    }

    public void SetPlaced()
    {
        state = States.Placed;
    }
}
