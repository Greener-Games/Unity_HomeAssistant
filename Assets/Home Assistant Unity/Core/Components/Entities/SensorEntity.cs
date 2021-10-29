using System;

[Serializable]
[EntityWorldGraphic("Sensor Marker")]
[EntityUiElement("Sensor Popup")]
public class SensorEntity : Entity              
{
    const string SensorTitleKey = "sensor_title";
    
    public string SensorTitle => currentStateObject.GetAttributeValue<string>(SensorTitleKey,FriendlyName);
}