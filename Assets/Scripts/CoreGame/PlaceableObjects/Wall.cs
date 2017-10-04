using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wall : PlaceableObject {

    public string testStr;

    protected override void Init()
    {
        testStr = "aslksa";
    }
}
