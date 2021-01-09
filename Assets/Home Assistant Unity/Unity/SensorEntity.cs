using System;
using System.Threading.Tasks;
using UnityEngine.Events;

[Serializable]
public class SensorEntity : Entity
{
    public TimeSpan defaultHistoryTimeSpan;

    public HistoryObject historyObject;
    
    public UnityAction historyFetched;

    protected override async Task ProcessFetchedData()
    {
        await GetDataForTime(defaultHistoryTimeSpan);
    }

    public async Task GetDataForTime(TimeSpan timeSpan)
    {
        historyObject = await RequestClient.GetHistory(entityId, DateTime.Now, timeSpan,true );
        historyFetched?.Invoke();
    }
}