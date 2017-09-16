using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private enum States
    {
        Hidden, Visible, Showing, Hiding
    }
    private States state;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float transitionTimer;
    private const float VerticalDist = 10f;
    private const float TransitionDuration = 0.4f;

    #region lifecycle

    private void Start()
    {
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
            default:
                break;
        }
    }

    #endregion lifecycle

    public void Show()
    {
        state = States.Showing;
        gameObject.SetActive(true);
        startPosition = targetPosition = transform.position;
        startPosition.y = -VerticalDist;
        targetPosition.y = 0f;
        transitionTimer = 0f;
    }

    public void Hide()
    {
        state = States.Hiding;
        startPosition = targetPosition = transform.position;
        targetPosition.y = -VerticalDist;
        transitionTimer = 0f;
    }

    public bool IsHidden() { return state == States.Hiding || state == States.Hidden || state == States.Showing; }
    public bool IsVisible() { return state == States.Visible; }

    private void MoveToVisiblePosition()
    {
        transitionTimer += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.Pow(transitionTimer / TransitionDuration, 0.1f));

        if (transitionTimer >= TransitionDuration)
        {
            state = States.Visible;
        }
    }

    private void MoveToHiddenPosition()
    {
        transitionTimer += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.Pow(transitionTimer / TransitionDuration, 8f));

        if (transitionTimer >= TransitionDuration)
        {
            gameObject.SetActive(false);
            state = States.Hidden;
        }
    }
}
