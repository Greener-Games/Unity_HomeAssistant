﻿using System;
using System.Threading.Tasks;

[Serializable]
public class SensorEntity : Entity
{
    public HistoryObject historyObject;
    
    protected override async Task ProcessFetchedData()
    {
        historyObject = await RequestClient.GetHistory(entityId, DateTime.Now, TimeSpan.FromDays(1),true );
    }
}