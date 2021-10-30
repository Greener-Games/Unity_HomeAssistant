using System;
using GG.Extensions;

[Serializable]
[EntityWorldGraphic("Sensor Marker")]
[EntityUiElement("Sensor Popup")]
public class SensorEntity : Entity              
{
    const string UnitOfMeasureTitle = "unit_of_measurement";
    
    
    public string UnitOfMeasure => currentStateObject.GetAttributeValue(UnitOfMeasureTitle, "").TitleCase();
}