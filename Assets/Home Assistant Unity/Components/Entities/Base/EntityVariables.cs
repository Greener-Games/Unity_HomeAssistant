using GG.Extensions;
using Sirenix.OdinInspector;

public partial class Entity
{
    const string FriendlyNameKey = "friendly_name";
    const string DeviceClassName = "device_class";
    
    public string FriendlyName => currentStateObject.GetAttributeValue(FriendlyNameKey, entityId).TitleCase();
    
    
    public string DeviceClass => currentStateObject.GetAttributeValue(DeviceClassName, "").TitleCase();

}