using System.Collections.Generic;
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
    
    void Awake()
    {
        ClientManager.Initialise(address, longLifeToken);
    }
}