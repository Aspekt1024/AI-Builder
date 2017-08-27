using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour {

    private enum States
    {
        None, Indicating, Stopping, Starting
    }
    private States state;

    private SelectableObject selectedObject;
    private Projector projector;

    private const float rotationSpeed = 110f;

    private void Start()
    {
        projector = GetComponent<Projector>();
        projector.fieldOfView = 34.4f;
        state = States.Stopping;
    }

    private void Update()
    {
        switch (state)
        {
            case States.None:
                break;
            case States.Stopping:
                projector.enabled = false;
                state = States.None;
                break;
            case States.Starting:
                projector.enabled = true;
                projector.fieldOfView = selectedObject.GetSelectionSize();
                state = States.Indicating;
                break;
            case States.Indicating:
                FollowAndAnimate();
                break;
        }
    }

    private void FollowAndAnimate()
    {
        transform.position = new Vector3(selectedObject.transform.position.x, transform.position.y, selectedObject.transform.position.z);
        transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
    }
    
    public void SelectObject(SelectableObject obj)
    {
        state = States.Starting;
        selectedObject = obj;
    }

    public void DeselectObject()
    {
        state = States.Stopping;
    }
}
