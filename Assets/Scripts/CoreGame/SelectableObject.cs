
using UnityEngine;

public class SelectableObject : PlaceableObject {

    protected float selectionSize = 34.4f;
    protected Rigidbody body;
    protected MeshRenderer meshRenderer;
    protected Level levelScript;
    
    public override void Init(PlaceableBehaviour placeable)
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
