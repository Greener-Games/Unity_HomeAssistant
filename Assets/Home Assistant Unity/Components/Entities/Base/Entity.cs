#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GG.Extensions;
using Requests;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

#endregion

[Serializable]
[EntityWorldGraphic("World Marker")]
public partial class Entity : SerializedMonoBehaviour
{
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
    
    [HideInInspector]
    public UnityAction<Entity> dataFetched;

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
    
    /// <summary>
    ///     Generate a series of fake data if the manager is set to do so, used for testing when the HA server is unreachable
    /// </summary>
    protected virtual void GenerateSimulationData()
    {
        historyData.GenerateSimulationInt(0, 50, HistoryTimeSpan);
        isGeneratedData = true;
        currentStateObject = historyData[0];
    }

    /// <summary>
    /// Draw a world marker for the gizmo at the location
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}