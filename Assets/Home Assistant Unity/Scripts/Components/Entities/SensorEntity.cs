using System;

[Serializable]
public class SensorEntity : Entity
{
    const string SensorTitle = "sensor_title";
    public string SensorName => currentStateObject.GetAttributeValue<string>(SensorTitle,FriendlyName);
    public string SensorValueName => currentStateObject.GetAttributeValue<string>(SensorTitle,FriendlyName);
}