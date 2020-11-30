using Newtonsoft.Json;

public class MessageObject
{
    [JsonProperty("message")]
    public string Message { get; set; }
}