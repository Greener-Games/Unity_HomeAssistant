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
    public float Temperature => (float)rawData.attributes["temperature"];
    public float Humidity => (float)rawData.attributes["humidity"];
    public float Pressure => (float)rawData.attributes["pressure"];
    public float WindBearing => (float)rawData.attributes["wind_bearing"];
    public float WindSpeed => (float)rawData.attributes["wind_speed"];

    public JArray RawForecast => (JArray) rawData.attributes["forecast"];
    public List<ForecastObject> forecast = new List<ForecastObject>();

    void Awake()
    {
        dataFetched += DataFetched;
    }

    void DataFetched()
    {
        foreach (JToken jToken in RawForecast)
        {
            JObject forecastRaw = (JObject) jToken;
            ForecastObject forecastObject = JsonConvert.DeserializeObject<ForecastObject>(forecastRaw.ToString());
            forecast.Add(forecastObject);
        }
    }
}