using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : GameController {

    private ObjectSelector objectSelector;

    private void Start()
    {
        objectSelector = FindObjectOfType<ObjectSelector>();
    }

    public override void LeftMouseDownReceived(Vector2 position)
    {
        if (state == States.Normal)
        {
            objectSelector.SelectSingleObject(position);
        }
    }

    public override void LeftMouseUpReceived(Vector2 position)
    {

    }

    public override void LeftMouseUpWithShiftReceived(Vector2 position)
    {

    }

    public override void CheckMouseover(Vector2 position)
    {
        if (state == States.Normal)
        {
            objectSelector.GetMouseOver(position);
        }
    }
}
