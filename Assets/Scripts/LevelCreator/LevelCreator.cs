using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {

    private ObjectPlacer objectPlacer;
    private Level levelScript;
    
    private void Awake()
    {
        objectPlacer = new ObjectPlacer();
        levelScript = FindObjectOfType<Level>();
    }

    private void Start ()
    {
        levelScript.ShowAllTiles();
	}
	
	private void Update ()
    {
        objectPlacer.Update();
	}

    public ObjectPlacer GetObjectPlacer()
    {
        return objectPlacer;
    }

    public void SetCurrentObject(PlaceableObject obj)
    {
        // TODO check states first
        objectPlacer.SetCurrentObject(obj);
    }
}
