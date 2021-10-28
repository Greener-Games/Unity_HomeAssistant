using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

/// <summary>
/// Represents a service domain definition in Home Assistant.
/// </summary>
public class ServiceDomainObject
{
    /// <summary>
    /// Gets the service domain's name.
    /// </summary>
    [JsonProperty("domain")]
    public string domain;

    /// <summary>
    /// Gets the list of services in this domain.
    /// </summary>
    [JsonProperty("services")]
    public Dictionary<string, ServiceObject> services;

    /// <summary>
    /// Lists all services within the domain
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> ServiceList => services.Select(s => $"{domain}.{s}");
    
}