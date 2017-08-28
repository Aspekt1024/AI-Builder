using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Direction = MoveComponent.Direction;

public class MovementButtons : MonoBehaviour {

    public void LeftButton()
    {
        Move(Direction.W);
    }

    public void RightButton()
    {
        Move(Direction.E);
    }

    public void UpButton()
    {
        Move(Direction.N);
    }

    public void DownButton()
    {
        Move(Direction.S);
    }

    public void GrabObject()
    {
        if (ObjectSelector.GetSelectedObject().IsType<Drone>())
        {
            ((Drone)ObjectSelector.GetSelectedObject()).GrabObject();
        }
    }

    public void ReleaseObject()
    {
        if (ObjectSelector.GetSelectedObject().IsType<Drone>())
        {
            ((Drone)ObjectSelector.GetSelectedObject()).ReleaseObject();
        }
    }

    private void Move(Direction direction)
    {
        if (ObjectSelector.GetSelectedObject() == null) return;

        if (ObjectSelector.GetSelectedObject().IsType<Unit>())
        {
            ((Unit)ObjectSelector.GetSelectedObject()).MoveForward();
        }
    }
}
