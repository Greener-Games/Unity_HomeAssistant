using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class EntityUiElement :Attribute
{
    readonly string uiElement;

    public EntityUiElement(string uiElement)
    {
        this.uiElement = uiElement;
    }

    public GameObject LoadUiMarker(Transform location)
    {
        GameObject go = Object.Instantiate(Resources.Load<GameObject>(uiElement), location);

        if (go != null)
        {
            return go;
        }
        else
        {
            Debug.LogWarning($"Unable to load resource {uiElement}");
            return null;
        }
    }

    public string GetName()
    {
        return uiElement;
    }
}