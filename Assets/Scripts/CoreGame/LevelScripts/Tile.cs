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
    private States state;
    
    public void SetupTile(Cell cellParent, Transform tilesParent, Vector3 tilePos)
    {
        transform.SetParent(tilesParent);
        transform.position = tilePos;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

        cell = cellParent;
        state = States.Hidden;
    }

    private void Update()
    {
        switch (state)
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
        if (state == States.Showing || state == States.Visible) return;

        transitionTimer = 0f;
        meshRenderer.enabled = true;
        state = States.Showing;
    }

    public void Hide()
    {
        if (state == States.Hidden || state == States.Hiding || state == States.ShowBeforeHiding) return;

        if (state == States.Showing)
        {
            state = States.ShowBeforeHiding;
            return;
        }
        else if (state == States.Visible)
        {
            state = States.Hiding;
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
            if (state == States.ShowBeforeHiding)
            {
                state = States.Visible;
                Hide();
            }
            else
            {
                state = States.Visible;
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
            state = States.Hidden;
        }
    }
}
