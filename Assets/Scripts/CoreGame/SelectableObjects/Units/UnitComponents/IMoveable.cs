﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable {

    bool Move(MoveComponent.MovementDirection direction);
    void FinishedMoving();
    void StopMoving();
}
