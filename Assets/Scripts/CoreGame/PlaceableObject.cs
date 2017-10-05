using System;
using UnityEngine;

public class PlaceableObject : MonoBehaviour {

    protected PlaceableBehaviour placeableBehaviour;
    protected Vector3 originalScale;
    
#region lifecycle

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public virtual void Init(PlaceableBehaviour behaviour)
    {
        placeableBehaviour = behaviour;
    }

#endregion
    
    public virtual void SetSize(float sizeRatio)
    {
        transform.localScale = originalScale * sizeRatio;
    }
    
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
