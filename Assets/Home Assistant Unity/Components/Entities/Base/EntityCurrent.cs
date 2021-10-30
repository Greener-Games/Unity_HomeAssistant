using System;
using System.Threading.Tasks;
using Requests;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

public partial class Entity
{
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

    public UnityAction<Entity> dataFetched;

    async Task StartRefreshLoop()
    {
        while (gameObject.activeInHierarchy && enabled)
        {
            await new WaitForSeconds(refreshRateSeconds);
            await FetchLiveData();
        }
    }

    [Button]
    [TabGroup("Current")]
    public virtual async Task FetchLiveData()
    {
        Debug.Log($"Fetching Data {entityId}");

        currentStateObject = await StateClient.GetState(entityId);
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
}