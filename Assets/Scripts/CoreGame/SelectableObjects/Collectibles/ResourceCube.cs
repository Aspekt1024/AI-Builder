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
}
