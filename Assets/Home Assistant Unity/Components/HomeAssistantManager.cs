#region

using System.Collections;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

#endregion

[DefaultExecutionOrder(-1000)]
public class HomeAssistantManager : MonoBehaviour
{
    public string address;
    public string longLifeToken;

    public bool generateSimulatedData;

    const string configFile = "HA_Config.json";
    string fileLocation => Path.Combine(Application.streamingAssetsPath, configFile);

    string environment = "environment";
    void Awake()
    {
        StartCoroutine(LoadData());
    }

    IEnumerator LoadData()
    {
        string dataAsJson;
        if (fileLocation.Contains ("://") || fileLocation.Contains (":///")) {
            //debugText.text += System.Environment.NewLine + filePath;
            Debug.Log ("UNITY:" + System.Environment.NewLine + fileLocation);
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get (fileLocation);
            yield return www.Send ();
            dataAsJson = www.downloadHandler.text;
        } else {
            dataAsJson = File.ReadAllText (fileLocation);
        }
        
        HomeAssistantUnityConfig config = JsonConvert.DeserializeObject<HomeAssistantUnityConfig>(dataAsJson);
        address = config.address;
        longLifeToken = config.longLifeToken;
        
        SimulationData.Initialise(generateSimulatedData);
        ClientManager.Initialise(address, longLifeToken);

        SceneManager.LoadScene(environment, LoadSceneMode.Additive);
    }
}