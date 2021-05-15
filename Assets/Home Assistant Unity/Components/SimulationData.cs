using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class SimulationData
{
    public static void GenerateSimulationInt(this HistoryObject historyObject, int min, int max)
    {
        List<StateObject> simulatedData = new List<StateObject>();
        DateTimeOffset start = DateTimeOffset.Now -historyObject.defaultHistoryTimeSpan;
        DateTimeOffset end = DateTimeOffset.Now;
            while (start < end)
            {
                simulatedData.Add(new StateObject()
                {
                    state = Random.Range(0, 50).ToString(),
                });
                start = start.AddHours(6);
            }

            historyObject.history = simulatedData;
        }
    
    public static void GenerateSimulationInt(this HistoryObject historyObject, float min, float max)
    {
        List<StateObject> simulatedData = new List<StateObject>();
            DateTimeOffset start = DateTimeOffset.Now -historyObject.defaultHistoryTimeSpan;
            DateTimeOffset end = DateTimeOffset.Now;
            while (start < end)
            {
                simulatedData.Add(new StateObject()
                {
                    state = Random.Range(0, 50).ToString(),
                });
                start = start.AddHours(6);
            }

            historyObject.history = simulatedData;
    }
    
    public static void GenerateSimulationBool(this HistoryObject historyObject, string trueValue, string falseValue)
    {
        List<StateObject> simulatedData = new List<StateObject>();
        DateTimeOffset start = DateTimeOffset.Now -historyObject.defaultHistoryTimeSpan;
            DateTimeOffset end = DateTimeOffset.Now;
            while (start < end)
            {
                simulatedData.Add(new StateObject()
                {
                    state = Random.value >= 0.5f ? trueValue : falseValue
                });
                start = start.AddHours(6);
            }

            historyObject.history = simulatedData;
    }
}