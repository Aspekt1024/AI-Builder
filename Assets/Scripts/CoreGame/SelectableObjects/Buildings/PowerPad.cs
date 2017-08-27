using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPad : Building
{
    private enum States
    {
        None, Disabled, Powered, Unpowered
    }
    private States state;

    private GrabberComponent grabber;
    
    private void Start()
    {
        CreateGrabber();
        energyUsePerSecond = 0.1f;
    }
    
    private void Update()
    {
        switch (state)
        {
            case States.None:
                break;
            case States.Powered:
                if (grabber.IsHoldingObject())
                {
                    RotatePowerSource();
                    ConsumePowerSource();
                }
                else
                {
                    state = States.None;
                }
                break;
            case States.Unpowered:
                break;
            case States.Disabled:
                break;
        }
    }

    public void TakeResource(ResourceCube resourceCube)
    {
        grabber.GrabObject(resourceCube);
        state = States.Powered;
    }
    
    private void RotatePowerSource()
    {
        const float pitchSpeed = 100;
        const float yawSpeed = 200f;
        grabber.RotateHeldObject(new Vector3(pitchSpeed, yawSpeed, 0f) * Time.deltaTime);
    }

    private void ConsumePowerSource()
    {
        Collectible heldCollectible = grabber.GetHeldCollectible();
        if (heldCollectible == null)
        {
            state = States.None;
            return;
        }

        if (heldCollectible.IsType<ResourceCube>())
        {
            ((ResourceCube)heldCollectible).UseEnergy(energyUsePerSecond * Time.deltaTime);
        }

    }

    private void CreateGrabber()
    {
        grabber = gameObject.AddComponent<GrabberComponent>();

        foreach (Transform tf in transform)
        {
            if (tf.name == "HeldObjectPosition")
            {
                grabber.SetObjectHoldPosition(tf);
            }
        }
    }
}
