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
    public float Temperature => (float)currentStateObject.attributes["temperature"];
    public float Humidity => currentStateObject.GetAttributeValue<float>("humidity");
    public float Pressure => (float)currentStateObject.attributes["pressure"];
    public float WindBearing => (float)currentStateObject.attributes["wind_bearing"];
    public float WindSpeed => (float)currentStateObject.attributes["wind_speed"];

    public JArray RawForecast => (JArray) currentStateObject.attributes["forecast"];
    public List<ForecastObject> forecast = new List<ForecastObject>();
    
    protected override async Task ProcessLiveDataPostFetch()
    {
        foreach (JToken jToken in RawForecast)
        {
            JObject forecastRaw = (JObject) jToken;
            ForecastObject forecastObject = JsonConvert.DeserializeObject<ForecastObject>(forecastRaw.ToString());
            forecast.Add(forecastObject);
        }
    }
    
    protected override void GenerateSimulationData()
    {
        historyObject.GenerateSimulationBool("on", "off");
        currentStateObject = historyObject[0];
    }
}