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
        StartCoroutine(MoveRoutine(unit, distance));
        return true;
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
        RotationVariables rotations = GetRotationVariables(unitTf, lookPoint);

        while (Mathf.Abs(rotations.Required) > 0.1f)
        {
            float rotation = Mathf.Clamp(rotations.Required, -turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime);

            if (Mathf.Abs(rotation) > Mathf.Abs(rotations.Target - unitTf.eulerAngles.y))
            {
                rotation = rotations.Target - unitTf.eulerAngles.y;
                rotations.Required = 0f;
            }
            else
            {
                rotations.Required -= rotation;
            }

            unitTf.Rotate(Vector3.up, rotation);
            yield return null;
        }
        unitTf.LookAt(lookPoint);
    }

    private struct RotationVariables
    {
        public float Target;
        public float Required;
    }

    private RotationVariables GetRotationVariables(Transform unitTf, Vector3 lookPoint)
    {
        float xDist = (lookPoint.x - unitTf.position.x);
        float zDist = (lookPoint.z - unitTf.position.z);

        const float radToDeg = 57.295779513f;

        RotationVariables rotations = new RotationVariables();

        rotations.Target = Mathf.Atan(-zDist / xDist) * radToDeg;

        if (xDist < 0)
        {
            rotations.Target = 180 + rotations.Target;
        }
        else if (zDist < 0)
        {
            rotations.Target = 360 + rotations.Target;
        }
        rotations.Target += 90f;

        rotations.Required = rotations.Target - unitTf.eulerAngles.y;
        if (rotations.Required > 180) rotations.Required -= 360f;
        else if (rotations.Required < -180) rotations.Required += 360f;

        return rotations;
    }
}
