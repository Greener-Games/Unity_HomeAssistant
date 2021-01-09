using System;
using System.Collections;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class Entity : SerializedMonoBehaviour
{
    public string entityId;
    
    [OdinSerialize][NonSerialized]
    public StateObject rawData;

    public UnityAction dataFetched;

    public float refreshRateSeconds;
    public DateTime lastDataFetchTime;

    public string State => rawData.State;
    
    void OnEnable()
    {
        FetchData();
        
        StartRefreshLoop();
    }

    async Task StartRefreshLoop()
    {
        while (isActiveAndEnabled)
        {
            await new WaitForSeconds(refreshRateSeconds);
            await FetchData();
        }
    }

    public virtual async Task FetchData()
    {
        Debug.Log($"Fetching Data {entityId}");
        await StartFetchData();
    }
    
    async Task StartFetchData()
    {
        rawData = await RequestClient.GetState(entityId);
        lastDataFetchTime = DateTime.Now;
        
        await ProcessFetchedData();
        dataFetched?.Invoke();
    }

    protected virtual async Task ProcessFetchedData()
    {
        await Task.Delay(0);
    }

}