using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Requests;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[DefaultExecutionOrder(-1000)]
public class HomeAssistantManager : MonoBehaviour
{
    public string address;
    public string longLifeToken;
    
    public bool generateSimulatedData;

    const string configFile = "HA_Config.json";
            string fileLocation => Path.Combine(Application.streamingAssetsPath, configFile);
            
    void Awake()
    {
        if (File.Exists(fileLocation))
        {
            string json = File.ReadAllText(fileLocation);
            HomeAssistantUnityConfig config = JsonConvert.DeserializeObject<HomeAssistantUnityConfig>(json);
            address = config.address;
            longLifeToken = config.longLifeToken;
        }
        
        SimulationData.Initialise(generateSimulatedData);
        ClientManager.Initialise(address, longLifeToken);
    }
}