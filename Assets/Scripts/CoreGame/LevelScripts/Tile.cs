using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : PlaceableObject {

    private Cell cell;
    private float transitionTimer;
    private const float ShowDuration = 0.4f;
    private const float HideDuration = 1f;
    
    private MeshRenderer meshRenderer;

    private enum States
    {
        Hidden, Visible, Showing, Hiding, ShowBeforeHiding
    }
    private States visibilityState;

    public void SetupTile(Cell cellParent, Transform tilesParent, Vector3 tilePos)
    {
        transform.SetParent(tilesParent);
        transform.position = tilePos;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

        cell = cellParent;
        visibilityState = States.Hidden;
    }

    private void Update()
    {
        switch (visibilityState)
        {
            case States.Hidden:
                break;
            case States.Visible:
                break;
            case States.Showing:
                MoveToVisiblePosition();
                break;
            case States.Hiding:
                MoveToHiddenPosition();
                break;
            case States.ShowBeforeHiding:
                MoveToVisiblePosition();
                break;
            default:
                break;
        }
    }

    public void Show()
    {
        if (visibilityState == States.Showing || visibilityState == States.Visible) return;

        transitionTimer = 0f;
        meshRenderer.enabled = true;
        visibilityState = States.Showing;
    }

    public void Hide()
    {
        if (visibilityState == States.Hidden || visibilityState == States.Hiding || visibilityState == States.ShowBeforeHiding) return;

        if (visibilityState == States.Showing)
        {
            visibilityState = States.ShowBeforeHiding;
            return;
        }
        else if (visibilityState == States.Visible)
        {
            visibilityState = States.Hiding;
        }

        transitionTimer = 0f;
    }

    private void MoveToVisiblePosition()
    {
        transitionTimer += Time.deltaTime;

        cell.SetObjectSize(transitionTimer / ShowDuration);
        SetSize(transitionTimer / ShowDuration);

        if (transitionTimer >= ShowDuration)
        {
            if (visibilityState == States.ShowBeforeHiding)
            {
                visibilityState = States.Visible;
                Hide();
            }
            else
            {
                visibilityState = States.Visible;
            }
        }
    }

    private void MoveToHiddenPosition()
    {
        transitionTimer += Time.deltaTime;

        float sizeRatio = 1 - transitionTimer / ShowDuration;
        cell.SetObjectSize(sizeRatio);
        SetSize(sizeRatio);

        if (transitionTimer >= HideDuration)
        {
            meshRenderer.enabled = false;
            visibilityState = States.Hidden;
        }
    }
}
