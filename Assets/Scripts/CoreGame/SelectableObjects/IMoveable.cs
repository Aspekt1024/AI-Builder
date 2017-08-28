using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Direction = MoveComponent.Direction;

public interface IMoveable : IUnitAttribute {

    bool MoveForward();
    bool MoveBackward();
    bool TurnTowards(Direction direction);
    void StopMoving();
    void FinishedMoving();
}
