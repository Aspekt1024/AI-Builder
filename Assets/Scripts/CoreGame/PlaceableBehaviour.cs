using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class PlaceableBehaviour {

    private PlaceableObject obj;

    public PlaceableObject Object
    {
        get { return obj; }
    }

    protected abstract void CreateObject();
    
    public bool IsType<T>()
    {
        if (typeof(T).IsInterface)
        {
            foreach (Type iface in GetType().GetInterfaces())
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
}
