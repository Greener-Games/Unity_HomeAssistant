using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class HistoryListObject : List<StateObject>
{
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
    
    public List<StateObject> ProcessDataAsFloats(DateTime start, TimeSpan ts)
    {
        List<StateObject> returnStates = new List<StateObject>();
        List<float> inTime = new List<float>();

        DateTime currentTime = start;

        int totalProcessed = 0;
        
        foreach (StateObject stateObject in this)
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