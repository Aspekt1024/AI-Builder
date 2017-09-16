using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour, ICanFade {

    protected float selectionSize = 34.4f;
    protected Rigidbody body;
    protected MeshRenderer meshRenderer;

    protected float[] originalAlphas;
    
	private void Awake ()
    {
        GetComponents();
	}

    protected virtual void GetComponents()
    {
        body = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        GetOriginalAlphas();
    }

    public virtual void SetAlpha(float alpha)
    {
        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            Color matColor = meshRenderer.materials[i].color;
            matColor.a = Mathf.Min(originalAlphas[i], alpha);
            meshRenderer.materials[i].color = matColor;
        }
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

    private void GetOriginalAlphas()
    {
        originalAlphas = new float[meshRenderer.materials.Length];
        for (int i = 0; i < originalAlphas.Length; i++)
        {
            originalAlphas[i] = meshRenderer.materials[i].color.a;
        }
    }

}
