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
        await EntityRequest(ServiceClient.CallService("light","turn_on",new { entity_id = this.entityId}));
    }
    
    [Button]
    public async void TurnOff()
    {
        await EntityRequest(ServiceClient.CallService("light","turn_off", new { entity_id = this.entityId}));
    }
    
    [Button]
    public async void SetBrightness(float brightness)
    {
        await EntityRequest(ServiceClient.CallService("light","turn_on", new
        {
            entity_id = this.entityId,
            brightness = brightness
        }));
    }
    
    protected override void GenerateHistoricSimulationData()
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
