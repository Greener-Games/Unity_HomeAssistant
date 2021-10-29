using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GG.Extensions;
using UnityEngine;

public class HistoryListObject : List<StateObject>
{
    public enum AverageTimeFrames
    {
        HOUR,
        DAY,
        WEEK,
        MONTH,
        QUATER,
        YEAR
    }
    
    /// <summary>
    /// Gets the EntityId for the state objects in this list.
    /// </summary>
    public string EntityId => Count > 0 ? this[0].entityId : null;

    /// <summary>
    /// Gets the earliest point in time represented by this history list.
    /// </summary>
    public DateTimeOffset DateFrom => Count > 0 ? this.OrderBy(s => s.lastUpdated).First().lastUpdated : DateTimeOffset.MinValue;

    /// <summary>
    /// Gets the most recent point in time represented by this history list.
    /// </summary>
    public DateTimeOffset DateTo => Count > 0 ? this.OrderByDescending(s => s.lastUpdated).First().lastUpdated : DateTimeOffset.MaxValue;

    /// <summary>
    /// Gets a string representation of this object.
    /// </summary>
    public override string ToString() => $"Historical state: {EntityId} - {Count} state(s)";


    public List<StateObject> ProcessDataAsFloats(DateTime start, AverageTimeFrames timeFrames)
    {
        switch (timeFrames)
        {
            case AverageTimeFrames.HOUR:
                return ProcessDataAsFloats(start, TimeSpan.FromHours(1));
            case AverageTimeFrames.DAY:
                return ProcessDataAsFloats(start, TimeSpan.FromDays(1));
            case AverageTimeFrames.WEEK:
                return ProcessDataAsFloats(start, TimeSpan.FromDays(7));
            case AverageTimeFrames.MONTH:
            case AverageTimeFrames.QUATER:
            case AverageTimeFrames.YEAR:
            default:
                throw new ArgumentOutOfRangeException(nameof(timeFrames), timeFrames, null);
        }

        return new List<StateObject>();
    }

    public List<StateObject> ProcessDataAsFloats(DateTime start, TimeSpan ts)
    {
        List<StateObject> returnStates = new List<StateObject>();
        
        DateTime currentTime = start;
        
        int currentIndex = 0;
        
        //TODO:try and remove the for loops for sanity
        while (currentTime < DateTime.Now)
        {
            List<float> inTime = new List<float>();
            bool include = true;
            
            while (include && currentIndex < Count)
            {
                if (this[currentIndex].lastChanged > currentTime && this[currentIndex].lastChanged < currentTime.Add(ts))
                {
                    if (float.TryParse(this[currentIndex].state, out float f))
                    {
                        inTime.Add(f);
                    }

                    currentIndex++;
                }
                else
                {
                    include = false;
                }
            }
                            
            if (inTime.Count > 0)
            {
                StateObject so = new StateObject
                {
                    lastChanged = currentTime, 
                    state = inTime.Average().ToString("F2")
                };
                
                returnStates.Add(so);
            }
            else
            {
                StateObject so = new StateObject
                {
                    lastChanged = currentTime, 
                    state = int.MinValue.ToString()
                };
                
                returnStates.Add(so);
            }
                
            currentTime = currentTime.Add(ts);
        }
        return returnStates;
    }
}