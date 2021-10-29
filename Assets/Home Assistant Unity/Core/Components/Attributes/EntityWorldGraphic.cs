using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class EntityWorldGraphic :Attribute
{
    readonly string worldMarker;

    public EntityWorldGraphic(string worldMarker)
    {
        this.worldMarker = worldMarker;
    }

    public GameObject LoadMarker(Transform location)
    {
        GameObject go = Object.Instantiate(Resources.Load<GameObject>(worldMarker), location);

        if (go != null)
        {
            return go;
        }
        else
        {
            Debug.LogWarning($"Unable to load resource {worldMarker}");
            return null;
        }
    }
}