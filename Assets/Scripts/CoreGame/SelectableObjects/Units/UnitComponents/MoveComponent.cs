using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public enum MovementDirection
    {
        None, N, S, E, W, NE, SE, SW, NW
    }
    
    private const float turnSpeed = 360f;    // degrees per sec

    public bool Move(Unit unit, MovementDirection direction)
    {
        Vector3 distance = new Vector3();
        switch (direction)
        {
            case MovementDirection.N:
                distance = Vector3.forward * GameConstants.GridSpacing;
                break;
            case MovementDirection.S:
                distance = Vector3.back * GameConstants.GridSpacing;
                break;
            case MovementDirection.E:
                distance = Vector3.right * GameConstants.GridSpacing;
                break;
            case MovementDirection.W:
                distance = Vector3.left * GameConstants.GridSpacing;
                break;
            default:
                Debug.LogWarning("Diagonal directions not implemented");
                return false;
        }
        
        LayerMask layers = 1 << Layers.BUILDING | 1 << Layers.COLLECTIBLE | 1 << Layers.WALL | 1 << Layers.UNIT;
        if (GridRaycaster.CheckForObject(unit.transform.position + distance, layers))
        {
            StartCoroutine(MoveFailRoutine(unit, distance));
            return false;
        }
        else
        {
            StartCoroutine(MoveRoutine(unit, distance));
            return true;
        }
    }

    private IEnumerator MoveFailRoutine(Unit unit, Vector3 distance)
    {
        Vector3 direction = distance.normalized;

        float distToMove = GameConstants.GridSpacing / 4f;
        Vector3 startPos = unit.transform.position;
        Vector3 endPos = startPos + direction * distToMove;

        yield return TurnTowardsPoint(unit.transform, endPos);

        while (Vector3.Distance(unit.transform.position, endPos) > 0.1f)
        {
            unit.transform.position += direction * Time.deltaTime * unit.Speed;
            yield return null;
        }

        while (Vector3.Distance(unit.transform.position, startPos) > 0.1f)
        {
            unit.transform.position -= direction * Time.deltaTime * unit.Speed;
            yield return null;
        }
        unit.transform.position = startPos;
        unit.FinishedMoving();
    }

    private IEnumerator MoveRoutine(Unit unit, Vector3 distance)
    {
        Vector3 startPos = unit.transform.position;
        Vector3 endPos = startPos += distance;

        yield return TurnTowardsPoint(unit.transform, endPos);

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
