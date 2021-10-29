using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class StateObject
{
    [JsonProperty("entity_id")]
    public string entityId;

    [JsonProperty("state")]
    public string state;

    [JsonProperty("attributes")]
    public Dictionary<string, object> attributes;

    /// <summary>
    ///  the last time there was a difference between the previous value and the new.
    /// </summary>
    [JsonProperty("last_changed")]
    public DateTime lastChanged;

    /// <summary>
    /// the last time an entity did send its value to HA
    /// </summary>
    [JsonProperty("last_updated")]
    public DateTime lastUpdated;

    [JsonProperty("context")]
    public ContextObject contextObject;


    public StateObject()
    {
        
    }

    public StateObject(string state)
    {
        this.state = state;
    }
    
    public StateObject(string state, DateTime lastUpdated)
    {
        this.state = state;
        this.lastChanged = lastUpdated;
        this.lastUpdated = lastUpdated;
    }
    
    public bool HasAttributeValue(string key)
    {
        return attributes != null && attributes.ContainsKey(key);
    }
    
    public T GetAttributeValue<T>(string key, T defaultIfNull = default(T))
    {
        if (attributes != null && attributes.ContainsKey(key))
        {
            try
            {
                return (T)attributes[key];
            }
            catch (Exception e)
            {
                Debug.LogWarning($"unable to cast {key} from {typeof(T)} to {attributes[key].GetType()}");
                return defaultIfNull;
            }
        }
        else
        {
            return defaultIfNull;
        }
    }
}