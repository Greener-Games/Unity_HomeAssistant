using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class StateObject
{
    [JsonProperty("entity_id")]
    public string EntityId;

    [JsonProperty("state")]
    public string State;

    [JsonProperty("attributes")]
    public Dictionary<string, dynamic> Attributes;

    [JsonProperty("last_changed")]
    public DateTime LastChanged;

    [JsonProperty("last_updated")]
    public DateTime LastUpdated;

    [JsonProperty("context")]
    public ContextObject ContextObject;
}