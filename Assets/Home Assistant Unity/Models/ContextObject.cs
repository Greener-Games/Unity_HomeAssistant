using Newtonsoft.Json;

[System.Serializable]
public class ContextObject
{
    [JsonProperty("id")]
    public string Id;

    [JsonProperty("parent_id")]
    public object ParentId;

    [JsonProperty("user_id")]
    public object UserId;
}