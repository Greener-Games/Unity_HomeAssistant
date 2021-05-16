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
        Dictionary<string, object> body = new Dictionary<string, object> {{"entity_id", entityId}};
        currentStateObject = await ServiceRequest.CallService("light","turn_on",body);
        dataFetched?.Invoke(this);
    }
    
    [Button]
    public async void TurnOff()
    {
        Dictionary<string, object> body = new Dictionary<string, object> {{"entity_id", entityId}};
        currentStateObject = await ServiceRequest.CallService("light","turn_off",body);
        dataFetched?.Invoke(this);
    }
    
    protected override void GenerateSimulationData()
    {
        historyObject.GenerateSimulationBool("on", "off");
        currentStateObject = historyObject.history[0];
    }
}
