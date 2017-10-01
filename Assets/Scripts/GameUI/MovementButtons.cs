using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Direction = MoveComponent.Direction;

public class MovementButtons : MonoBehaviour {

    private ObjectSelector objectSelector;

    private void Start()
    {
        objectSelector = FindObjectOfType<ObjectSelector>();
    }

    public void Run()
    {
        ((IHasQueue)objectSelector.GetSelectedObject()).StartQueue();
    }

    public void ClearQueue()
    {
        ((IHasQueue)objectSelector.GetSelectedObject()).ClearCommands();
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
        if (objectSelector.GetSelectedObject().IsType<IHasQueue>())
        {
            ((IHasQueue)objectSelector.GetSelectedObject()).AddCommand(CommandQueue.Commands.GrabObject);
        }
    }

    public void ReleaseObject()
    {
        if (objectSelector.GetSelectedObject().IsType<IHasQueue>())
        {
            ((IHasQueue)objectSelector.GetSelectedObject()).AddCommand(CommandQueue.Commands.ReleaseObject);
        }
    }

    private void LookTowards(Direction direction)
    {
        if (objectSelector.GetSelectedObject() == null) return;
        
        if (objectSelector.GetSelectedObject().IsType<IMoveable>())
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
            ((IHasQueue)objectSelector.GetSelectedObject()).AddCommand(command);
        }
    }

    public void MoveForward()
    {
        if (objectSelector.GetSelectedObject() == null) return;
        if (objectSelector.GetSelectedObject().IsType<IMoveable>())
        {
            ((IHasQueue)objectSelector.GetSelectedObject()).AddCommand(CommandQueue.Commands.MoveForward);
        }
    }

    public void MoveBackward()
    {
        if (objectSelector.GetSelectedObject() == null) return;
        if (objectSelector.GetSelectedObject().IsType<IMoveable>())
        {
            ((IHasQueue)objectSelector.GetSelectedObject()).AddCommand(CommandQueue.Commands.MoveBackward);
        }
    }
}
