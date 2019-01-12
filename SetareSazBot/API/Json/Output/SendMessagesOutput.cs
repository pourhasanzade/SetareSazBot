using Newtonsoft.Json;

namespace SetareSazBot.API.Json.Output
{
    public class SendMessagesOutput
    {
        [JsonProperty("data")] public SendMessagesOutputData Data { get; set; }
    }

    public class SendMessagesOutputData
    {
        [JsonProperty("message_id")] public string MessageId { get; set; }
    }
}