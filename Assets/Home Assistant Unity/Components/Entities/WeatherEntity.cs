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
    [ShowInInspector][TabGroup("Current")]public float Temperature => currentStateObject.GetAttributeValue("temperature", 0);
    [ShowInInspector][TabGroup("Current")]public float Humidity => currentStateObject.GetAttributeValue("humidity", 0);
    [ShowInInspector][TabGroup("Current")]public float Pressure => currentStateObject.GetAttributeValue("pressure", 0);
    [ShowInInspector][TabGroup("Current")]public float WindBearing => currentStateObject.GetAttributeValue("wind_bearing", 0);
    [ShowInInspector][TabGroup("Current")]public float WindSpeed => currentStateObject.GetAttributeValue("wind_speed", 0);
    JArray RawForecast => currentStateObject.GetAttributeValue<JArray>("forecast", new JArray());
    
    [TabGroup("Current")][ReadOnly]
    public List<ForecastObject> forecast = new List<ForecastObject>();
    
    protected override void ProcessData()
    {
        
        foreach (JToken jToken in RawForecast)
        {
            JObject forecastRaw = (JObject) jToken;
            ForecastObject forecastObject = JsonConvert.DeserializeObject<ForecastObject>(forecastRaw.ToString());
            forecast.Add(forecastObject);
        }
    }
    
    protected override void GenerateHistoricSimulationData()
    {
        historyData.GenerateSimulationBool("on", "off", HistoryTimeSpan);
        currentStateObject = historyData[0];
        isGeneratedData = true;
    }
}