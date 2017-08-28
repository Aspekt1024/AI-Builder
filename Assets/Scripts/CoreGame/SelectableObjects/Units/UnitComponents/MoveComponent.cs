using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour, IMovement
{
    public enum Direction
    {
        None,
        N, S, E, W,
        NE, SE, SW, NW
    }
    
    private const float turnSpeed = 720f;    // degrees per sec
    private Coroutine moveRoutine;
    private Vector3 originalPosition;

    public void StopMovement()
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
    }

    public bool TurnTowards(Unit unit, Direction direction)
    {
        switch(direction)
        {
            case Direction.None:
                return true;
            case Direction.N:
                StartCoroutine(TurnTowardsPoint(unit.transform, unit.transform.position + Vector3.forward));
                break;
            case Direction.S:
                StartCoroutine(TurnTowardsPoint(unit.transform, unit.transform.position + Vector3.back));
                break;
            case Direction.E:
                StartCoroutine(TurnTowardsPoint(unit.transform, unit.transform.position + Vector3.right));
                break;
            case Direction.W:
                StartCoroutine(TurnTowardsPoint(unit.transform, unit.transform.position + Vector3.left));
                break;
            default:
                Debug.LogWarning("Diagonal directions not implemented");
                return false;
        }
        return true;
    }

    public bool MoveForward(Unit unit, int numSpaces = 1)
    {
        Vector3 distance = transform.forward * numSpaces * GameConstants.GridSpacing;
        moveRoutine = StartCoroutine(MoveRoutine(unit, distance));
        return true;
    }

    public bool MoveBackward(Unit unit, int numSpaces = 1)
    {
        Vector3 distance = -transform.forward * numSpaces * GameConstants.GridSpacing;
        moveRoutine = StartCoroutine(MoveRoutine(unit, distance));
        return true;
    }

    public void ReverseMovement(Unit unit)
    {
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(ReverseMovementRoutine(unit));
    }

    private IEnumerator ReverseMovementRoutine(Unit unit)
    {
        Vector3 direction = (originalPosition - unit.transform.position).normalized;
        while (Vector3.Distance(unit.transform.position, originalPosition) > 0.1f)
        {
            unit.transform.position += direction * Time.deltaTime * unit.Speed;
            yield return null;
        }
        unit.transform.position = originalPosition;
        unit.FinishedMoving();
    }

    private IEnumerator MoveRoutine(Unit unit, Vector3 distance)
    {
        Vector3 startPos = unit.transform.position;
        Vector3 endPos = startPos + distance;

        originalPosition = startPos;

        while (Vector3.Distance(unit.transform.position, endPos) > 0.1f)
        {
            unit.transform.position += distance.normalized * Time.deltaTime * unit.Speed;
            yield return null;
        }
        unit.transform.position = endPos;
        unit.FinishedMoving();
    }

    private IEnumerator TurnTowardsPoint(Transform unitTf, Vector3 lookPoint)
    {
        float targetRotation = GetTargetRotation(unitTf, lookPoint);
        float requiredRotation = GetRequiredRotation(unitTf, targetRotation);

        while (Mathf.Abs(requiredRotation) > 0.1f)
        {
            float rotation = Mathf.Clamp(requiredRotation, -turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime);

            if (Mathf.Abs(rotation) > Mathf.Abs(targetRotation - unitTf.eulerAngles.y))
            {
                rotation = targetRotation - unitTf.eulerAngles.y;
                requiredRotation = 0f;
            }
            else
            {
                requiredRotation -= rotation;
            }

            unitTf.Rotate(Vector3.up, rotation);
            yield return null;
        }
        unitTf.LookAt(lookPoint);
    }

    private float GetTargetRotation(Transform unitTf, Vector3 lookPoint)
    {
        float xDist = (lookPoint.x - unitTf.position.x);
        float zDist = (lookPoint.z - unitTf.position.z);

        const float radToDeg = 57.295779513f;

        float targetRotation = Mathf.Atan(-zDist / xDist) * radToDeg;

        if (xDist < 0)
        {
            targetRotation = 180 + targetRotation;
        }
        else if (zDist < 0)
        {
            targetRotation = 360 + targetRotation;
        }
        return targetRotation + 90f;
    }

    private float GetRequiredRotation(Transform unitTf, float targetRotation)
    {
        float requiredRotation = targetRotation - unitTf.eulerAngles.y;
        if (requiredRotation > 180)
        {
            requiredRotation -= 360f;
        }
        else if (requiredRotation < -180)
        {
            requiredRotation += 360f;
        }

        return requiredRotation;
    }
}
