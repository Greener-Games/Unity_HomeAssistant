using System;
using System.Collections;
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
    public string FriendlyName => rawData != null ? rawData.GetValue<string>("friendly_name") : "";


    [OdinSerialize][NonSerialized][ReadOnly]
    public StateObject rawData = new StateObject();

    public UnityAction<Entity> dataFetched;

    public float refreshRateSeconds = 300;
    
    [CustomDateTimeViewer("dd/MM/yy HH:mm:ss")]
    public DateTime lastDataFetchTime;

    public string State => rawData.state;
    

    public HistoryObject historyObject = new HistoryObject();
    public UnityEvent HistoryFetched => historyObject.historyFetched;

    async void Start()
    {
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
        
        rawData = await RequestClient.GetState(entityId);
        lastDataFetchTime = DateTime.Now;

        await ProcessData();
        dataFetched?.Invoke(this);
    }

    protected virtual async Task ProcessData()
    { }
    
    public virtual async Task FetchHistory(TimeSpan timeSpan)
    {
        Debug.Log($"Fetching History for {entityId}");

        await historyObject.GetDataHistory(entityId,timeSpan);
    }
}