using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandQueue : MonoBehaviour, ICommandQueue {

    // TODO make tags/attributes for each IObjectAttribute
    // then automatically populate this and call commands directly,
    // based on the object this class is attached to
    public enum Commands
    {
        LookLeft, LookRight, LookUp, LookDown, MoveForward, MoveBackward,
        GrabObject, ReleaseObject
    }

    private int queueIndex;
    private const int MaxCommands = 10;
    List<Commands> commandQueue = new List<Commands>();

    private void Start()
    {
        queueIndex = 0;
    }

    public List<Commands> GetCommandQueue()
    {
        return commandQueue;
    }

    public bool CallNext(IHasQueue obj)
    {
        if (commandQueue == null) return false;
        if (commandQueue.Count == queueIndex)
        {
            MessageBox.ClearStepIndicator();
            obj.QueueComplete();
            queueIndex = 0;
            return false;
        }

        Commands cmd = commandQueue[queueIndex];
        MessageBox.SetStepIndicator(queueIndex);
        queueIndex++;
        
        switch (cmd)
        {
            case Commands.LookUp:
                return ((IMoveable)obj).TurnTowards(MoveComponent.Direction.N);
            case Commands.LookDown:
                return ((IMoveable)obj).TurnTowards(MoveComponent.Direction.S);
            case Commands.LookLeft:
                return ((IMoveable)obj).TurnTowards(MoveComponent.Direction.W);
            case Commands.LookRight:
                return ((IMoveable)obj).TurnTowards(MoveComponent.Direction.E);
            case Commands.MoveForward:
                return ((IMoveable)obj).MoveForward();
            case Commands.MoveBackward:
                return ((IMoveable)obj).MoveBackward();
            case Commands.GrabObject:
                return (((ICanGrab)obj).GrabObject());
            case Commands.ReleaseObject:
                return ((ICanGrab)obj).ReleaseObject();
            default:
                return false;
        }
    }

    public bool AddCommand(Commands command)
    {
        if (commandQueue.Count == MaxCommands)
        {
            return false;
        }
        else
        {
            commandQueue.Add(command);
            MessageBox.SetTextFromQueue(commandQueue);
            MessageBox.SetMemoryInidcator(MaxCommands - commandQueue.Count, MaxCommands);
            return true;
        }
    }

    public bool CallFirst(IHasQueue obj)
    {
        queueIndex = 0;
        return CallNext(obj);
    }

    public bool ClearCommands()
    {
        commandQueue = new List<Commands>();
        MessageBox.SetTextFromQueue(commandQueue);
        MessageBox.SetMemoryInidcator(MaxCommands - commandQueue.Count, MaxCommands);
        return true;
    }

    public bool RemoveLastCommand()
    {
        if (commandQueue == null || commandQueue.Count == 0) return false;
        commandQueue.RemoveAt(commandQueue.Count);
        return true;
    }
}
