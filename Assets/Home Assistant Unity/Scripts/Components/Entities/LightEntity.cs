using System;
using System.Collections;
using System.Collections.Generic;
using Requests;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class LightEntity : Entity
{
    public bool LightOn => State == "on";
    
    [Button]
    public async void TurnOn()
    {
        currentStateObject = await ServiceClient.CallService("light","turn_on",new { entity_id = this.entityId});
        dataFetched?.Invoke(this);
    }
    
    [Button]
    public async void TurnOff()
    {
        currentStateObject = await ServiceClient.CallService("light","turn_off", new { entity_id = this.entityId}) ;
        dataFetched?.Invoke(this);
    }
    
    protected override void GenerateSimulationData()
    {
        historyData.GenerateSimulationBool("on", "off", historyTimeSpan);
        isGeneratedData = true;
        currentStateObject = historyData[0];
    }
}
