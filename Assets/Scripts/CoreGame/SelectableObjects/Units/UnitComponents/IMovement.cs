using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Direction = MoveComponent.Direction;

public interface IMovement : IUnitComponent {

    bool TurnTowards(Unit unit, Direction direction);
    bool MoveForward(Unit unit, int numSpaces = 1);
    bool MoveBackward(Unit unit, int numSpaces = 1);
    void ReverseMovement(Unit unit);
    void StopMovement();
}
