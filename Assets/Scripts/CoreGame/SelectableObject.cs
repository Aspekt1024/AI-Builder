
using UnityEngine;

public class SelectableObject : PlaceableObject {

    protected float selectionSize = 34.4f;
    protected Rigidbody body;
    protected MeshRenderer meshRenderer;
    protected Level levelScript;
    
    protected override void Init()
    {
        levelScript = FindObjectOfType<Level>();
        GetComponents();
	}

    protected virtual void GetComponents()
    {
        body = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    
    public float GetSelectionSize() { return selectionSize; }
    
}
