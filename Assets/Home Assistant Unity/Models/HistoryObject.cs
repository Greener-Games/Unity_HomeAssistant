using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Requests;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HistoryObject
{
    [OdinSerialize][NonSerialized][ShowInInspector][ReadOnly]
    List<StateObject> history = new List<StateObject>();

    public StateObject this[int index] => history[index];
    public int Count => history.Count;
    
    [ShowInInspector]
    public TimeSpan defaultHistoryTimeSpan = TimeSpan.FromDays(14);
    
    [OdinSerialize][NonSerialized][ShowInInspector][ReadOnly]
    internal bool isGeneratedData;
    
    public List<StateObject> AverageHour => ProcessDataAsFloats(this[0].lastChanged.RoundDown(TimeSpan.FromHours(1)), TimeSpan.FromHours(1));
    public List<StateObject> AverageDay => ProcessDataAsFloats(this[0].lastChanged.RoundDown(TimeSpan.FromDays(1)), TimeSpan.FromDays(1));
    public List<StateObject> AverageWeek => ProcessDataAsFloats(this[0].lastChanged.StartOfWeek(DayOfWeek.Monday), TimeSpan.FromDays(7));

    internal UnityEvent historyFetched = new UnityEvent();
    
    public void Add(StateObject item)
    {
        history.Add(item);
    }
    
    public async Task GetDataHistory(string entityId, TimeSpan timeSpan)
    {
        history = (await HistoryClient.GetHistory(entityId, timeSpan,false)).history;
        historyFetched?.Invoke();
    }
    
    List<StateObject> ProcessDataAsFloats(DateTime start, TimeSpan ts)
    {
        List<StateObject> returnStates = new List<StateObject>();
        List<float> inTime = new List<float>();

        DateTime currentTime = start;

        int totalProcessed = 0;
        
        foreach (StateObject stateObject in history)
        {
            if (float.TryParse(stateObject.state, out float f))
            {
                if (stateObject.lastChanged < currentTime.Add(ts))
                {
                    inTime.Add(f);
                }
                else
                {
                    StateObject so = new StateObject
                    {
                        lastChanged = currentTime, 
                        state = inTime.Average().ToString(CultureInfo.InvariantCulture)
                    };
                    returnStates.Add(so);
                    totalProcessed += inTime.Count;

                    inTime = new List<float>();
                    currentTime = currentTime.Add(ts);
                    inTime.Add(f);
                }
            }
        }

        //add the last element if needed as not a complete loop for bigger data sets
        if (totalProcessed != Count)
        {
            StateObject final = new StateObject
            {
                lastChanged = currentTime,
                state = inTime.Average().ToString(CultureInfo.InvariantCulture)
            };
            returnStates.Add(final);
        }

        return returnStates;
    }
}