using Newtonsoft.Json;

/// <summary>
/// Represents a signle field in a service call.
/// </summary>
public class ServiceFieldObject
{
    /// <summary>
    /// The description of the field
    /// </summary>
    [JsonProperty("name")]
    public string name;
    
    /// <summary>
    /// The description of the field.
    /// </summary>
    [JsonProperty("description")]
    public string description;

    /// <summary>
    /// Gets or sets the example text for this field
    /// </summary>
    [JsonProperty("example")]
    public string Example;
}