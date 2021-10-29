using GG.Extensions;
using Sirenix.OdinInspector;

public partial class Entity
{
    const string FriendlyNameKey = "friendly_name";
    const string DeviceClassName = "device_class";
    const string TitleKey = "unity_title_name";
    const string UnitOfMeasureTitle = "unit_of_measurement";
    
    [TabGroup("Current")]
    [ShowInInspector]
    [ReadOnly]
    public string FriendlyName => currentStateObject.GetAttributeValue(FriendlyNameKey, entityId).TitleCase();

    [TabGroup("Current")]
    [ShowInInspector]
    [ReadOnly]
    public string EntityTitle => currentStateObject.GetAttributeValue(TitleKey,FriendlyName).TitleCase();
    
    [TabGroup("Current")]
    [ShowInInspector]
    [ReadOnly]
    public string DeviceClass => currentStateObject.GetAttributeValue(DeviceClassName, "").TitleCase();
    
    [TabGroup("Current")]
    [ShowInInspector]
    [ReadOnly]
    public string UnitOfMeasure => currentStateObject.GetAttributeValue(UnitOfMeasureTitle, "").TitleCase();
}