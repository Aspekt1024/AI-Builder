using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreatorGameController : GameController {

    private ObjectPlacer objectPlacer;

    private void Start()
    {
        objectPlacer = FindObjectOfType<LevelCreator>().GetObjectPlacer();
    }

    public override void LeftMouseDownReceived(Vector2 position)
    {
    }

    public override void LeftMouseUpReceived(Vector2 position)
    {
        if (state == States.Normal)
        {
            objectPlacer.PlaceObject(position);
        }
    }

    public override void LeftMouseUpWithShiftReceived(Vector2 position)
    {
        if (state == States.Normal)
        {
            objectPlacer.PlaceObjectAndRetainSelection(position);
        }
    }
}
