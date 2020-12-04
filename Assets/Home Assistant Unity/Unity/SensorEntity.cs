using System;
using System.Threading.Tasks;

public class SensorEntity : Entity
{
    public HistoryObject historyObject;
    
    protected override async Task CustomFetchData()
    {
        historyObject = await RequestClient.GetHistory(entityId, DateTime.Now,true);
    }
}