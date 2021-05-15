using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;

[System.Serializable]
public class ForecastObject
{
    [JsonProperty("condition")]
    public string Condition;

    [JsonProperty("precipitation")]
    public double Precipitation;

    [JsonProperty("temperature")]
    public double Temperature;

    [JsonProperty("templow")]
    public double Templow;

    [JsonProperty("datetime")][ShowInInspector]
    public DateTimeOffset Datetime;

    [JsonProperty("wind_bearing")]
    public double WindBearing;

    [JsonProperty("wind_speed")]
    public double WindSpeed;
}