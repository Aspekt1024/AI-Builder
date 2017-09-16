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
    private const float VerticalDist = 10f;
    private const float ShowDuration = 0.2f;
    private const float HideDuration = 0.2f;

    private MeshRenderer meshRenderer;
    private SelectableObject inhabitingObject;
    private float[] originalObjectAlpha;

    #region lifecycle

    private void Start()
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

    public void SetInhabitingObject(SelectableObject obj)
    {
        inhabitingObject = obj;
        originalObjectAlpha = GetOriginalAlpha(inhabitingObject);
    }

    public void Show()
    {
        if (state == States.Showing || state == States.Visible) return;
        
        startPosition = targetPosition = transform.position;
        startPosition.y = -VerticalDist;
        targetPosition.y = 0f;

        transitionTimer = 0f;
        transform.position = startPosition;
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

        startPosition = targetPosition = transform.position;
        targetPosition.y = -VerticalDist;
        transitionTimer = 0f;
    }

    public bool IsHidden() { return state == States.Hiding || state == States.Hidden; }
    public bool IsVisible() { return state == States.Visible || state == States.Showing; }

    private void MoveToVisiblePosition()
    {
        transitionTimer += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition, targetPosition, transitionTimer / ShowDuration);
        SetObjectAlpha(inhabitingObject, transitionTimer / ShowDuration);

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
        if (transitionTimer < HideDuration / 3f) return;

        transform.position = Vector3.Lerp(startPosition, targetPosition, (transitionTimer - HideDuration / 3f) / (2f * HideDuration / 3f));
        SetObjectAlpha(inhabitingObject, 1 - transitionTimer / ShowDuration);

        if (transitionTimer >= HideDuration)
        {
            meshRenderer.enabled = false;
            state = States.Hidden;
        }
    }

    private void SetObjectAlpha(SelectableObject obj, float alpha)
    {
        if (inhabitingObject == null) return;
        
        MeshRenderer objMeshRenderer = obj.GetComponentInChildren<MeshRenderer>();
        for (int i = 0; i < originalObjectAlpha.Length; i++)
        {
            Color matColor = objMeshRenderer.materials[i].color;
            matColor.a = Mathf.Min(originalObjectAlpha[i], alpha);
            objMeshRenderer.materials[i].color = matColor;
        }
    }

    private float[] GetOriginalAlpha(SelectableObject obj)
    {
        if (inhabitingObject == null) return null;

        MeshRenderer objMeshRenderer = obj.GetComponentInChildren<MeshRenderer>();
        float[] alphas = new float[objMeshRenderer.materials.Length];
        for (int i = 0; i < alphas.Length; i++)
        {
            alphas[i] = objMeshRenderer.materials[i].color.a;
        }
        return alphas;
    }
}
