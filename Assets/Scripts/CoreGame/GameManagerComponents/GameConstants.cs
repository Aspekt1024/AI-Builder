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
}
