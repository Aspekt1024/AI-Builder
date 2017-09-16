using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tile))]
public class TileInspectorTest : Editor {

    private Tile tile;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        tile = (Tile)target;

        if (GUILayout.Button("show"))
        {
            Vector3 pos = new Vector3(tile.transform.position.x, 0f, tile.transform.position.z);
            tile.Show();
        }

        if (GUILayout.Button("hide"))
        {
            tile.Hide();
        }
    }
}
