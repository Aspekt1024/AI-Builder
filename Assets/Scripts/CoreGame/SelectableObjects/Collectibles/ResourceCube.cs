using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ResourceCube : Collectible {
    
    private enum States
    {
        None, Collision, Consumed
    }
    private States state;

    private Light pointLight;
    private float energyRemaining;
    private ParticleSystem particles;

	void Start () {
        pointLight = GetComponentInChildren<Light>();
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop();
        energyRemaining = 100f;
	}

    public void Hit(Vector3 otherPosition)
    {
        particles.transform.LookAt(otherPosition);
        particles.transform.position += particles.transform.forward * 0.3f;
        state = States.Collision;
    }
    
    private void Update ()
    {
		switch(grabState)
        {
            case GrabState.None:
                pointLight.intensity = 0.6f;
                grabState = GrabState.PutDown;
                break;
            case GrabState.PickedUp:
                if (currentHolder.GetType().Equals(typeof(PowerPad)))
                {
                    pointLight.intensity = 100f;
                }
                else
                {
                    pointLight.intensity = 0.6f;
                }
                break;
            case GrabState.PutDown:
                grabState = GrabState.None;
                break;
        }

        switch(state)
        {
            case States.None:
                break;
            case States.Collision:
                particles.Play();
                state = States.None;
                break;
            case States.Consumed:
                break;
        }

	}

    public void Explode()
    {
        pointLight.color = Color.yellow;
        pointLight.intensity = 150f;
    }

    public bool UseEnergy(float energyUse)
    {
        if (energyRemaining < energyUse) return false;
        energyRemaining -= energyUse;
        return true;
    }

    protected override void OnPlaced()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray()
        {
            origin = transform.position + Vector3.up,
            direction = Vector3.down
        };

        if (Physics.Raycast(ray, out hit, 10f, Layers.BUILDING))
        {
            PowerPad pad = hit.collider.gameObject.GetComponent<PowerPad>();
            if (pad == null) return;

            pad.ReceiveObject(this);
        }
    }

    public override bool CheckForValidDrop(Vector3 position)
    {
        LayerMask layers = Layers.BUILDING | Layers.TERRAIN;
        GameObject obj = GridRaycaster.GetObject(position, layers);
        if (obj == null) return false;

        if (obj.GetComponent<Building>() == null)
        {
            return true;
        }
        else
        {
            PowerPad pad = obj.GetComponent<PowerPad>();
            return pad != null;
        }
        
    }
}
