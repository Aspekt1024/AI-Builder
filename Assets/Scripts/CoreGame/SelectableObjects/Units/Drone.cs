using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Drone : Unit, ICanGrab {
    
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
        Speed = 10f;
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
            if (grabber.ReleaseObjectToPosition(transform.position + transform.forward * GameConstants.GridSpacing))
            {
                return true;
            }
        }
        return false;
    }

    public override bool MoveForward()
    {
        if (state == States.Moving || state == States.Disabled) return false;

        if (base.MoveForward())
        {
            state = States.Moving;
            return true;
        }
        return false;
    }

    public override bool MoveBackward()
    {
        if (state == States.Moving || state == States.Disabled) return false;

        if (base.MoveBackward())
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

    private void OnCollisionEnter(Collision collision)
    {
        if (state != States.Moving) return;
        switch (Layers.GetMaskFromIndex(collision.collider.gameObject.layer))
        {
            case Layers.WALL:
                moveComponent.ReverseMovement(this);
                break;
            case Layers.BUILDING:
                moveComponent.ReverseMovement(this);
                break;
            case Layers.COLLECTIBLE:
                if (collision.collider.GetComponent<ResourceCube>() != null)
                {
                    Explode();
                }
                moveComponent.ReverseMovement(this);
                break;
            default:
                break;
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
