using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Entity : SerializedMonoBehaviour
{
    public string entityId;

    [ShowInInspector]
    [ReadOnly]
    public string FriendlyName => currentStateObject != null ? currentStateObject.GetAttributeValue<string>("friendly_name") : "";


    [OdinSerialize][NonSerialized][ReadOnly]
    public StateObject currentStateObject = new StateObject();

    public UnityAction<Entity> dataFetched;

    public float refreshRateSeconds = 300;
    
    public DateTime lastDataFetchTime;

    public string State => currentStateObject?.state;
    

    public HistoryObject historyObject = new HistoryObject();
    public UnityEvent HistoryFetched => historyObject.historyFetched;

    async void Start()
    {
        HistoryFetched.AddListener(GenerateSimulationData);
        await FetchHistory(historyObject.defaultHistoryTimeSpan);
    }

    async void OnEnable()
    {
       await FetchLiveData();
       await StartRefreshLoop();
    }

    async Task StartRefreshLoop()
    {
        while (gameObject.activeInHierarchy && enabled)
        {
            await new WaitForSeconds(refreshRateSeconds);
            await FetchLiveData();
        }
    }

    public virtual async Task FetchLiveData()
    {
        Debug.Log($"Fetching Data {entityId}");
        
        currentStateObject = await RequestClient.GetState(entityId);
        lastDataFetchTime = DateTime.Now;

        await ProcessLiveDataPostFetch();
        dataFetched?.Invoke(this);

        if (historyObject.history.Count == 0 || historyObject.history[historyObject.history.Count - 1] != currentStateObject)
        {
            historyObject.history.Add(currentStateObject);
        }
    }

    protected virtual async Task ProcessLiveDataPostFetch()
    {
        //TODO: can we remove this
    }
    
    public virtual async Task FetchHistory(TimeSpan timeSpan)
    {
        Debug.Log($"Fetching History for {entityId}");
        await historyObject.GetDataHistory(entityId,timeSpan);
    }

    /// <summary>
    /// Returns a human readable entity type value, can be overridden with a value in the HA config files
    /// </summary>
    /// <returns></returns>
    public string GetEntityType()
    {
        if (currentStateObject != null && currentStateObject.HasAttributeValue("entity_type"))
        {
            return currentStateObject.GetAttributeValue<string>("entity_type");
        }
        else
        {
            switch (this)
            {
                case LightEntity _:
                    return "Light";
                case SensorEntity _:
                    return "Sensor";
                default:
                    return "Undefined";
            }
        }
    }

    /// <summary>
    /// Generate a series of fake data if the manager is set to do so, used for testing when the HA server is unreachable
    /// </summary>
    protected virtual void GenerateSimulationData()
    {
        if (historyObject.history.Count == 0 && HomeAssistantManager._generateFakeData)
        {
            historyObject.GenerateSimulationInt(0, 50);
            historyObject.isGeneratedData = true;
            currentStateObject = historyObject.history[0];
        }
    }
}