using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class LightEntity : Entity
{
    [Button]
    public async void TurnOn()
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("entity_id", entityId);
        await RequestClient.Post<StateObject>($"api/services/light/turn_on", body);
    }
    
    [Button]
    public async void TurnOff()
    {
        Dictionary<string, object> body = new Dictionary<string, object>();
        body.Add("entity_id", entityId);
        await RequestClient.Post<StateObject>($"api/services/light/turn_off", body);
    }
}
