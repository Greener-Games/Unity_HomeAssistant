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

    const float requestRefresh = 0.5f;
    bool isRequestingData;
    
    protected virtual async void Start()
    {
        await FetchHistory();
    }

    async void OnEnable()
    {
        await FetchLiveData();
        await StartRefreshLoop();
    }
    
    /// <summary>
    /// Draw a world marker for the gizmo at the location
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

    protected async Task EntityRequest(Task<StateObject> request, float refreshRate = requestRefresh)
    {
        if (isRequestingData)
        {
            //if we are mid request then dont send more data to avoid server conflicts
            return;
        }

        isRequestingData = true;
        StateObject data = await request;
        
        if (data != null)
        {
            currentStateObject = data;
            dataFetched?.Invoke(this);
        }
        else
        {
            //if we dont get any data back, just wait and fetch the latest to ensure we are correct
            await new WaitForSeconds(refreshRate);
            await FetchLiveData();
        }

        isRequestingData = false;
    }
}