using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {

    private ObjectPlacer objectPlacer;

    private static LevelCreator levelCreator;
    
    private void Awake()
    {
        if (levelCreator == null)
        {
            levelCreator = this;
        }
        else
        {
            Debug.LogError("Multiple instance of Level (script) found in scene.");
        }
        
        objectPlacer = new ObjectPlacer();
    }

    private void Start ()
    {
        Level.ShowAllTiles();
	}
	
	private void Update ()
    {
        objectPlacer.Update();
	}

    public static void SetCurrentObject(PlaceableObject obj)
    {
        // TODO check states first
        levelCreator.objectPlacer.SetCurrentObject(obj);
    }
}
