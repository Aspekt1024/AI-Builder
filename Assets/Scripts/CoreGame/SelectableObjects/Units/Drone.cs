using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Drone : Unit {
    
    public enum States
    {
        None, Moving, Idle, Disabled
    }
    private States state;

    private ParticleSystem explosionPS;
    private GameObject droneBody;
    private GrabberComponent grabber;

    #region lifecycle
    private void Start()
    {
        Speed = 5f;
        Health = 1;
        MaxHealth = 1;
        GetComponents();
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
        if (other.tag == "ResourceCube")
        {
            if (grabber.IsHoldingObject())
            {
                if (other.gameObject == grabber.GetHeldCollectible().gameObject) return;
            }

            Explode();
            other.GetComponent<ResourceCube>().Explode();
        }
    }
    #endregion

    public bool GrabObject()
    {
        if (state == States.Moving || state == States.Disabled) return false;
        return grabber.GrabObjectAtPosition(transform.position + transform.forward * GameConstants.GridSpacing + Vector3.up * 2f);
    }

    public bool ReleaseObject()
    {
        if (grabber.IsHoldingObject())
        {
            grabber.ReleaseObjectToPosition(transform.position + transform.forward * GameConstants.GridSpacing);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool Move(MoveComponent.MovementDirection direction)
    {
        if (state == States.Moving || state == States.Disabled) return false;

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
    
    protected sealed override void GetComponents()
    {
        base.GetComponents();
        grabber = gameObject.AddComponent<GrabberComponent>();

        foreach (Transform tf in transform)
        {
            if (tf.name == "HeldObjectPosition")
            {
                grabber.SetObjectHoldPosition(tf);
            }
            else if (tf.name == "Body")
            {
                droneBody = tf.gameObject;
            }
            else if (tf.name == "Particle System")
            {
                explosionPS = gameObject.GetComponentInChildren<ParticleSystem>();
                explosionPS.Stop();
            }
        }
    }

    private void Explode()
    {
        state = States.Disabled;
        StopMoving();
        droneBody.SetActive(false);
        explosionPS.Play();
    }
}
