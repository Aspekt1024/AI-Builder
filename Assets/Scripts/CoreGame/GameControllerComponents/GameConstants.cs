using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants {

    public const float GridSpacing = 2f;
    
}

public static class Layers
{
    public const int UNIT = 1 << 8;
    public const int COLLECTIBLE = 1 << 9;
    public const int TERRAIN = 1 << 10;
    public const int BUILDING = 1 << 11;
    public const int WALL = 1 << 12;

    public const int SELECTABLE_OBJECT = UNIT | COLLECTIBLE | BUILDING;

    public static int GetMaskFromIndex(int layer)
    {
        return LayerMask.GetMask(LayerMask.LayerToName(layer));
    }
}

// currently not used, while MovementDirections exists in MoveComponent.cs

//public static class Directions
//{
//    public const int FORWARD = 0;
//    public const int BACK = 1;
//    public const int UP = 2;
//    public const int LEFT = 4;
//    public const int RIGHT = 8;
//    public const int DOWN = 16;
//    public const int UP_RIGHT = UP + RIGHT;
//    public const int UP_LEFT = UP + LEFT;
//    public const int DOWN_RIGHT = DOWN + RIGHT;
//    public const int DOWN_LEFT = DOWN + LEFT;
//}
