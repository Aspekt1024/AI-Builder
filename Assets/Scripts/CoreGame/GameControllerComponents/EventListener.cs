using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener {

    public delegate void SelectListener(SelectableObject obj);
    public static SelectListener OnUnitSelected;

    public delegate void DeselectListener();
    public static DeselectListener OnDeselected;

    public delegate void ResourceListener();
    public static ResourceListener OnCurrencyChange;

    public static void ObjectSelected(SelectableObject obj)
    {
        if (OnUnitSelected != null)
            OnUnitSelected(obj);
    }

    public static void Deselected()
    {
        if (OnDeselected != null)
            OnDeselected();
    }

    public static void CurrencyChange()
    {
        if (OnCurrencyChange != null)
            OnCurrencyChange();
    }
}
