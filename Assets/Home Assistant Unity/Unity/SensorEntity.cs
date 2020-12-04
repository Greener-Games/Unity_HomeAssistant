using System;
using System.Threading.Tasks;

public class SensorEntity : Entity
{
    public History history;
    
    protected override async Task CustomFetchData()
    {
        history = await RequestClient.GetHistory(entityId, DateTime.Now.AddDays(-1));
    }
}