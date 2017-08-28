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
