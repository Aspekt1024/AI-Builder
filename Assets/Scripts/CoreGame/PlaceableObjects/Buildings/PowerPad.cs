using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPad : Building, IReceptor
{
    private enum States
    {
        None, Disabled, Powered, Unpowered
    }
    private States state;

    private IGrabbable grabbedObject;
    private Transform receptorHoldPoint;
    
    private void Start()
    {
        energyUsePerSecond = 0.1f;
        SetReceptorPoint();
    }
    
    private void Update()
    {
        switch (state)
        {
            case States.None:
                break;
            case States.Powered:
                if (grabbedObject != null)
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

    public bool ReceiveObject(IGrabbable grabbableObject)
    {
        if (InterfaceAnalyser.TypeMatch<ResourceCube>(grabbableObject))
        {
            grabbedObject = grabbableObject;
            grabbedObject.PickedUp(this);
            levelScript.AddObjectToTile((SelectableObject)grabbedObject);

            state = States.Powered;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ReleaseObject()
    {
        if (grabbedObject == null)
        {
            return false;
        }
        else
        {
            grabbedObject = null;
            return true;
        }
    }

    public bool DetachGrabbedObject()
    {
        state = States.None;
        grabbedObject = null;
        return true;
    }

    private void RotatePowerSource()
    {
        const float pitchSpeed = 100;
        const float yawSpeed = 200f;
        grabbedObject.SetPosition(receptorHoldPoint.position);
        grabbedObject.RotateObject(new Vector3(pitchSpeed, yawSpeed, 0f) * Time.deltaTime);
    }

    private void ConsumePowerSource()
    {
        if (grabbedObject == null)
        {
            state = States.None;
            return;
        }

        if (grabbedObject.GetType().Equals(typeof(ResourceCube)))
        {
            ((ResourceCube)grabbedObject).UseEnergy(energyUsePerSecond * Time.deltaTime);
        }

    }

    public void SetReceptorPoint()
    {
        foreach (Transform tf in transform)
        {
            if (tf.name == "HeldObjectPosition")
            {
                receptorHoldPoint = tf;
            }
        }
    }
}
