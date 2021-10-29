using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ClientManager
{
    public static bool isInitialized;
    public static HttpClient client;

    /// <summary>
    /// Initializes the client.
    /// </summary>
    /// <param name="basePath">The Home Assistant base instance address.</param>
    /// <param name="accessToken">The Home Assistant long-lived access token.</param>
    public static void Initialise(string basePath, string accessToken) => Initialise(new Uri(basePath), accessToken);
    
    /// <summary>
    /// Initializes the client.
    /// </summary>
    /// <param name="basePath">The Home Assistant base instance address.</param>
    /// <param name="accessToken">The Home Assistant long-lived access token.</param>
    public static void Initialise(Uri basePath, string accessToken)
    {
        client = new HttpClient
        {
            BaseAddress = basePath,
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", accessToken),
                AcceptEncoding =
                {
                    new StringWithQualityHeaderValue("identity")
                }
            }
        };
        isInitialized = true;
    }
    
    /// <summary>
    /// Performs a GET request
    /// </summary>
    /// <param name="path">path for the request</param>
    /// <typeparam name="T">Type of data expected on the return</typeparam>
    /// <returns></returns>
    public static async Task<T> Get<T>(string path) where T : class
    {
        HttpResponseMessage request = await client.GetAsync(path);
        request.EnsureSuccessStatusCode();
        
        string responseContent = await request.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseContent);
    }

    /// <summary>
    /// Performs A POST Request to a path
    /// </summary>
    /// <param name="path">path for the request</param>
    /// <typeparam name="T">Type of data expected on the return</typeparam>
    /// <returns></returns>
    public static async Task<T> Post<T>(string path) where T : class
    {
        return await Post<T>(path, "");
    }

    /// <summary>
    /// Performs A POST Request to a path
    /// </summary>
    /// <param name="path">path for the request</param>
    /// <param name="bodyRaw">body contents of the request, if provided as an object will convert to json body</param>
    /// <typeparam name="T">Type of data expected on the return</typeparam>
    /// <returns></returns>
    public static async Task<T> Post<T>(string path, object bodyRaw) where T : class
    {
        string body = "";
        if (bodyRaw is string s)
        {
            body = s;
        }
        else
        {
            body = JsonConvert.SerializeObject(bodyRaw);
        }

        HttpResponseMessage request = await client.PostAsync(path, new StringContent(body, Encoding.UTF8, "application/json"));
        request.EnsureSuccessStatusCode();

        string responseContent = await request.Content?.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(responseContent))
        {
            return default(T);
        }
        
        JToken token = JToken.Parse(responseContent);
        switch (token)
        {
            //is array and expecting array all good
            case JArray _ when typeof(T).IsArray:
                return JsonConvert.DeserializeObject<T>(responseContent);
            // is an array and not expecting one just return the first
            case JArray _ when !typeof(T).IsArray:
                try
                {
                    return JsonConvert.DeserializeObject<List<T>>(responseContent).First();
                }
                catch (Exception e)
                {
                    return default(T);
                }
            //Return the object as expected
            default:
                return JsonConvert.DeserializeObject<T>(responseContent);
        }
    }
}