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

    private void Move(MovementDirection direction)
    {
        if (SelectionProperties.GetSelectedObject().IsType<Unit>())
        {
            ((Unit)SelectionProperties.GetSelectedObject()).Move(direction);
        }
    }
}
