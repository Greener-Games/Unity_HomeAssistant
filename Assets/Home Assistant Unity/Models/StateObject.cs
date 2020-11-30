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
    public Dictionary<string, string> Attributes;

    [JsonProperty("last_changed")]
    public DateTimeOffset LastChanged;

    [JsonProperty("last_updated")]
    public DateTimeOffset LastUpdated;

    [JsonProperty("context")]
    public ContextObject ContextObject;
}