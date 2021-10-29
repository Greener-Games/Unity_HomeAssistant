using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// Represents a single service definition.
/// </summary>
public class ServiceObject
{
    /// <summary>
    /// The description of the service object.
    /// </summary>
    [JsonProperty("name")]
    public string name;
    
    /// <summary>
    /// The description of the service object.
    /// </summary>
    [JsonProperty("description")]
    public string description;

    /// <summary>
    /// The fields/parameters that the service supports.
    /// </summary>
    [JsonProperty("fields")]
    public Dictionary<string, ServiceFieldObject> fields;
}