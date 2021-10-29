using GG.Extensions;
using Sirenix.OdinInspector;

public partial class Entity
{
    const string FriendlyNameKey = "friendly_name";
    const string DeviceClassName = "device_class";
    const string TitleKey = "unity_title_name";
    const string UnitOfMeasureTitle = "unit_of_measurement";
    
    public string FriendlyName => currentStateObject.GetAttributeValue(FriendlyNameKey, entityId).TitleCase();
    
    public string EntityTitle => currentStateObject.GetAttributeValue(TitleKey,FriendlyName).TitleCase();
    
    public string DeviceClass => currentStateObject.GetAttributeValue(DeviceClassName, "").TitleCase();

    public string UnitOfMeasure => currentStateObject.GetAttributeValue(UnitOfMeasureTitle, "").TitleCase();
}