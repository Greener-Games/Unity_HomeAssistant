using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[System.Serializable]
public class HistoryObject
{ 
    [OdinSerialize][NonSerialized][ShowInInspector]
    public List<StateObject> history;

    public List<StateObject> AverageHour => ProcessData(history[0].LastUpdated.RoundDown(TimeSpan.FromHours(1)), TimeSpan.FromHours(1));

    public List<StateObject> AverageDay => ProcessData(history[0].LastUpdated.RoundDown(TimeSpan.FromDays(1)), TimeSpan.FromDays(1));

    public List<StateObject> AverageWeek => ProcessData(history[0].LastUpdated.StartOfWeek(DayOfWeek.Monday), TimeSpan.FromDays(7));

    List<StateObject> ProcessData(DateTime start, TimeSpan ts)
    {
        List<StateObject> returnStates = new List<StateObject>();
        List<float> inTime = new List<float>();

        DateTime currentTime = start;

        int totalProcessed = 0;
        
        foreach (StateObject stateObject in history)
        {
            if (float.TryParse(stateObject.State, out float f))
            {
                if (stateObject.LastUpdated < currentTime.Add(ts))
                {
                    inTime.Add(f);
                }
                else
                {
                    StateObject so = new StateObject
                    {
                        LastUpdated = currentTime, 
                        State = inTime.Average().ToString(CultureInfo.InvariantCulture)
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
                LastUpdated = currentTime,
                State = inTime.Average().ToString(CultureInfo.InvariantCulture)
            };
            returnStates.Add(final);
        }

        return returnStates;
    }
}