using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class HomeAssistantManager : MonoBehaviour
{
    public string address;
    public string longLifeToken;
    
    public static string _hostAddress;
    public static string _apiKey;
    
    void Awake()
    {
        Initialize(address, longLifeToken);
    }
    
    /// <summary>
    /// Initialise the client for all future requests
    /// </summary>
    /// <param name="address">Home Assistant web address 192.168.X.X:XXXX</param>
    /// <param name="accessToken">The Home Assistant access token.</param>
    public static void Initialize(string address, string accessToken)
    {
        _hostAddress = address;
        _apiKey = accessToken;
    }
}