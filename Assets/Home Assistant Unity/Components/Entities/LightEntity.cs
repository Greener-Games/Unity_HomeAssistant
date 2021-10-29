using System;
using System.Collections;
using System.Collections.Generic;
using Requests;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
[EntityWorldGraphic("Light Marker")]
[EntityUiElement("Light Popup")]
public class LightEntity : Entity
{
    public bool LightOn => State == "on";

    const string BrightnessKey = "brightness";
    public float Brightness =>  Mathf.Floor((float)currentStateObject.GetAttributeValue<double>(BrightnessKey, 0));
    
    [Button]
    public async void TurnOn()
    {
        StateObject data = await ServiceClient.CallService("light","turn_on",new { entity_id = this.entityId});

        if (data != null)
        {
            currentStateObject = data;
        }

        dataFetched?.Invoke(this);
    }
    
    [Button]
    public async void TurnOff()
    {
        StateObject data = await ServiceClient.CallService("light","turn_off", new { entity_id = this.entityId}) ;
        
        if (data != null)
        {
            currentStateObject = data;
        }
        
        dataFetched?.Invoke(this);
    }
    
    [Button]
    public async void SetBrightness(float brightness)
    {
        StateObject data = await ServiceClient.CallService("light","turn_on", new
        {
            entity_id = this.entityId,
            brightness = brightness
        }) ;
        
        if (data != null)
        {
            currentStateObject = data;
        }
        
        dataFetched?.Invoke(this);
    }
    
    protected override void GenerateSimulationData()
    {
        historyData.GenerateSimulationBool("on", "off", HistoryTimeSpan);
        isGeneratedData = true;
        currentStateObject = historyData[0];
    }

    public void ToggleLight()
    {
        if (LightOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }
}
