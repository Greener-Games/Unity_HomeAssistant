#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Requests;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

#endregion

[Serializable]
public class Entity : SerializedMonoBehaviour
{
    const string FriendlyNameKey = "friendly_name";

    [TabGroup("Data")]
    public string entityId;
    
    [TabGroup("Data")]
    public float refreshRateSeconds = 300;

    [TabGroup("Current")]
    [ShowInInspector]
    [ReadOnly]
    public string State => currentStateObject?.state;
    
    [TabGroup("Current")]
    [OdinSerialize]
    [NonSerialized]
    [ReadOnly]
    public StateObject currentStateObject = new StateObject();

    [TabGroup("Current")]
    public DateTime lastDataFetchTime;

    [TabGroup("Current")]
    [ShowInInspector]
    [ReadOnly]
    public string FriendlyName => currentStateObject.GetAttributeValue(FriendlyNameKey, entityId);

    [TabGroup("History")]
    public StateObject this[int index] => historyData[index];

    [TabGroup("History")][ReadOnly]
    public HistoryListObject historyData = new HistoryListObject();

    [TabGroup("History")]
    [ShowInInspector]
    public TimeSpan historyTimeSpan = TimeSpan.FromDays(14);

    [TabGroup("History")]
    [OdinSerialize]
    [NonSerialized]
    [ShowInInspector]
    [ReadOnly]
    internal bool isGeneratedData;

    //TODO: have the averages be able to work with different data types other than floats/ints
    [TabGroup("History")]
    public List<StateObject> AverageHour => historyData.ProcessDataAsFloats(this[0].lastChanged.RoundDown(TimeSpan.FromHours(1)), HistoryListObject.AverageTimeFrames.HOUR);

    [TabGroup("History")]
    public List<StateObject> AverageDay => historyData.ProcessDataAsFloats(this[0].lastChanged.RoundDown(TimeSpan.FromDays(1)), HistoryListObject.AverageTimeFrames.DAY);

    [TabGroup("History")]
    public List<StateObject> AverageWeek => historyData.ProcessDataAsFloats(this[0].lastChanged.StartOfWeek(DayOfWeek.Monday),  HistoryListObject.AverageTimeFrames.WEEK);
    

    [HideInInspector]
    public UnityAction<Entity> dataFetched;
    [HideInInspector]
    public UnityEvent historyFetched;

    protected virtual async void Start()
    {
        await FetchHistory();
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

        currentStateObject = await StateClientManager.GetState(entityId);
        lastDataFetchTime = DateTime.Now;
        
        if (historyData.Count == 0 || historyData[historyData.Count - 1] != currentStateObject)
        {
            historyData.Add(currentStateObject);
        }
        
        ProcessData();
        dataFetched?.Invoke(this);
    }

    protected virtual void ProcessData()
    {
      //handle any processing of data after fetching here  
    }

    [Button][TabGroup("History")]
    public virtual async Task FetchHistory()
    {
        await FetchHistory(historyTimeSpan);
    }

    public virtual async Task FetchHistory(TimeSpan timeSpan)
    {
        Debug.Log($"Fetching History for {entityId}");
        historyData = await HistoryClient.GetHistory(entityId, timeSpan, false);

        if (historyData.Count == 0 && SimulationData.SimulateData)
        {
            GenerateSimulationData();
        }
        
        historyFetched?.Invoke();
    }

    /// <summary>
    ///     Returns a human readable entity type value, can be overridden with a value in the HA config files
    /// </summary>
    /// <returns></returns>
    public string GetEntityType()
    {
        if (currentStateObject != null && currentStateObject.HasAttributeValue("entity_type"))
        {
            return currentStateObject.GetAttributeValue<string>("entity_type");
        }

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

    /// <summary>
    ///     Generate a series of fake data if the manager is set to do so, used for testing when the HA server is unreachable
    /// </summary>
    protected virtual void GenerateSimulationData()
    {
        historyData.GenerateSimulationInt(0, 50, historyTimeSpan);
        isGeneratedData = true;
        currentStateObject = historyData[0];
    }
}