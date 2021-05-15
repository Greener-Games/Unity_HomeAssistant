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
    //TODO: handle different history values other than float/ints

    [ShowInInspector]public TimeSpan defaultHistoryTimeSpan = TimeSpan.FromDays(14);

    [OdinSerialize][NonSerialized][ShowInInspector][ReadOnly]
    public bool isGeneratedData;
    
    [OdinSerialize][NonSerialized][ShowInInspector][ReadOnly]
    public List<StateObject> history = new List<StateObject>();

    
    public List<StateObject> AverageHour => ProcessDataAsFloats(history[0].lastChanged.RoundDown(TimeSpan.FromHours(1)), TimeSpan.FromHours(1));
    public List<StateObject> AverageDay => ProcessDataAsFloats(history[0].lastChanged.RoundDown(TimeSpan.FromDays(1)), TimeSpan.FromDays(1));
    public List<StateObject> AverageWeek => ProcessDataAsFloats(history[0].lastChanged.StartOfWeek(DayOfWeek.Monday), TimeSpan.FromDays(7));

    internal UnityEvent historyFetched = new UnityEvent();

    
    public async Task GetDataHistory(string entityId, TimeSpan timeSpan)
    {
        history = await HistoryRequest.GetHistory(entityId, timeSpan,true);
        
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
        if (totalProcessed != history.Count)
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