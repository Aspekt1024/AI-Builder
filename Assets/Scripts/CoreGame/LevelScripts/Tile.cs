using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private enum States
    {
        Hidden, Visible, Showing, Hiding, ShowBeforeHiding
    }
    private States state;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float transitionTimer;
    private const float VerticalDist = 2f;
    private const float ShowDuration = 0.4f;
    private const float HideDuration = 1f;

    private MeshRenderer meshRenderer;
    private List<SelectableObject> inhabitingObjects = new List<SelectableObject>();

    #region lifecycle

    private void Awake()
    {
        state = States.Hidden;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
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

    #endregion lifecycle

    public void RemoveInhabitingObject(SelectableObject obj)
    {
        obj.SetAlpha(1f);
        inhabitingObjects.Remove(obj);
    }

    public void AddInhabitingObject(SelectableObject obj)
    {
        if (inhabitingObjects.Contains(obj)) return;
        inhabitingObjects.Add(obj);

        if (state == States.Hidden || state == States.Hiding)
        {
            obj.SetAlpha(0f);
        }
    }

    public void Show()
    {
        if (state == States.Showing || state == States.Visible) return;
        
        startPosition = targetPosition = transform.position;
        startPosition.y = -VerticalDist;
        targetPosition.y = 0f;

        transform.position = startPosition;
        SetObjectAlpha(0f);
        SetTileAlpha(0f);

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
        transform.position = Vector3.Lerp(startPosition, targetPosition, transitionTimer / ShowDuration);
        SetObjectAlpha(transitionTimer / ShowDuration);
        SetTileAlpha(transitionTimer / ShowDuration);

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

        float alpha = 1 - transitionTimer / ShowDuration;
        SetObjectAlpha(alpha);
        SetTileAlpha(alpha);

        if (transitionTimer >= HideDuration)
        {
            meshRenderer.enabled = false;
            state = States.Hidden;
        }
    }
    
    private void SetObjectAlpha(float alpha)
    {
        if (inhabitingObjects == null) return;
        
        foreach (SelectableObject obj in inhabitingObjects)
        {
            obj.SetAlpha(alpha);
        }
    }

    private void SetTileAlpha(float alpha)
    {
        Color matColor = meshRenderer.material.color;
        matColor.a = alpha;
        meshRenderer.material.color = matColor;
    }
}
