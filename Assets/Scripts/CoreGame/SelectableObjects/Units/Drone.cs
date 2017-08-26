using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Drone : Unit {
    
    public enum States
    {
        None, Moving, Idle, Disabled
    }
    private States state;

    private GrabberComponent grabber;

    #region lifecycle
    private void Start()
    {
        Speed = 5f;
        Health = 1;
        MaxHealth = 1;
        GetComponents();
    }

    protected sealed override void GetComponents()
    {
        base.GetComponents();
        grabber = gameObject.AddComponent<GrabberComponent>();

        foreach(Transform tf in transform)
        {
            if (tf.name == "HeldObjectPosition")
            {
                grabber.SetObjectHoldPosition(tf);
            }
        }
    }

    private void Update()
    {
        switch (state)
        {
            case States.None:
                break;
            case States.Idle:
                break;
            case States.Moving:
                break;
            case States.Disabled:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Collectible collectibleObject = other.GetComponent<Collectible>();
        if (collectibleObject != null)
        {
            collectibleObject.PickedUp(grabber);
        }
    }
    #endregion

    public override bool Move(MoveComponent.MovementDirection direction)
    {
        if (state == States.Moving) return false;

        if (base.Move(direction))
        {
            state = States.Moving;
            return true;
        }
        return false;
    }

    public override void FinishedMoving()
    {
        if (state == States.Moving)
        {
            SetState(States.Idle);
        }
    }

    private void SetState(States newState)
    {
        // TODO handle logic for state transitions
        state = newState;
    }

}
