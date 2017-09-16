using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Commands = CommandQueue.Commands;

public sealed class Drone : Unit, ICanGrab, IHasQueue, ICanSee {
    
    public enum States
    {
        None, Executing, CallingNext, Stopping, Disabled
    }
    private States state;

    private ParticleSystem explosionPS;
    private GrabberComponent grabber;
    private CommandQueue commandQueue;
    private VisionComponent vision;

    #region lifecycle
    private void Start()
    {
        Speed = 7f;
        Health = 1;
        MaxHealth = 1;
        GetComponents();

        Look();
    }

    private void Update()
    {
        switch (state)
        {
            case States.None:
                break;
            case States.Executing:
                break;
            case States.CallingNext:
                CallNext();
                break;
            case States.Disabled:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ResourceCube")
        {
            if (grabber.IsHoldingObject())
            {
                if (other.gameObject == grabber.GetHeldCollectible().gameObject) return;
            }
            
            other.GetComponent<ResourceCube>().Hit(transform.position);
            moveComponent.ReverseMovement(this);
        }
    }
    #endregion

    public bool Look()
    {
        return vision.Look();
    }

    public bool GrabObject()
    {
        if (state == States.Disabled) return false;
        return grabber.GrabObjectAtPosition(transform.position + transform.forward * GameConstants.GridSpacing + Vector3.up * 2f);
    }

    public bool ReleaseObject()
    {
        if (state == States.Disabled) return false;
        
        if (grabber.IsHoldingObject())
        {
            if (grabber.ReleaseObjectToPosition(transform.position + transform.forward * GameConstants.GridSpacing))
            {
                return true;
            }
        }
        return false;
    }

    public override bool MoveForward()
    {
        if (state == States.Disabled) return false;
        return base.MoveForward();
    }

    public override bool MoveBackward()
    {
        if (state == States.Disabled) return false;
        return base.MoveBackward();
    }

    public override void FinishedAction(bool success)
    {
        if (success)
        {
            Look();
        }

        if (state == States.Executing)
        {
            SetState(States.CallingNext);
        }
    }

    private void SetState(States newState)
    {
        // TODO handle logic for state transitions
        state = newState;
    }
    
    protected sealed override void GetComponents()
    {
        base.GetComponents();
        grabber = gameObject.AddComponent<GrabberComponent>();
        commandQueue = gameObject.AddComponent<CommandQueue>();
        vision = gameObject.AddComponent<VisionComponent>();

        foreach (Transform tf in transform)
        {
            if (tf.name == "HeldObjectPosition")
            {
                grabber.SetObjectHoldPosition(tf);
            }
            else if (tf.name == "Particle System")
            {
                explosionPS = gameObject.GetComponentInChildren<ParticleSystem>();
                explosionPS.Stop();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != States.Executing) return;
        switch (Layers.GetMaskFromIndex(collision.collider.gameObject.layer))
        {
            case Layers.WALL:
                moveComponent.ReverseMovement(this);
                break;
            case Layers.BUILDING:
                moveComponent.ReverseMovement(this);
                break;
            case Layers.COLLECTIBLE:
                moveComponent.ReverseMovement(this);
                break;
            default:
                break;
        }
    }

    public bool StartQueue()
    {
        if (state == States.Disabled || state == States.CallingNext || state == States.Stopping || state == States.Executing) return false;
        state = States.CallingNext;
        return true;
    }

    public bool AddCommand(Commands function)
    {
        return commandQueue.AddCommand(function);
    }

    public bool RemoveLastCommand()
    {
        return commandQueue.RemoveLastCommand();
    }

    public bool ClearCommands()
    {
        return commandQueue.ClearCommands();
    }

    public bool CallNext()
    {
        if (state != States.CallingNext) return false;
        state = States.Executing;
        return commandQueue.CallNext(this);
    }

    public bool RestartQueue()
    {
        if (state == States.None || state == States.None)
        {
            return commandQueue.CallFirst(this);
        }
        else
        {
            return false;
        }
    }

    public void QueueComplete()
    {
        if (state != States.Disabled)
        {
            state = States.None;
        }
    }

    public List<Commands> GetCommandQueue()
    {
        return commandQueue.GetCommandQueue();
    }
}
