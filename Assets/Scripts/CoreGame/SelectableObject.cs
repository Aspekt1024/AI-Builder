using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour {

    protected Rigidbody body;

	// Use this for initialization
	void Start () {
        GetComponents();
	}
	
	// Update is called once per frame
	void Update () {

    }

    protected virtual void GetComponents()
    {
        body = GetComponent<Rigidbody>();
    }

    public bool IsType<T>()
    {
        return GetType().Equals(typeof(T)) || GetType().IsSubclassOf(typeof(T));
    }
}
