using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ResourceCube : Collectible {
    
    private Light pointLight;

	// Use this for initialization
	void Start () {
        pointLight = GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	private void Update ()
    {
		switch(grabState)
        {
            case GrabState.None:
                pointLight.intensity = 0.6f;
                break;
            case GrabState.PickedUp:
                pointLight.intensity = 100f;
                transform.position = grabber.GetObjectHoldPosition();
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
