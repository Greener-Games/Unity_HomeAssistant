using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

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

    [JsonProperty("datetime")]
    public DateTimeOffset Datetime;

    [JsonIgnore]
    public string dateTimeReadable; 
    
    [JsonProperty("wind_bearing")]
    public double WindBearing;

    [JsonProperty("wind_speed")]
    public double WindSpeed;
    
    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        dateTimeReadable = Datetime.ToString();
    }
}