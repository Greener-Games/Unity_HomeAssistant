using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class WeatherEntity : Entity
{
    public float Temperature => (float)rawData.Attributes["temperature"];
    public float Humidity => (float)rawData.Attributes["humidity"];
    public float Pressure => (float)rawData.Attributes["pressure"];
    public float WindBearing => (float)rawData.Attributes["wind_bearing"];
    public float WindSpeed => (float)rawData.Attributes["wind_speed"];

    public JArray RawForecast => (JArray) rawData.Attributes["forecast"];
    public List<ForecastObject> forecast = new List<ForecastObject>();

    protected override async Task CustomFetchData()
    {
        foreach (JToken jToken in RawForecast)
        {
            JObject forecastRaw = (JObject) jToken;
            ForecastObject forecastObject = JsonConvert.DeserializeObject<ForecastObject>(forecastRaw.ToString());
            forecast.Add(forecastObject);
        }
    }
}