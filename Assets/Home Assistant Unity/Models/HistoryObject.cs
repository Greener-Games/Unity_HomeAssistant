using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[System.Serializable]
public class HistoryObject
{
    [OdinSerialize][NonSerialized][ShowInInspector]
    public List<StateObject> history;

    public List<StateObject> AverageHour
    {
        get
        {
            List<StateObject> returnStates = new List<StateObject>();
            DateTime currentTime = history[0].LastUpdated.RoundDown(TimeSpan.FromHours(1));
            List<StateObject> inTime = new List<StateObject>();
            foreach (StateObject stateObject in history)
            {
                if (stateObject.LastUpdated < currentTime.AddHours(1))
                {
                    inTime.Add(stateObject);
                }
                else
                {
                    StateObject so = new StateObject();
                    so.LastUpdated = currentTime;
                    so.State = Enumerable.Average(inTime.Select(x => float.Parse(x.State))).ToString();
                    returnStates.Add(so);

                    inTime = new List<StateObject>();
                    currentTime = currentTime.AddHours(1);
                    inTime.Add(stateObject);
                }
            }

            return returnStates;
        }
    }
}