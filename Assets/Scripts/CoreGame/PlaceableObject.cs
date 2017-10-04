using System;
using UnityEngine;

[Serializable]
public class PlaceableObject : MonoBehaviour {

    protected Vector3 originalScale;
    
    private enum States
    {
        None, Placed, Held
    }
    private States state;

#region lifecycle

    private void Awake()
    {
        originalScale = transform.localScale;

        Init();
    }

    protected virtual void Init() { }

#endregion
    
    public void SetHeld()
    {
        state = States.Held;
    }

    public void SetPlaced()
    {
        state = States.Placed;
    }
    
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
