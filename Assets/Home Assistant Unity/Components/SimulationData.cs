#region

using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

#endregion

public static class SimulationData
{
    public static void GenerateSimulationInt(this HistoryObject historyObject, int min, int max)
    {
        historyObject.isGeneratedData = true;
        
        DateTimeOffset current = DateTimeOffset.Now - historyObject.defaultHistoryTimeSpan;
        DateTimeOffset end = DateTimeOffset.Now;
        while (current < end)
        {
            historyObject.Add(new StateObject(Random.Range(min, max).ToString(), current.DateTime));
            current = current.AddHours(6);
        }
    }

    public static void GenerateSimulationInt(this HistoryObject historyObject, float min, float max)
    {
        historyObject.isGeneratedData = true;

        DateTimeOffset current = DateTimeOffset.Now - historyObject.defaultHistoryTimeSpan;
        DateTimeOffset end = DateTimeOffset.Now;
        while (current < end)
        {
            historyObject.Add(new StateObject(Random.Range(min, max).ToString(), current.DateTime));
            current = current.AddHours(6);
        }
    }

    public static void GenerateSimulationBool(this HistoryObject historyObject, string trueValue, string falseValue)
    {
        historyObject.isGeneratedData = true;

        DateTimeOffset current = DateTimeOffset.Now - historyObject.defaultHistoryTimeSpan;
        DateTimeOffset end = DateTimeOffset.Now;
        while (current < end)
        {
            historyObject.Add(new StateObject(Random.value >= 0.5f ? trueValue : falseValue, current.DateTime));
            current = current.AddHours(6);
        }
    }
}