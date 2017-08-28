using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InterfaceAnalyser {

    public static bool TypeMatch<T>(object iface)
    {
        return typeof(T).Equals(iface.GetType());
    }
}
