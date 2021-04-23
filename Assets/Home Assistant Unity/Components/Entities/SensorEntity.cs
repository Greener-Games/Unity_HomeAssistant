using System;

[Serializable]
public class SensorEntity : Entity
{
    const string SensorTitle = "sensor_title";
    public string SensorName => currentStateObject != null && currentStateObject.HasAttributeValue(SensorTitle)  ? currentStateObject.GetAttributeValue<string>(SensorTitle) : FriendlyName;
    public string SensorValueName => currentStateObject != null  && currentStateObject.HasAttributeValue(SensorTitle) ? currentStateObject.GetAttributeValue<string>(SensorTitle) : FriendlyName;
}