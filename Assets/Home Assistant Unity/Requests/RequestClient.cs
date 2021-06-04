using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GG.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Networking;

public class RequestClient : MonoBehaviour
{
    /// <summary>
    /// Returns a message if the API is up and running.
    /// </summary>
    /// <returns>A <see cref="ConfigObject" />.</returns>
    public static async Task<bool> IsRunning()
    {
        var result = await Get<MessageObject>("api/");
        return result.Message == "API running.";
    }
    
    /// <summary>
    /// Returns the current configuration
    /// </summary>
    /// <returns>A <see cref="ConfigObject" />.</returns>
    public static async Task<ConfigObject> GetConfiguration() => await Get<ConfigObject>("api/config");
    
    
    
    /// <summary>
    /// Performs a GET request
    /// </summary>
    /// <param name="path">API Endpoint</param>
    /// <typeparam name="T">Type of data expected on the return</typeparam>
    /// <returns></returns>
    public static async Task<T> Get<T>(string path) where T : class
    {
        using (UnityWebRequest request = UnityWebRequest.Get(HomeAssistantManager._hostAddress + path))
        {
            request.SetRequestHeader("Authorization", "Bearer " + HomeAssistantManager._apiKey);

            await request.SendWebRequest();

            if (!request.isHttpError)
            {
                try
                {
                    Debug.Log(request.uri);
                    return JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                }
                catch (Exception e)
                {
                    Debug.LogException(new Exception($"Web Request Error in {request.uri} : Error {request.error}"));
                }
            }
            else
            {
                Debug.LogException(new Exception($"Web Request Error in {request.uri} : Error {request.error}"));
            }
        }

        return default;
    }

    /// <summary>
    /// Performs A Post Request
    /// </summary>
    /// <param name="path">API Endpoint</param>
    /// <typeparam name="T">Type expected back</typeparam>
    /// <returns></returns>
    public static async Task<T> Post<T>(string path) where T : class
    {
        return await Post<T>(path, "");
    }
    
    /*/// <summary>
    /// Performs A Post Request
    /// </summary>
    /// <param name="path">API Endpoint</param>
    /// <param name="body">Content within the post request</param>
    /// <typeparam name="T">Type expected back</typeparam>
    /// <returns></returns>
    public static async Task<T> Post<T>(string path, Dictionary<string, object> body) where T : class
    {
        return await Post<T>(path, JsonConvert.SerializeObject(body));
    }*/

    /// <summary>
    /// Performs A Post Request
    /// </summary>
    /// <param name="path">API Endpoint</param>
    /// <param name="bodyRawontent within the post request</param>
    /// <typeparam name="T">Type expected back</typeparam>
    /// <returns></returns>
    public static async Task<T> Post<T>(string path, object bodyRaw) where T : class
    {
        using (UnityWebRequest request = UnityWebRequest.Post(HomeAssistantManager._hostAddress + path,"")) //No data passed here as need json and have to do manually
        {
            request.SetRequestHeader("Authorization", "Bearer " + HomeAssistantManager._apiKey);

            //Convert and add the body if submitted
            if (bodyRaw != null)
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

                byte[] bodyBytes = Encoding.UTF8.GetBytes(body);
                request.uploadHandler = new UploadHandlerRaw(bodyBytes);
                request.SetRequestHeader("Content-Type", "application/json");
            }

            
            await request.SendWebRequest();

            if (!request.isHttpError)
            {
                JToken token = JToken.Parse(request.downloadHandler.text);
                
                if (token is JArray && typeof(T).IsArray) //is array and expecting array all good
                {
                    return JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                }
                else if ((token is JArray) && !typeof(T).IsArray) // is an array and not expecting one just return the first
                {
                    return JsonConvert.DeserializeObject<List<T>>(request.downloadHandler.text).First();
                }
                else //Return the object as expected
                {
                    return JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                }
            }
            else
            {
                throw new Exception($"Web Request Error in {request.uri} : Error {request.error}");
            }
        }
    }
}