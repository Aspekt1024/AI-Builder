using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementDirection = MoveComponent.MovementDirection;

public class MovementButtons : MonoBehaviour {

    public void LeftButton()
    {
        Move(MovementDirection.W);
    }

    public void RightButton()
    {
        Move(MovementDirection.E);
    }

    public void UpButton()
    {
        Move(MovementDirection.N);
    }

    public void DownButton()
    {
        Move(MovementDirection.S);
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

    private void Move(MovementDirection direction)
    {
        if (ObjectSelector.GetSelectedObject() == null) return;

        if (ObjectSelector.GetSelectedObject().IsType<Unit>())
        {
            ((Unit)ObjectSelector.GetSelectedObject()).Move(direction);
        }
    }
}
