using Newtonsoft.Json;

public class ContextObject
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("parent_id")]
    public object ParentId { get; set; }

    [JsonProperty("user_id")]
    public object UserId { get; set; }
}