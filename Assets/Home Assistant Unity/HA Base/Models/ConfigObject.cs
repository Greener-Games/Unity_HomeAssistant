#region

using System.Collections.Generic;
using Newtonsoft.Json;

#endregion


public class ConfigObject
{
    [JsonProperty("latitude")]
    public double Latitude { get; set; }

    [JsonProperty("longitude")]
    public double Longitude { get; set; }

    [JsonProperty("elevation")]
    public long Elevation { get; set; }

    [JsonProperty("unit_system")]
    public UnitSystemObject UnitSystemObject { get; set; }

    [JsonProperty("location_name")]
    public string LocationName { get; set; }

    [JsonProperty("time_zone")]
    public string TimeZone { get; set; }

    [JsonProperty("components")]
    public List<string> Components { get; set; }

    [JsonProperty("config_dir")]
    public string ConfigDir { get; set; }

    [JsonProperty("whitelist_external_dirs")]
    public List<string> WhitelistExternalDirs { get; set; }

    [JsonProperty("allowlist_external_dirs")]
    public List<string> AllowlistExternalDirs { get; set; }

    [JsonProperty("allowlist_external_urls")]
    public List<object> AllowlistExternalUrls { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("config_source")]
    public string ConfigSource { get; set; }

    [JsonProperty("safe_mode")]
    public bool SafeMode { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("external_url")]
    public object ExternalUrl { get; set; }

    [JsonProperty("internal_url")]
    public object InternalUrl { get; set; }
}