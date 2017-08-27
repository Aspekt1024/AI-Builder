using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour {

    protected float selectionSize = 34.4f;
    protected Rigidbody body;

	void Start ()
    {
        GetComponents();
	}

    protected virtual void GetComponents()
    {
        body = GetComponent<Rigidbody>();
    }

    public bool IsType<T>()
    {
        return GetType().Equals(typeof(T)) || GetType().IsSubclassOf(typeof(T));
    }

    public float GetSelectionSize() { return selectionSize; }
}
