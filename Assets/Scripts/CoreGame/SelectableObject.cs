using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour {

    protected float selectionSize = 34.4f;
    protected Rigidbody body;
    protected MeshRenderer meshRenderer;
    protected Vector3 originalScale;
    
	private void Awake ()
    {
        GetComponents();
	}

    protected virtual void GetComponents()
    {
        body = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalScale = transform.localScale;
    }

    public virtual void SetSize(float sizeRatio)
    {
        transform.localScale = originalScale * sizeRatio;
    }

    public bool IsType<T>()
    {
        if (typeof(T).IsInterface)
        {
            foreach(Type iface in GetType().GetInterfaces())
            {
                if (iface.Equals(typeof(T)))
                {
                    return true;
                }
            }
            return false;
        }
        return GetType().Equals(typeof(T)) || GetType().IsSubclassOf(typeof(T));
    }
    
    public float GetSelectionSize() { return selectionSize; }
    
}
