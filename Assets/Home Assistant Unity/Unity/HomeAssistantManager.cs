using UnityEngine;


public class HomeAssistantManager : MonoBehaviour
{
    internal static string hostAddress;
    internal static string apiKey;
    
    /// <summary>
    /// Initialise the client for all future requests
    /// </summary>
    /// <param name="address">Home Assistant web address 192.168.X.X:XXXX</param>
    /// <param name="accessToken">The Home Assistant access token.</param>
    public static void Initialize(string address, string accessToken)
    {
        hostAddress = address;
        apiKey = accessToken;
    }
}