using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ResourceCube : Collectible {
    
    private Light pointLight;
    private float energyRemaining;

	void Start () {
        pointLight = GetComponentInChildren<Light>();
        energyRemaining = 100f;
	}
	
	private void Update ()
    {
		switch(grabState)
        {
            case GrabState.None:
                pointLight.intensity = 0.6f;
                break;
            case GrabState.PickedUp:
                pointLight.intensity = 100f;
                break;
            case GrabState.PutDown:
                pointLight.intensity = 0.6f;
                break;
            case GrabState.Consumed:
                // TODO destroy
                break;
        }
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
        LayerMask layers = 1 << LayerMask.NameToLayer("Building");
        if (Physics.Raycast(ray, out hit, 10f, layers))
        {
            if (hit.collider.gameObject.GetComponent<PowerPad>())
            {
                hit.collider.gameObject.GetComponent<PowerPad>().TakeResource(this);
            }
        }
    }
}
