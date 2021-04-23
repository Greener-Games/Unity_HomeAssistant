using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Serialization;

[System.Serializable]
public class StateObject
{
    [JsonProperty("entity_id")]
    public string entityId;

    [JsonProperty("state")]
    public string state;

    [JsonProperty("attributes")]
    public Dictionary<string, dynamic> attributes;

    /// <summary>
    ///  the last time there was a difference between the previous value and the new.
    /// </summary>
    [JsonProperty("last_changed")][CustomDateTimeViewer("dd/MM/yy HH:mm:ss")]
    public DateTime lastChanged;

    /// <summary>
    /// the last time an entity did send its value to HA
    /// </summary>
    [JsonProperty("last_updated")][CustomDateTimeViewer("dd/MM/yy HH:mm:ss")]
    public DateTime lastUpdated;

    [JsonProperty("context")]
    public ContextObject contextObject;

    
    public bool HasAttributeValue(string key)
    {
        return attributes != null && attributes.ContainsKey(key);
    }
    
    public T GetAttributeValue<T>(string key)
    {
        if (attributes != null && attributes.ContainsKey(key))
        {
            return (T)attributes[key];
        }
        else
        {
            return default(T);
        }
    }
}