using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class StateObject
{
    [JsonProperty("entity_id")]
    public string EntityId { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("attributes")]
    public Dictionary<string,string> Attributes { get; set; }

    [JsonProperty("last_changed")]
    public DateTimeOffset LastChanged { get; set; }

    [JsonProperty("last_updated")]
    public DateTimeOffset LastUpdated { get; set; }

    [JsonProperty("context")]
    public ContextObject ContextObject { get; set; }
}