using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Direction = MoveComponent.Direction;

public class MovementButtons : MonoBehaviour {

    public void Run()
    {
        ((IHasQueue)ObjectSelector.GetSelectedObject()).StartQueue();
    }

    public void ClearQueue()
    {
        ((IHasQueue)ObjectSelector.GetSelectedObject()).ClearCommands();
    }

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
        if (ObjectSelector.GetSelectedObject().IsType<IHasQueue>())
        {
            ((IHasQueue)ObjectSelector.GetSelectedObject()).AddCommand(CommandQueue.Commands.GrabObject);
        }
    }

    public void ReleaseObject()
    {
        if (ObjectSelector.GetSelectedObject().IsType<IHasQueue>())
        {
            ((IHasQueue)ObjectSelector.GetSelectedObject()).AddCommand(CommandQueue.Commands.ReleaseObject);
        }
    }

    private void LookTowards(Direction direction)
    {
        if (ObjectSelector.GetSelectedObject() == null) return;
        
        if (ObjectSelector.GetSelectedObject().IsType<IMoveable>())
        {
            CommandQueue.Commands command = CommandQueue.Commands.LookDown;
            switch(direction)
            {
                case Direction.N:
                    command = CommandQueue.Commands.LookUp;
                    break;
                case Direction.S:
                    command = CommandQueue.Commands.LookDown;
                    break;
                case Direction.E:
                    command = CommandQueue.Commands.LookRight;
                    break;
                case Direction.W:
                    command = CommandQueue.Commands.LookLeft;
                    break;
            }
            ((IHasQueue)ObjectSelector.GetSelectedObject()).AddCommand(command);
        }
    }

    public void MoveForward()
    {
        if (ObjectSelector.GetSelectedObject() == null) return;
        if (ObjectSelector.GetSelectedObject().IsType<IMoveable>())
        {
            ((IHasQueue)ObjectSelector.GetSelectedObject()).AddCommand(CommandQueue.Commands.MoveForward);
        }
    }

    public void MoveBackward()
    {
        if (ObjectSelector.GetSelectedObject() == null) return;
        if (ObjectSelector.GetSelectedObject().IsType<IMoveable>())
        {
            ((IHasQueue)ObjectSelector.GetSelectedObject()).AddCommand(CommandQueue.Commands.MoveBackward);
        }
    }
}
