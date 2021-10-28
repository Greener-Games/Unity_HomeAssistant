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
    
    public bool generateFakeData;
    
    public static bool _generateFakeData;
    
    void Awake()
    {
        Initialize(address, longLifeToken, generateFakeData);
    }
    
    /// <summary>
    /// Initialise the client for all future requests
    /// </summary>
    /// <param name="address">Home Assistant web address 192.168.X.X:XXXX</param>
    /// <param name="accessToken">The Home Assistant access token.</param>
    public static void Initialize(string address, string accessToken, bool generateData)
    {
        ClientManager.Initialise(address, accessToken);
        _generateFakeData = generateData;
    }
}