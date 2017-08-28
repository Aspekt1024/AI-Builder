using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Direction = MoveComponent.Direction;

public class MovementButtons : MonoBehaviour {

    public void LookLeft()
    {
        LookTowards(Direction.W);
    }

    public void LookRight()
    {
        LookTowards(Direction.E);
    }

    public void LookUp()
    {
        LookTowards(Direction.N);
    }

    public void LookDown()
    {
        LookTowards(Direction.S);
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

    private void LookTowards(Direction direction)
    {
        if (ObjectSelector.GetSelectedObject() == null) return;

        Debug.Log(ObjectSelector.GetSelectedObject());
        if (ObjectSelector.GetSelectedObject().IsType<IMoveable>())
        {
            ((IMoveable)ObjectSelector.GetSelectedObject()).TurnTowards(direction);
        }
    }

    public void MoveForward()
    {
        if (ObjectSelector.GetSelectedObject() == null) return;
        if (ObjectSelector.GetSelectedObject().IsType<IMoveable>())
        {
            ((IMoveable)ObjectSelector.GetSelectedObject()).MoveForward();
        }
    }

    public void MoveBackward()
    {
        if (ObjectSelector.GetSelectedObject() == null) return;
        if (ObjectSelector.GetSelectedObject().IsType<IMoveable>())
        {
            ((IMoveable)ObjectSelector.GetSelectedObject()).MoveBackward();
        }
    }
}
