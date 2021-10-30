using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Requests;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public partial class Entity
{
    [HideInInspector]
    public UnityEvent historyFetched;
    
    [TabGroup("History")]
    public StateObject this[int index] => historyData[index];

    [TabGroup("History")][ReadOnly]
    public HistoryListObject historyData = new HistoryListObject();

    [TabGroup("History")]
    public int historyDays =0;
    [TabGroup("History")]
    public int historyHours = 14;
    public TimeSpan HistoryTimeSpan => new TimeSpan(historyDays, historyHours,0,0);

    [TabGroup("History")]
    [ReadOnly]
    internal bool isGeneratedData;

    //TODO: have the averages be able to work with different data types other than floats/ints
    [TabGroup("History")]
    public List<StateObject> AverageHour => historyData.ProcessDataAsFloats(this[0].lastChanged.RoundDown(TimeSpan.FromHours(1)), HistoryListObject.AverageTimeFrames.HOUR);

    [TabGroup("History")]
    public List<StateObject> AverageDay => historyData.ProcessDataAsFloats(this[0].lastChanged.RoundDown(TimeSpan.FromDays(1)), HistoryListObject.AverageTimeFrames.DAY);

    [TabGroup("History")]
    public List<StateObject> AverageWeek => historyData.ProcessDataAsFloats(this[0].lastChanged.StartOfWeek(DayOfWeek.Monday),  HistoryListObject.AverageTimeFrames.WEEK);
    
    [Button][TabGroup("History")]
    public virtual async Task FetchHistory()
    {
        await FetchHistory(HistoryTimeSpan);
    }

    public virtual async Task FetchHistory(TimeSpan timeSpan)
    {
        Debug.Log($"Fetching History for {entityId}");
        historyData = await HistoryClient.GetHistory(entityId, timeSpan, false);

        if (historyData.Count == 0 && SimulationData.SimulateData)
        {
            GenerateHistoricSimulationData();
        }
        
        historyFetched?.Invoke();
    }
    
    /// <summary>
    ///     Generate a series of fake data if the manager is set to do so, used for testing when the HA server is unreachable
    /// </summary>
    protected virtual void GenerateHistoricSimulationData()
    {
        historyData.GenerateSimulationInt(0, 50, HistoryTimeSpan);
        isGeneratedData = true;
        currentStateObject = historyData[0];
    }
}
