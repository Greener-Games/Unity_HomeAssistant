#region

using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

#endregion

public static class SimulationData
{
    public static void GenerateSimulationInt(this HistoryListObject historyListObject, int min, int max, TimeSpan historyTimeSpan)
    { 
        DateTimeOffset current = DateTimeOffset.Now - historyTimeSpan;
        DateTimeOffset end = DateTimeOffset.Now;
        while (current < end)
        {
            historyListObject.Add(new StateObject(Random.Range(min, max).ToString(), current.DateTime));
            current = current.AddHours(6);
        }
    }

    public static void GenerateSimulationInt(this HistoryListObject historyListObject,float min, float max , TimeSpan historyTimeSpan)
    {
        DateTimeOffset current = DateTimeOffset.Now - historyTimeSpan;
        DateTimeOffset end = DateTimeOffset.Now;
        while (current < end)
        {
            historyListObject.Add(new StateObject(Random.Range(min, max).ToString(), current.DateTime));
            current = current.AddHours(6);
        }
    }

    public static void GenerateSimulationBool(this HistoryListObject historyListObject,string trueValue, string falseValue, TimeSpan historyTimeSpan)
    {
        DateTimeOffset current = DateTimeOffset.Now - historyTimeSpan;
        DateTimeOffset end = DateTimeOffset.Now;
        while (current < end)
        {
            historyListObject.Add(new StateObject(Random.value >= 0.5f ? trueValue : falseValue, current.DateTime));
            current = current.AddHours(6);
        }
    }
}